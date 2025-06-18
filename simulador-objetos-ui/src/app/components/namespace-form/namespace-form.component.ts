import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NamespaceService, NamespaceModel } from '../../services/namespace.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-namespace-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header">
              <h2 class="mb-0">Crear Nuevo Namespace</h2>
            </div>
            <div class="card-body">
              <div *ngIf="successMessage" class="alert alert-success">
                <i class="bi bi-check-circle"></i> {{ successMessage }}
              </div>
              <div *ngIf="errorMessage" class="alert alert-danger">
                <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
              </div>

              <form [formGroup]="namespaceForm" (ngSubmit)="onSubmit()">
                <div class="mb-3">
                  <label for="name" class="form-label">Nombre</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="name" 
                    formControlName="name"
                    [ngClass]="{'is-invalid': namespaceForm.get('name')?.invalid && namespaceForm.get('name')?.touched}"
                  >
                  <div class="invalid-feedback" *ngIf="namespaceForm.get('name')?.invalid && namespaceForm.get('name')?.touched">
                    El nombre es requerido
                  </div>
                </div>

                <div class="d-flex gap-2">
                  <button type="submit" class="btn btn-primary" [disabled]="namespaceForm.invalid">
                    <i class="bi bi-plus-circle"></i> Crear Namespace
                  </button>
                  <button type="button" class="btn btn-secondary" (click)="router.navigate(['/namespaces'])">
                    <i class="bi bi-x-circle"></i> Cancelar
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .card {
      box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
    }
    .card-header {
      background-color: #f8f9fa;
      border-bottom: 1px solid rgba(0, 0, 0, 0.125);
    }
    .form-label {
      font-weight: 500;
    }
  `]
})
export class NamespaceFormComponent {
  namespaceForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private namespaceService: NamespaceService,
    public router: Router
  ) {
    this.namespaceForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  onSubmit(): void {
    if (this.namespaceForm.valid) {
      const namespaceData: NamespaceModel = {
        name: this.namespaceForm.value.name
      } as any;
      this.namespaceService.createNamespace(namespaceData).subscribe({
        next: () => {
          this.successMessage = '¡Namespace creado exitosamente!';
          this.errorMessage = null;
          setTimeout(() => this.router.navigate(['/namespaces']), 2000);
        },
        error: (error: Error) => {
          this.errorMessage = `Error al crear el namespace: ${error.message}`;
          this.successMessage = null;
        }
      });
    }
  }
}
