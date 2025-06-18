import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MethodService, MethodModel } from '../../services/method.service';
import { ClassService, ClassModel } from '../../services/class.service';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-method-detail',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header d-flex justify-content-between align-items-center">
              <h2 class="mb-0">Detalle del Método</h2>
              <button class="btn btn-secondary btn-sm" (click)="router.navigate(['/methods'])">
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
              <button class="btn btn-primary" (click)="loadMethod()">
                <i class="bi bi-arrow-clockwise"></i> Reintentar
              </button>
            </div>

            <div class="card-body" *ngIf="method && !loading">
              <div class="row">
                <div class="col-md-6">
                  <ul class="list-group mb-3">
                    <li class="list-group-item d-flex justify-content-between">
                      <strong>ID:</strong> 
                      <span class="text-muted">{{ method.id }}</span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between">
                      <strong>Nombre:</strong> 
                      <span class="fw-bold text-primary">{{ method.name }}</span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between">
                      <strong>Tipo de Retorno:</strong> 
                      <span class="badge bg-info">{{ method.returnType || 'void' }}</span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between">
                      <strong>Clase:</strong> 
                      <span class="text-success">{{ getClassName(method.classId) }}</span>
                    </li>
                  </ul>
                </div>
                <div class="col-md-6">
                  <div class="card">
                    <div class="card-header">
                      <h6 class="mb-0">Modificadores</h6>
                    </div>
                    <div class="card-body">
                      <div class="d-flex flex-column gap-2">
                        <div class="d-flex justify-content-between align-items-center">
                          <span>Virtual:</span>
                          <span class="badge" [ngClass]="method.isVirtual ? 'bg-success' : 'bg-secondary'">
                            {{ method.isVirtual ? 'Sí' : 'No' }}
                          </span>
                        </div>
                        <div class="d-flex justify-content-between align-items-center">
                          <span>Static:</span>
                          <span class="badge" [ngClass]="method.isStatic ? 'bg-success' : 'bg-secondary'">
                            {{ method.isStatic ? 'Sí' : 'No' }}
                          </span>
                        </div>
                        <div class="d-flex justify-content-between align-items-center">
                          <span>Override:</span>
                          <span class="badge" [ngClass]="method.isOverride ? 'bg-success' : 'bg-secondary'">
                            {{ method.isOverride ? 'Sí' : 'No' }}
                          </span>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div class="mt-3">
                <button class="btn btn-warning me-2" (click)="router.navigate(['/methods', method.id, 'edit'])">
                  <i class="bi bi-pencil"></i> Editar
                </button>
                <button class="btn btn-danger me-2" (click)="deleteMethod()">
                  <i class="bi bi-trash"></i> Eliminar
                </button>
                <button class="btn btn-secondary" (click)="router.navigate(['/methods'])">
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
export class MethodDetailComponent implements OnInit {
  method: MethodModel | null = null;
  classes: ClassModel[] = [];
  loading = false;
  errorMessage: string | null = null;
  methodId: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private methodService: MethodService,
    private classService: ClassService,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.methodId = this.route.snapshot.paramMap.get('id');
    if (this.methodId) {
      this.loadMethod();
      this.loadClasses();
    }
  }

  loadMethod(): void {
    if (!this.methodId) return;
    
    this.loading = true;
    this.errorMessage = null;
    
    this.methodService.getMethod(this.methodId).subscribe({
      next: (data: MethodModel) => {
        this.method = data;
        this.loading = false;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading method:', error);
        this.errorMessage = 'Error al cargar el método.';
        this.method = null;
        this.loading = false;
      }
    });
  }

  loadClasses(): void {
    this.classService.getClasses().subscribe({
      next: (data: any) => {
        const classes = data.$values || data;
        this.classes = classes;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading classes:', error);
        this.classes = [];
      }
    });
  }

  getClassName(classId: string): string {
    return this.classes.find(c => c.id === classId)?.name || '';
  }

  deleteMethod(): void {
    if (!this.method?.id) return;
    
    if (confirm(`¿Está seguro de que desea eliminar el método '${this.method.name}'?`)) {
      this.loading = true;
      this.methodService.deleteMethod(this.method.id).subscribe({
        next: () => {
          this.router.navigate(['/methods']);
        },
        error: (error: HttpErrorResponse) => {
          console.error('Error deleting method:', error);
          this.errorMessage = 'Error al eliminar el método.';
          this.loading = false;
        }
      });
    }
  }
}
