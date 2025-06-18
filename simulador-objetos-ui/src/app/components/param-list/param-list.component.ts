import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ParamService, ParamModel } from '../../services/param.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-param-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <div class="container mt-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Parámetros</h2>
        <a routerLink="/params/new" class="btn btn-primary">
          <i class="bi bi-plus-circle"></i> Nuevo Parámetro
        </a>
      </div>

      <div class="card">
        <div class="card-body">
          <div class="row mb-3">
            <div class="col-md-6">
              <div class="input-group">
                <span class="input-group-text">
                  <i class="bi bi-search"></i>
                </span>
                <input 
                  type="text" 
                  class="form-control" 
                  placeholder="Buscar parámetros..." 
                  [(ngModel)]="searchTerm"
                  (input)="filterParams()"
                >
              </div>
            </div>
          </div>

          <div *ngIf="loading" class="text-center my-4">
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Cargando...</span>
            </div>
          </div>

          <div *ngIf="errorMessage" class="alert alert-danger">
            <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
          </div>

          <div *ngIf="!loading" class="table-responsive">
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Tipo</th>
                  <th>Método</th>
                  <th>Clase</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let param of filteredParams">
                  <td>{{ param.name }}</td>
                  <td>
                    <span class="badge bg-info">{{ param.type || 'object' }}</span>
                  </td>
                  <td>{{ getMethodName(param.methodId) }}</td>
                  <td>{{ getClassName(param.methodId) }}</td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/params', param.id]" class="btn btn-sm btn-info" title="Ver">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/params', param.id, 'edit']" class="btn btn-sm btn-warning" title="Editar">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-sm btn-danger" title="Eliminar" (click)="deleteParam(param)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
                <tr *ngIf="filteredParams.length === 0 && !loading">
                  <td colspan="5" class="text-center py-4">
                    <div class="text-muted">
                      <i class="bi bi-inbox fs-1"></i>
                      <p class="mt-2">No se encontraron parámetros</p>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .btn-group {
      gap: 0.5rem;
    }
    
    .table th {
      font-weight: 600;
      background-color: #f8f9fa;
    }
    
    .btn-sm {
      padding: 0.25rem 0.5rem;
    }
    
    .badge {
      font-size: 0.75em;
    }
  `]
})
export class ParamListComponent implements OnInit {
  params: ParamModel[] = [];
  filteredParams: ParamModel[] = [];
  methods: MethodModel[] = [];
  loading = false;
  errorMessage: string | null = null;
  searchTerm: string = '';

  constructor(private paramService: ParamService, private methodService: MethodService) {}

  ngOnInit(): void {
    this.loadParams();
    this.loadMethods();
  }

  loadParams(): void {
    this.loading = true;
    this.errorMessage = null;
    this.paramService.getParams().subscribe({
      next: (data: any) => {
        const params = data.$values || data;
        this.params = params;
        this.filterParams();
        this.loading = false;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading params:', error);
        this.errorMessage = 'Error al cargar los parámetros';
        this.params = [];
        this.filteredParams = [];
        this.loading = false;
      }
    });
  }

  loadMethods(): void {
    this.methodService.getMethods().subscribe({
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

  filterParams(): void {
    if (!this.searchTerm) {
      this.filteredParams = this.params;
      return;
    }

    const search = this.searchTerm.toLowerCase();
    this.filteredParams = this.params.filter(p => 
      p.name.toLowerCase().includes(search) ||
      (p.type && p.type.toLowerCase().includes(search)) ||
      this.getMethodName(p.methodId).toLowerCase().includes(search) ||
      this.getClassName(p.methodId).toLowerCase().includes(search)
    );
  }

  getMethodName(methodId: string): string {
    return this.methods.find(m => m.id === methodId)?.name || '';
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

  deleteParam(param: ParamModel): void {
    if (!param?.id) return;
    
    if (confirm(`¿Está seguro de que desea eliminar el parámetro '${param.name}'?`)) {
      this.loading = true;
      this.paramService.deleteParam(param.id).subscribe({
        next: () => {
          this.loadParams();
        },
        error: (error: HttpErrorResponse) => {
          console.error('Error deleting param:', error);
          this.errorMessage = 'Error al eliminar el parámetro';
          this.loading = false;
        }
      });
    }
  }
}
