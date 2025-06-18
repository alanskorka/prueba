import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AttributeService, AttributeModel } from '../../services/attribute.service';

@Component({
  selector: 'app-attribute-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="container mt-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Atributos</h2>
        <a routerLink="/attributes/new" class="btn btn-primary">
          <i class="bi bi-plus-circle"></i> Nuevo Atributo
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
          <div *ngIf="!loading && attributes.length > 0" class="table-responsive">
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Clase</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let attr of attributes">
                  <td>{{ attr.name }}</td>
                  <td>{{ attr.classId }}</td>
                  <td>
                    <div class="btn-group float-end">
                      <!-- Aquí puedes agregar acciones como ver, editar, eliminar -->
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div *ngIf="!loading && attributes.length === 0" class="text-center text-muted py-4">
            <i class="bi bi-inbox fs-1"></i>
            <p class="mt-2">No hay atributos registrados</p>
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
export class AttributeListComponent implements OnInit {
  attributes: AttributeModel[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(private attributeService: AttributeService) {}

  ngOnInit(): void {
    this.loadAttributes();
  }

  loadAttributes(): void {
    this.loading = true;
    this.errorMessage = null;
    this.attributeService.getAttributes().subscribe({
      next: (data) => {
        this.attributes = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar los atributos';
        this.loading = false;
      }
    });
  }
}
