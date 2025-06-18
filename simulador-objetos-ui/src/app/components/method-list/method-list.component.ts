import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MethodService, MethodModel } from '../../services/method.service';
import { ClassService, ClassModel } from '../../services/class.service';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-method-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <div class="container mt-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Métodos</h2>
        <a routerLink="/methods/new" class="btn btn-primary">
          <i class="bi bi-plus-circle"></i> Nuevo Método
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
                  placeholder="Buscar métodos..." 
                  [(ngModel)]="searchTerm"
                  (input)="filterMethods()"
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
                  <th>Tipo Retorno</th>
                  <th>Clase</th>
                  <th>Modificadores</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let method of filteredMethods">
                  <td>{{ method.name }}</td>
                  <td>{{ method.returnType || 'void' }}</td>
                  <td>{{ getClassName(method.classId) }}</td>
                  <td>
                    <span *ngIf="method.isVirtual" class="badge bg-info me-1">virtual</span>
                    <span *ngIf="method.isStatic" class="badge bg-success me-1">static</span>
                    <span *ngIf="method.isOverride" class="badge bg-warning me-1">override</span>
                  </td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/methods', method.id]" class="btn btn-sm btn-info" title="Ver">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/methods', method.id, 'edit']" class="btn btn-sm btn-warning" title="Editar">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-sm btn-danger" title="Eliminar" (click)="deleteMethod(method)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
                <tr *ngIf="filteredMethods.length === 0 && !loading">
                  <td colspan="5" class="text-center py-4">
                    <div class="text-muted">
                      <i class="bi bi-inbox fs-1"></i>
                      <p class="mt-2">No se encontraron métodos</p>
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
export class MethodListComponent implements OnInit {
  methods: MethodModel[] = [];
  filteredMethods: MethodModel[] = [];
  classes: ClassModel[] = [];
  loading = false;
  errorMessage: string | null = null;
  searchTerm: string = '';

  constructor(private methodService: MethodService, private classService: ClassService) {}

  ngOnInit(): void {
    this.loadMethods();
    this.loadClasses();
  }

  loadMethods(): void {
    this.loading = true;
    this.errorMessage = null;
    this.methodService.getMethods().subscribe({
      next: (data: any) => {
        const methods = data.$values || data;
        this.methods = methods;
        this.filterMethods();
        this.loading = false;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading methods:', error);
        this.errorMessage = 'Error al cargar los métodos';
        this.methods = [];
        this.filteredMethods = [];
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

  filterMethods(): void {
    if (!this.searchTerm) {
      this.filteredMethods = this.methods;
      return;
    }

    const search = this.searchTerm.toLowerCase();
    this.filteredMethods = this.methods.filter(m => 
      m.name.toLowerCase().includes(search) ||
      (m.returnType && m.returnType.toLowerCase().includes(search)) ||
      this.getClassName(m.classId).toLowerCase().includes(search)
    );
  }

  getClassName(classId: string): string {
    return this.classes.find(c => c.id === classId)?.name || '';
  }

  deleteMethod(method: MethodModel): void {
    if (!method?.id) return;
    
    if (confirm(`¿Está seguro de que desea eliminar el método '${method.name}'?`)) {
      this.loading = true;
      this.methodService.deleteMethod(method.id).subscribe({
        next: () => {
          this.loadMethods();
        },
        error: (error: HttpErrorResponse) => {
          console.error('Error deleting method:', error);
          this.errorMessage = 'Error al eliminar el método';
          this.loading = false;
        }
      });
    }
  }
}
