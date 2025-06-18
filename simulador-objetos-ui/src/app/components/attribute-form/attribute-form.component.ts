import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AttributeService, AttributeModel } from '../../services/attribute.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-attribute-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header">
              <h2 class="mb-0">Crear Nuevo Atributo</h2>
            </div>
            <div class="card-body">
              <div *ngIf="successMessage" class="alert alert-success">
                <i class="bi bi-check-circle"></i> {{ successMessage }}
              </div>
              <div *ngIf="errorMessage" class="alert alert-danger">
                <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
              </div>

              <form [formGroup]="attributeForm" (ngSubmit)="onSubmit()">
                <div class="mb-3">
                  <label for="name" class="form-label">Nombre</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="name" 
                    formControlName="name"
                    [ngClass]="{'is-invalid': attributeForm.get('name')?.invalid && attributeForm.get('name')?.touched}"
                  >
                  <div class="invalid-feedback" *ngIf="attributeForm.get('name')?.invalid && attributeForm.get('name')?.touched">
                    El nombre es requerido
                  </div>
                </div>

                <div class="mb-3">
                  <label for="classId" class="form-label">Clase (ID)</label>
                  <input 
                    type="number" 
                    class="form-control" 
                    id="classId" 
                    formControlName="classId"
                    [ngClass]="{'is-invalid': attributeForm.get('classId')?.invalid && attributeForm.get('classId')?.touched}"
                  >
                  <div class="invalid-feedback" *ngIf="attributeForm.get('classId')?.invalid && attributeForm.get('classId')?.touched">
                    El ID de la clase es requerido
                  </div>
                </div>

                <div class="d-flex gap-2">
                  <button type="submit" class="btn btn-primary" [disabled]="attributeForm.invalid">
                    <i class="bi bi-plus-circle"></i> Crear Atributo
                  </button>
                  <button type="button" class="btn btn-secondary" (click)="router.navigate(['/attributes'])">
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
export class AttributeFormComponent {
  attributeForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private attributeService: AttributeService,
    public router: Router
  ) {
    this.attributeForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      classId: [null, [Validators.required, Validators.min(1)]]
    });
  }

  onSubmit(): void {
    if (this.attributeForm.valid) {
      const attributeData: AttributeModel = {
        name: this.attributeForm.value.name,
        classId: this.attributeForm.value.classId
      };
      this.attributeService.createAttribute(attributeData).subscribe({
        next: () => {
          this.successMessage = '¡Atributo creado exitosamente!';
          this.errorMessage = null;
          setTimeout(() => this.router.navigate(['/attributes']), 2000);
        },
        error: (error: Error) => {
          this.errorMessage = `Error al crear el atributo: ${error.message}`;
          this.successMessage = null;
        }
      });
    }
  }
}
