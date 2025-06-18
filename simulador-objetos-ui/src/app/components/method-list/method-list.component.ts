import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MethodService, MethodModel } from '../../services/method.service';
import { ClassService, ClassModel } from '../../services/class.service';

@Component({
  selector: 'app-method-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
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
          <div *ngIf="loading" class="text-center my-4">
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Cargando...</span>
            </div>
          </div>
          <div *ngIf="errorMessage" class="alert alert-danger">
            <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
          </div>
          <div *ngIf="!loading && methods.length > 0" class="table-responsive">
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Tipo Retorno</th>
                  <th>Clase</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let method of methods">
                  <td>{{ method.name }}</td>
                  <td>{{ method.returnType }}</td>
                  <td>{{ getClassName(method.classId) }}</td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/methods', method.id]" class="btn btn-outline-primary btn-sm" title="Ver">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/methods', method.id, 'edit']" class="btn btn-outline-secondary btn-sm" title="Editar">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-outline-danger btn-sm" title="Eliminar" (click)="deleteMethod(method)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div *ngIf="!loading && methods.length === 0" class="text-center text-muted py-4">
            <i class="bi bi-inbox fs-1"></i>
            <p class="mt-2">No hay métodos registrados</p>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .btn-group { gap: 0.5rem; }
    .table th { font-weight: 600; background-color: #f8f9fa; }
  `]
})
export class MethodListComponent implements OnInit {
  methods: MethodModel[] = [];
  classes: ClassModel[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(private methodService: MethodService, private classService: ClassService) {}

  ngOnInit(): void {
    this.loadMethods();
    this.classService.getClasses().subscribe({
      next: (data) => this.classes = data,
      error: () => this.errorMessage = 'Error al cargar las clases.'
    });
  }

  loadMethods(): void {
    this.loading = true;
    this.errorMessage = null;
    this.methodService.getMethods().subscribe({
      next: (data) => {
        this.methods = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar los métodos';
        this.loading = false;
      }
    });
  }

  getClassName(classId: string): string {
    return this.classes.find(c => c.id === classId)?.name || '';
  }

  deleteMethod(method: MethodModel): void {
    if (confirm(`¿Seguro que deseas eliminar el método '${method.name}'?`)) {
      this.loading = true;
      this.methodService.deleteMethod(method.id!).subscribe({
        next: () => {
          this.loadMethods();
        },
        error: () => {
          this.errorMessage = 'Error al eliminar el método';
          this.loading = false;
        }
      });
    }
  }
}
