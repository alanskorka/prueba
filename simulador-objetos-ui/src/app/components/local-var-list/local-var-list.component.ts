import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { LocalVarService } from '../../services/local-var.service';
import { MethodService, MethodModel } from '../../services/method.service';

@Component({
  selector: 'app-local-var-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="container mt-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Variables Locales</h2>
        <a routerLink="/local-vars/new" class="btn btn-primary">
          <i class="bi bi-plus-circle"></i> Nueva Variable Local
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
          <div *ngIf="!loading && localVars.length > 0" class="table-responsive">
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
                <tr *ngFor="let v of localVars">
                  <td>{{ v.name }}</td>
                  <td>{{ v.type }}</td>
                  <td>{{ getMethodName(v.methodId) }}</td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/local-vars', v.id]" class="btn btn-outline-primary btn-sm" title="Ver">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/local-vars', v.id, 'edit']" class="btn btn-outline-secondary btn-sm" title="Editar">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-outline-danger btn-sm" title="Eliminar" (click)="deleteLocalVar(v)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div *ngIf="!loading && localVars.length === 0" class="text-center text-muted py-4">
            <i class="bi bi-inbox fs-1"></i>
            <p class="mt-2">No hay variables locales registradas</p>
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
export class LocalVarListComponent implements OnInit {
  localVars: any[] = [];
  methods: MethodModel[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(private localVarService: LocalVarService, private methodService: MethodService) {}

  ngOnInit(): void {
    this.loadLocalVars();
    this.methodService.getMethods().subscribe({
      next: (data) => this.methods = data,
      error: () => this.errorMessage = 'Error al cargar los métodos.'
    });
  }

  loadLocalVars(): void {
    this.loading = true;
    this.errorMessage = null;
    this.localVarService.getLocalVars().subscribe({
      next: (data) => {
        this.localVars = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar las variables locales';
        this.loading = false;
      }
    });
  }

  getMethodName(methodId: string): string {
    return this.methods.find(m => m.id === methodId)?.name || '';
  }

  deleteLocalVar(v: any): void {
    if (confirm(`¿Seguro que deseas eliminar la variable local?`)) {
      this.loading = true;
      this.localVarService.deleteLocalVar(v.id).subscribe({
        next: () => {
          this.loadLocalVars();
        },
        error: () => {
          this.errorMessage = 'Error al eliminar la variable local';
          this.loading = false;
        }
      });
    }
  }
}
