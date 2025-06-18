import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { InterfaceMethodService } from '../../services/interface-method.service';
import { InterfaceService } from '../../services/interface.service';

@Component({
  selector: 'app-interface-method-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="container mt-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Métodos de Interfaz</h2>
        <a routerLink="/interface-methods/new" class="btn btn-primary">
          <i class="bi bi-plus-circle"></i> Nuevo Método de Interfaz
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
          <div *ngIf="!loading && interfaceMethods.length > 0" class="table-responsive">
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Tipo de Retorno</th>
                  <th>Interfaz</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let method of interfaceMethods">
                  <td>{{ method.name }}</td>
                  <td>{{ method.returnType }}</td>
                  <td>{{ getInterfaceName(method.interfaceId) }}</td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/interface-methods', method.id]" class="btn btn-outline-primary btn-sm" title="Ver">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/interface-methods', method.id, 'edit']" class="btn btn-outline-secondary btn-sm" title="Editar">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-outline-danger btn-sm" title="Eliminar" (click)="deleteInterfaceMethod(method)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div *ngIf="!loading && interfaceMethods.length === 0" class="text-center text-muted py-4">
            <i class="bi bi-inbox fs-1"></i>
            <p class="mt-2">No hay métodos de interfaz registrados</p>
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
export class InterfaceMethodListComponent implements OnInit {
  interfaceMethods: any[] = [];
  interfaces: any[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(private interfaceMethodService: InterfaceMethodService, private interfaceService: InterfaceService) {}

  ngOnInit(): void {
    this.loadInterfaceMethods();
    this.interfaceService.getInterfaces().subscribe({
      next: (data) => this.interfaces = data,
      error: () => this.errorMessage = 'Error al cargar las interfaces.'
    });
  }

  loadInterfaceMethods(): void {
    this.loading = true;
    this.errorMessage = null;
    this.interfaceMethodService.getInterfaceMethods().subscribe({
      next: (data) => {
        this.interfaceMethods = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar los métodos de interfaz';
        this.loading = false;
      }
    });
  }

  getInterfaceName(interfaceId: string): string {
    return this.interfaces.find(i => i.id === interfaceId)?.name || '';
  }

  deleteInterfaceMethod(method: any): void {
    if (confirm(`¿Seguro que deseas eliminar el método de interfaz?`)) {
      this.loading = true;
      this.interfaceMethodService.deleteInterfaceMethod(method.id).subscribe({
        next: () => {
          this.loadInterfaceMethods();
        },
        error: () => {
          this.errorMessage = 'Error al eliminar el método de interfaz';
          this.loading = false;
        }
      });
    }
  }
}
