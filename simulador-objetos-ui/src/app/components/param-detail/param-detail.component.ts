import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ParamService, ParamModel } from '../../services/param.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-param-detail',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header d-flex justify-content-between align-items-center">
              <h2 class="mb-0">Detalle del Parámetro</h2>
              <button class="btn btn-secondary btn-sm" (click)="router.navigate(['/params'])">
                <i class="bi bi-arrow-left"></i> Volver
              </button>
            </div>
            
            <div class="card-body" *ngIf="loading">
              <div class="text-center my-4">
                <div class="spinner-border" role="status">
                  <span class="visually-hidden">Cargando...</span>
                </div>
              </div>
            </div>

            <div class="card-body" *ngIf="errorMessage">
              <div class="alert alert-danger">
                <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
              </div>
              <button class="btn btn-primary" (click)="loadParam()">
                <i class="bi bi-arrow-clockwise"></i> Reintentar
              </button>
            </div>

            <div class="card-body" *ngIf="param && !loading">
              <div class="row">
                <div class="col-md-6">
                  <ul class="list-group mb-3">
                    <li class="list-group-item d-flex justify-content-between">
                      <strong>ID:</strong> 
                      <span class="text-muted">{{ param.id }}</span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between">
                      <strong>Nombre:</strong> 
                      <span class="fw-bold text-primary">{{ param.name }}</span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between">
                      <strong>Tipo:</strong> 
                      <span class="badge bg-info">{{ param.type || 'object' }}</span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between">
                      <strong>Método:</strong> 
                      <span class="text-success">{{ getMethodName(param.methodId) }}</span>
                    </li>
                  </ul>
                </div>
                <div class="col-md-6">
                  <div class="card">
                    <div class="card-header">
                      <h6 class="mb-0">Información del Método</h6>
                    </div>
                    <div class="card-body">
                      <div class="d-flex flex-column gap-2">
                        <div class="d-flex justify-content-between align-items-center">
                          <span>Nombre del método:</span>
                          <span class="fw-bold">{{ getMethodName(param.methodId) }}</span>
                        </div>
                        <div class="d-flex justify-content-between align-items-center">
                          <span>Tipo de retorno:</span>
                          <span class="badge bg-secondary">{{ getMethodReturnType(param.methodId) }}</span>
                        </div>
                        <div class="d-flex justify-content-between align-items-center">
                          <span>Clase:</span>
                          <span class="text-info">{{ getClassName(param.methodId) }}</span>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div class="mt-3">
                <button class="btn btn-warning me-2" (click)="router.navigate(['/params', param.id, 'edit'])">
                  <i class="bi bi-pencil"></i> Editar
                </button>
                <button class="btn btn-danger me-2" (click)="deleteParam()">
                  <i class="bi bi-trash"></i> Eliminar
                </button>
                <button class="btn btn-secondary" (click)="router.navigate(['/params'])">
                  <i class="bi bi-x-circle"></i> Volver a la lista
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .card {
      box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
    }
    
    .card-header {
      background-color: #f8f9fa;
      border-bottom: 1px solid rgba(0, 0, 0, 0.125);
    }
    
    .list-group-item {
      border-left: none;
      border-right: none;
    }
    
    .badge {
      font-size: 0.875em;
    }
  `]
})
export class ParamDetailComponent implements OnInit, OnDestroy {
  param: ParamModel | null = null;
  methods: MethodModel[] = [];
  loading = false;
  errorMessage: string | null = null;
  paramId: string | null = null;
  private destroy$ = new Subject<void>();

  constructor(
    private route: ActivatedRoute,
    private paramService: ParamService,
    private methodService: MethodService,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.paramId = this.route.snapshot.paramMap.get('id');
    if (this.paramId) {
      this.loadParam();
      this.loadMethods();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadParam(): void {
    if (!this.paramId) return;
    
    this.loading = true;
    this.errorMessage = null;
    
    this.paramService.getParam(this.paramId).pipe(takeUntil(this.destroy$)).subscribe({
      next: (data: ParamModel) => {
        this.param = data;
        this.loading = false;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading param:', error);
        this.errorMessage = 'Error al cargar el parámetro.';
        this.param = null;
        this.loading = false;
      }
    });
  }

  loadMethods(): void {
    this.methodService.getMethods().pipe(takeUntil(this.destroy$)).subscribe({
      next: (data: any) => {
        const methods = data.$values || data;
        this.methods = methods;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading methods:', error);
        this.methods = [];
      }
    });
  }
  
  getMethodName(methodId: string): string {
    return this.methods.find(m => m.id === methodId)?.name || '';
  }

  getMethodReturnType(methodId: string): string {
    const method = this.methods.find(m => m.id === methodId);
    return method?.returnType || 'void';
  }

  getClassName(methodId: string): string {
    const method = this.methods.find(m => m.id === methodId);
    return method ? this.getClassNameFromMethod(method) : '';
  }

  private getClassNameFromMethod(method: MethodModel): string {
    // Aquí necesitaríamos obtener la clase del método
    // Por ahora retornamos un valor por defecto
    return 'Clase';
  }

  deleteParam(): void {
    if (!this.param?.id) return;
    
    if (confirm(`¿Está seguro de que desea eliminar el parámetro '${this.param.name}'?`)) {
      this.loading = true;
      this.paramService.deleteParam(this.param.id).pipe(takeUntil(this.destroy$)).subscribe({
        next: () => {
          this.router.navigate(['/params']);
        },
        error: (error: HttpErrorResponse) => {
          console.error('Error deleting param:', error);
          this.errorMessage = 'Error al eliminar el parámetro.';
          this.loading = false;
        }
      });
    }
  }
}
