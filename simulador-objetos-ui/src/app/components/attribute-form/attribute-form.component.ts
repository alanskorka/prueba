import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AttributeService, AttributeModel } from '../../services/attribute.service';
import { CommonModule } from '@angular/common';
import { ClassService, ClassModel } from '../../services/class.service';

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
                  <label for="classId" class="form-label">Clase</label>
                  <select 
                    class="form-select" 
                    id="classId" 
                    formControlName="classId"
                    [ngClass]="{'is-invalid': attributeForm.get('classId')?.invalid && attributeForm.get('classId')?.touched}"
                  >
                    <option value="">Seleccione una clase</option>
                    <option *ngFor="let class of classes" [value]="class.id">{{ class.name }}</option>
                  </select>
                  <div class="invalid-feedback" *ngIf="attributeForm.get('classId')?.invalid && attributeForm.get('classId')?.touched">
                    La clase es requerida
                  </div>
                </div>

                <div class="mb-3">
                  <label for="type" class="form-label">Tipo</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="type" 
                    formControlName="type"
                    [ngClass]="{'is-invalid': attributeForm.get('type')?.invalid && attributeForm.get('type')?.touched}"
                  >
                  <div class="invalid-feedback" *ngIf="attributeForm.get('type')?.invalid && attributeForm.get('type')?.touched">
                    El tipo es requerido
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
  classes: ClassModel[] = [];

  constructor(
    private fb: FormBuilder,
    private attributeService: AttributeService,
    public router: Router,
    private classService: ClassService
  ) {
    this.attributeForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      classId: ['', [Validators.required]],
      type: ['', [Validators.required]]
    });
    this.classService.getClasses().subscribe({
      next: (data) => this.classes = data.filter(c => !c.isSealed),
      error: () => this.errorMessage = 'Error al cargar las clases.'
    });
  }

  onSubmit(): void {
    if (this.attributeForm.valid) {
      const attributeData: AttributeModel = {
        name: this.attributeForm.value.name,
        classId: String(this.attributeForm.value.classId),
        type: this.attributeForm.value.type
      };
      this.attributeService.createAttribute(attributeData).subscribe({
        next: () => {
          this.successMessage = '¡Atributo creado exitosamente!';
          this.errorMessage = null;
          this.router.navigate(['/attributes']);
        },
        error: (error: Error) => {
          this.errorMessage = `Error al crear el atributo: ${error.message}`;
          this.successMessage = null;
        }
      });
    }
  }
}
