import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ClassService, Class } from '../../services/class.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-class-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header">
              <h2 class="mb-0">Crear Nueva Clase</h2>
            </div>
            <div class="card-body">
              <div *ngIf="successMessage" class="alert alert-success">
                <i class="bi bi-check-circle"></i> {{ successMessage }}
              </div>
              <div *ngIf="errorMessage" class="alert alert-danger">
                <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
              </div>

              <form [formGroup]="classForm" (ngSubmit)="onSubmit()">
                <div class="mb-3">
                  <label for="name" class="form-label">Nombre</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="name" 
                    formControlName="name"
                    [ngClass]="{'is-invalid': classForm.get('name')?.invalid && classForm.get('name')?.touched}"
                  >
                  <div class="invalid-feedback" *ngIf="classForm.get('name')?.invalid && classForm.get('name')?.touched">
                    El nombre es requerido
                  </div>
                </div>

                <div class="mb-3">
                  <div class="form-check">
                    <input 
                      type="checkbox" 
                      class="form-check-input" 
                      id="isAbstract" 
                      formControlName="isAbstract"
                    >
                    <label class="form-check-label" for="isAbstract">Clase Abstracta</label>
                  </div>
                </div>

                <div class="mb-3">
                  <div class="form-check">
                    <input 
                      type="checkbox" 
                      class="form-check-input" 
                      id="isSealed" 
                      formControlName="isSealed"
                    >
                    <label class="form-check-label" for="isSealed">Clase Sellada</label>
                  </div>
                </div>

                <div class="d-flex gap-2">
                  <button type="submit" class="btn btn-primary" [disabled]="classForm.invalid">
                    <i class="bi bi-plus-circle"></i> Crear Clase
                  </button>
                  <button type="button" class="btn btn-secondary" (click)="router.navigate(['/classes'])">
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
export class ClassFormComponent implements OnInit {
  classForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private classService: ClassService,
    public router: Router
  ) {
    this.classForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      isAbstract: [false],
      isSealed: [false]
    });
  }

  ngOnInit(): void {
    // Aquí podríamos cargar las clases base disponibles si es necesario
  }

  onSubmit(): void {
    if (this.classForm.valid) {
      const classData: Class = {
        name: this.classForm.value.name,
        isAbstract: this.classForm.value.isAbstract,
        isSealed: this.classForm.value.isSealed,
        attributes: [],
        methods: []
      } as any;
      this.classService.createClass(classData).subscribe({
        next: () => {
          this.successMessage = '¡Clase creada exitosamente!';
          this.errorMessage = null;
          setTimeout(() => this.router.navigate(['/classes']), 2000);
        },
        error: (error: Error) => {
          this.errorMessage = `Error al crear la clase: ${error.message}`;
          this.successMessage = null;
        }
      });
    }
  }
}
