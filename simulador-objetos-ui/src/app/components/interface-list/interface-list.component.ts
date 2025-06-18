import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { InterfaceService } from '../../services/interface.service';
import { InterfaceModel } from '../../models/interface.model';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-interface-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <div class="container mt-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Interfaces</h2>
        <a routerLink="/interfaces/new" class="btn btn-primary">
          <i class="bi bi-plus-circle"></i> Nueva Interface
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
                  placeholder="Buscar interfaces..." 
                  [(ngModel)]="searchTerm"
                  (input)="filterInterfaces()"
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

          <div *ngIf="!loading && !errorMessage" class="table-responsive">
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let i of filteredInterfaces">
                  <td>{{ i.name }}</td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/interfaces', i.id]" class="btn btn-sm btn-info">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/interfaces', i.id, 'edit']" class="btn btn-sm btn-warning">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-sm btn-danger" (click)="deleteInterface(i)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
                 <tr *ngIf="filteredInterfaces.length === 0">
                  <td colspan="2" class="text-center py-4">
                    <div class="text-muted">
                      <i class="bi bi-inbox fs-1"></i>
                      <p class="mt-2">No se encontraron interfaces</p>
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
  `]
})
export class InterfaceListComponent implements OnInit {
  interfaces: InterfaceModel[] = [];
  filteredInterfaces: InterfaceModel[] = [];
  searchTerm: string = '';
  loading = false;
  errorMessage: string | null = null;

  constructor(private interfaceService: InterfaceService) {}

  ngOnInit(): void {
    this.loadInterfaces();
  }

  loadInterfaces(): void {
    this.loading = true;
    this.errorMessage = null;
    this.interfaceService.getInterfaces().subscribe({
      next: (data: any) => {
        const interfaces = data.$values || data;
        this.interfaces = interfaces;
        this.filteredInterfaces = interfaces;
        this.loading = false;
      },
      error: (error: HttpErrorResponse) => {
        this.errorMessage = 'Error al cargar las interfaces';
        this.loading = false;
        console.error('Error loading interfaces:', error);
      }
    });
  }

  filterInterfaces(): void {
    if (!this.searchTerm) {
      this.filteredInterfaces = this.interfaces;
      return;
    }

    const search = this.searchTerm.toLowerCase();
    this.filteredInterfaces = this.interfaces.filter(i => 
      i.name.toLowerCase().includes(search)
    );
  }

  deleteInterface(interfaceToDelete: InterfaceModel): void {
    if (!interfaceToDelete?.id) return;
    if (confirm('¿Está seguro de que desea eliminar esta interface?')) {
      this.interfaceService.deleteInterface(interfaceToDelete).subscribe({
        next: () => {
          this.interfaces = this.interfaces.filter(i => i.id !== interfaceToDelete.id);
          this.filterInterfaces();
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage = 'Error al eliminar la interface.';
          console.error('Error deleting interface:', error);
        }
      });
    }
  }
}
