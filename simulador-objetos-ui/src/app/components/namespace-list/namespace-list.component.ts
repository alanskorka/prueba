import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { NamespaceService, NamespaceModel } from '../../services/namespace.service';

@Component({
  selector: 'app-namespace-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="container mt-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Namespaces</h2>
        <a routerLink="/namespaces/new" class="btn btn-primary">
          <i class="bi bi-plus-circle"></i> Nuevo Namespace
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
          <div *ngIf="!loading && namespaces.length > 0" class="table-responsive">
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let ns of namespaces">
                  <td>{{ ns.name }}</td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/namespaces', ns.id]" class="btn btn-outline-primary btn-sm" title="Ver">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/namespaces', ns.id, 'edit']" class="btn btn-outline-secondary btn-sm" title="Editar">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-outline-danger btn-sm" title="Eliminar" (click)="deleteNamespace(ns)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div *ngIf="!loading && namespaces.length === 0" class="text-center text-muted py-4">
            <i class="bi bi-inbox fs-1"></i>
            <p class="mt-2">No hay namespaces registrados</p>
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
  `]
})
export class NamespaceListComponent implements OnInit {
  namespaces: NamespaceModel[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(private namespaceService: NamespaceService) {}

  ngOnInit(): void {
    this.loadNamespaces();
  }

  loadNamespaces(): void {
    this.loading = true;
    this.errorMessage = null;
    this.namespaceService.getNamespaces().subscribe({
      next: (data) => {
        this.namespaces = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar los namespaces';
        this.loading = false;
      }
    });
  }

  deleteNamespace(ns: NamespaceModel): void {
    if (confirm(`¿Seguro que deseas eliminar el namespace '${ns.name}'?`)) {
      this.loading = true;
      this.namespaceService.deleteNamespace(ns).subscribe({
        next: () => {
          this.loadNamespaces();
        },
        error: () => {
          this.errorMessage = 'Error al eliminar el namespace';
          this.loading = false;
        }
      });
    }
  }
}
