import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MethodService, MethodModel } from '../../services/method.service';
import { ClassService, ClassModel } from '../../services/class.service';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-method-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header">
              <h2 class="mb-0">{{ isEditMode ? 'Editar Método' : 'Crear Nuevo Método' }}</h2>
            </div>
            <div class="card-body">
              <div *ngIf="successMessage" class="alert alert-success">
                <i class="bi bi-check-circle"></i> {{ successMessage }}
              </div>
              <div *ngIf="errorMessage" class="alert alert-danger">
                <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
              </div>

              <form [formGroup]="methodForm" (ngSubmit)="onSubmit()">
                <div class="mb-3">
                  <label for="name" class="form-label">Nombre</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="name" 
                    formControlName="name"
                    [ngClass]="{'is-invalid': methodForm.get('name')?.invalid && methodForm.get('name')?.touched}"
                    placeholder="Ej: CalculateTotal"
                  >
                  <div class="invalid-feedback" *ngIf="methodForm.get('name')?.invalid && methodForm.get('name')?.touched">
                    <span *ngIf="methodForm.get('name')?.errors?.['required']">El nombre es requerido</span>
                    <span *ngIf="methodForm.get('name')?.errors?.['minlength']">El nombre debe tener al menos 2 caracteres</span>
                  </div>
                </div>

                <div class="mb-3">
                  <label for="returnType" class="form-label">Tipo de Retorno</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="returnType" 
                    formControlName="returnType"
                    [ngClass]="{'is-invalid': methodForm.get('returnType')?.invalid && methodForm.get('returnType')?.touched}"
                    placeholder="Ej: int, string, void"
                  >
                  <div class="invalid-feedback" *ngIf="methodForm.get('returnType')?.invalid && methodForm.get('returnType')?.touched">
                    El tipo de retorno es requerido
                  </div>
                </div>

                <div class="mb-3">
                  <label for="classId" class="form-label">Clase</label>
                  <select 
                    class="form-select" 
                    id="classId" 
                    formControlName="classId"
                    [ngClass]="{'is-invalid': methodForm.get('classId')?.invalid && methodForm.get('classId')?.touched}"
                  >
                    <option value="">Seleccione una clase</option>
                    <option *ngFor="let class of classes" [value]="class.id">{{ class.name }}</option>
                  </select>
                  <div class="invalid-feedback" *ngIf="methodForm.get('classId')?.invalid && methodForm.get('classId')?.touched">
                    La clase es requerida
                  </div>
                </div>

                <div class="mb-3">
                  <label class="form-label">Modificadores</label>
                  <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="isVirtual" formControlName="isVirtual">
                    <label class="form-check-label" for="isVirtual">
                      <i class="bi bi-arrow-repeat"></i> Virtual
                    </label>
                  </div>
                  <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="isStatic" formControlName="isStatic">
                    <label class="form-check-label" for="isStatic">
                      <i class="bi bi-layers"></i> Static
                    </label>
                  </div>
                  <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="isOverride" formControlName="isOverride">
                    <label class="form-check-label" for="isOverride">
                      <i class="bi bi-arrow-up-circle"></i> Override
                    </label>
                  </div>
                </div>

                <div class="d-flex gap-2">
                  <button type="submit" class="btn btn-primary" [disabled]="methodForm.invalid || loading">
                    <i class="bi" [ngClass]="isEditMode ? 'bi-pencil' : 'bi-plus-circle'"></i> 
                    {{ isEditMode ? 'Actualizar Método' : 'Crear Método' }}
                  </button>
                  <button type="button" class="btn btn-secondary" (click)="router.navigate(['/methods'])">
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
    
    .form-check {
      margin-bottom: 0.5rem;
    }
    
    .form-check-label {
      cursor: pointer;
    }
  `]
})
export class MethodFormComponent implements OnInit {
  methodForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  classes: ClassModel[] = [];
  isEditMode = false;
  methodId: string | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private methodService: MethodService,
    public router: Router,
    private classService: ClassService,
    private route: ActivatedRoute
  ) {
    this.methodForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      returnType: ['void', [Validators.required]],
      classId: ['', [Validators.required]],
      isVirtual: [false],
      isStatic: [false],
      isOverride: [false]
    });
  }

  ngOnInit(): void {
    this.loadClasses();
    this.methodId = this.route.snapshot.paramMap.get('id');
    if (this.methodId) {
      this.isEditMode = true;
      this.loadMethod();
    }
  }

  loadClasses(): void {
    this.classService.getClasses().subscribe({
      next: (data: any) => {
        const classes = data.$values || data;
        this.classes = classes;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading classes:', error);
        this.errorMessage = 'Error al cargar las clases.';
      }
    });
  }

  loadMethod(): void {
    if (!this.methodId) return;
    
    this.methodService.getMethod(this.methodId).subscribe({
      next: (data: MethodModel) => {
        this.methodForm.patchValue({
          name: data.name,
          returnType: data.returnType || 'void',
          classId: data.classId,
          isVirtual: data.isVirtual || false,
          isStatic: data.isStatic || false,
          isOverride: data.isOverride || false
        });
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading method:', error);
        this.errorMessage = 'Error al cargar el método.';
      }
    });
  }

  onSubmit(): void {
    if (this.methodForm.valid) {
      this.loading = true;
      this.errorMessage = null;
      this.successMessage = null;

      const methodData: MethodModel = {
        name: this.methodForm.value.name,
        returnType: this.methodForm.value.returnType,
        classId: String(this.methodForm.value.classId),
        isVirtual: this.methodForm.value.isVirtual,
        isStatic: this.methodForm.value.isStatic,
        isOverride: this.methodForm.value.isOverride
      };

      if (this.isEditMode && this.methodId) {
        this.methodService.updateMethod(this.methodId, methodData).subscribe({
          next: () => {
            this.successMessage = '¡Método actualizado exitosamente!';
            this.loading = false;
            setTimeout(() => this.router.navigate(['/methods']), 2000);
          },
          error: (error: HttpErrorResponse) => {
            console.error('Error updating method:', error);
            this.errorMessage = this.getErrorMessage(error);
            this.loading = false;
          }
        });
      } else {
        this.methodService.createMethod(methodData).subscribe({
          next: () => {
            this.successMessage = '¡Método creado exitosamente!';
            this.loading = false;
            setTimeout(() => this.router.navigate(['/methods']), 2000);
          },
          error: (error: HttpErrorResponse) => {
            console.error('Error creating method:', error);
            this.errorMessage = this.getErrorMessage(error);
            this.loading = false;
          }
        });
      }
    }
  }

  private getErrorMessage(error: HttpErrorResponse): string {
    if (error.error?.detail) {
      return error.error.detail;
    }
    if (error.error?.title) {
      return error.error.title;
    }
    return `Error al ${this.isEditMode ? 'actualizar' : 'crear'} el método`;
  }
}
