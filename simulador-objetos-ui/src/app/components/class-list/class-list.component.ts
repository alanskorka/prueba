import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ClassService, Class } from '../../services/class.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-class-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <div class="container mt-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Clases</h2>
        <a routerLink="/classes/new" class="btn btn-primary">
          <i class="bi bi-plus-circle"></i> Nueva Clase
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
                  placeholder="Buscar clases..." 
                  [(ngModel)]="searchTerm"
                  (input)="filterClasses()"
                >
              </div>
            </div>
          </div>

          <div class="table-responsive">
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th class="text-end">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let class of filteredClasses">
                  <td>{{class.name}}</td>
                  <td>
                    <div class="btn-group float-end">
                      <a [routerLink]="['/classes', class.id]" class="btn btn-sm btn-info">
                        <i class="bi bi-eye"></i>
                      </a>
                      <a [routerLink]="['/classes', class.id, 'edit']" class="btn btn-sm btn-warning">
                        <i class="bi bi-pencil"></i>
                      </a>
                      <button class="btn btn-sm btn-danger" (click)="deleteClass(class.id)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
                <tr *ngIf="filteredClasses.length === 0">
                  <td colspan="4" class="text-center py-4">
                    <div class="text-muted">
                      <i class="bi bi-inbox fs-1"></i>
                      <p class="mt-2">No se encontraron clases</p>
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
export class ClassListComponent implements OnInit {
  classes: Class[] = [];
  filteredClasses: Class[] = [];
  searchTerm: string = '';

  constructor(private classService: ClassService) {}

  ngOnInit(): void {
    this.loadClasses();
  }

  loadClasses(): void {
    this.classService.getClasses().subscribe({
      next: (classes) => {
        this.classes = classes;
        this.filterClasses();
      },
      error: (error: Error) => {
        console.error('Error loading classes:', error);
        // TODO: Mostrar mensaje de error al usuario
      }
    });
  }

  filterClasses(): void {
    if (!this.searchTerm) {
      this.filteredClasses = this.classes;
      return;
    }

    const search = this.searchTerm.toLowerCase();
    this.filteredClasses = this.classes.filter(c => 
      c.name.toLowerCase().includes(search)
    );
  }

  deleteClass(id: string | undefined): void {
    if (!id) return;
    if (confirm('¿Está seguro de que desea eliminar esta clase?')) {
      this.classService.deleteClass(id).subscribe({
        next: () => {
          this.classes = this.classes.filter(c => c.id !== id);
          this.filterClasses();
        },
        error: (error: Error) => {
          console.error('Error deleting class:', error);
          // TODO: Mostrar mensaje de error al usuario
        }
      });
    }
  }
}
