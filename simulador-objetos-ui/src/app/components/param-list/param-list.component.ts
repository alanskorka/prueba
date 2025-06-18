import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ParamService, ParamModel } from '../../services/param.service';
import { MethodService, MethodModel } from '../../services/method.service';

@Component({
  selector: 'app-param-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
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
          <div *ngIf="loading" class="text-center my-4">
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Cargando...</span>
            </div>
          </div>
          <div *ngIf="errorMessage" class="alert alert-danger">
            <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
          </div>
          <div *ngIf="!loading && params.length > 0" class="table-responsive">
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Tipo</th>
                  <th>Método</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let param of params">
                  <td>{{ param.name }}</td>
                  <td>{{ param.type }}</td>
                  <td>{{ getMethodName(param.methodId) }}</td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/params', param.id]" class="btn btn-outline-primary btn-sm" title="Ver">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/params', param.id, 'edit']" class="btn btn-outline-secondary btn-sm" title="Editar">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-outline-danger btn-sm" title="Eliminar" (click)="deleteParam(param)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div *ngIf="!loading && params.length === 0" class="text-center text-muted py-4">
            <i class="bi bi-inbox fs-1"></i>
            <p class="mt-2">No hay parámetros registrados</p>
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
export class ParamListComponent implements OnInit {
  params: ParamModel[] = [];
  methods: MethodModel[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(private paramService: ParamService, private methodService: MethodService) {}

  ngOnInit(): void {
    this.loadParams();
    this.methodService.getMethods().subscribe({
      next: (data) => this.methods = data,
      error: () => this.errorMessage = 'Error al cargar los métodos.'
    });
  }

  loadParams(): void {
    this.loading = true;
    this.errorMessage = null;
    this.paramService.getParams().subscribe({
      next: (data) => {
        this.params = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar los parámetros';
        this.loading = false;
      }
    });
  }

  getMethodName(methodId: string): string {
    return this.methods.find(m => m.id === methodId)?.name || '';
  }

  deleteParam(param: ParamModel): void {
    if (confirm(`¿Seguro que deseas eliminar el parámetro '${param.name}'?`)) {
      this.loading = true;
      this.paramService.deleteParam(param.id!).subscribe({
        next: () => {
          this.loadParams();
        },
        error: () => {
          this.errorMessage = 'Error al eliminar el parámetro';
          this.loading = false;
        }
      });
    }
  }
}
