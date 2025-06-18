import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ParamService, ParamModel } from '../../services/param.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-param-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header">
              <h2 class="mb-0">{{ isEditMode ? 'Editar Parámetro' : 'Crear Nuevo Parámetro' }}</h2>
            </div>
            <div class="card-body">
              <div *ngIf="successMessage" class="alert alert-success">
                <i class="bi bi-check-circle"></i> {{ successMessage }}
              </div>
              <div *ngIf="errorMessage" class="alert alert-danger">
                <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
              </div>

              <form [formGroup]="paramForm" (ngSubmit)="onSubmit()">
                <div class="mb-3">
                  <label for="name" class="form-label">Nombre</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="name" 
                    formControlName="name"
                    [ngClass]="{'is-invalid': paramForm.get('name')?.invalid && paramForm.get('name')?.touched}"
                    placeholder="Ej: amount, customer, items"
                  >
                  <div class="invalid-feedback" *ngIf="paramForm.get('name')?.invalid && paramForm.get('name')?.touched">
                    <span *ngIf="paramForm.get('name')?.errors?.['required']">El nombre es requerido</span>
                    <span *ngIf="paramForm.get('name')?.errors?.['minlength']">El nombre debe tener al menos 2 caracteres</span>
                  </div>
                </div>

                <div class="mb-3">
                  <label for="type" class="form-label">Tipo</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="type" 
                    formControlName="type"
                    [ngClass]="{'is-invalid': paramForm.get('type')?.invalid && paramForm.get('type')?.touched}"
                    placeholder="Ej: int, string, Customer, List&lt;Product&gt;"
                  >
                  <div class="invalid-feedback" *ngIf="paramForm.get('type')?.invalid && paramForm.get('type')?.touched">
                    El tipo es requerido
                  </div>
                </div>

                <div class="mb-3">
                  <label for="methodId" class="form-label">Método</label>
                  <select 
                    class="form-select" 
                    id="methodId" 
                    formControlName="methodId"
                    [ngClass]="{'is-invalid': paramForm.get('methodId')?.invalid && paramForm.get('methodId')?.touched}"
                  >
                    <option value="">Seleccione un método</option>
                    <option *ngFor="let method of methods" [value]="method.id">
                      {{ method.name }} ({{ method.returnType || 'void' }})
                    </option>
                  </select>
                  <div class="invalid-feedback" *ngIf="paramForm.get('methodId')?.invalid && paramForm.get('methodId')?.touched">
                    El método es requerido
                  </div>
                </div>

                <div class="d-flex gap-2">
                  <button type="submit" class="btn btn-primary" [disabled]="paramForm.invalid || loading">
                    <i class="bi" [ngClass]="isEditMode ? 'bi-pencil' : 'bi-plus-circle'"></i> 
                    {{ isEditMode ? 'Actualizar Parámetro' : 'Crear Parámetro' }}
                  </button>
                  <button type="button" class="btn btn-secondary" (click)="router.navigate(['/params'])">
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
export class ParamFormComponent implements OnInit {
  paramForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  methods: MethodModel[] = [];
  isEditMode = false;
  paramId: string | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private paramService: ParamService,
    public router: Router,
    private methodService: MethodService,
    private route: ActivatedRoute
  ) {
    this.paramForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      type: ['object', [Validators.required]],
      methodId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loadMethods();
    this.paramId = this.route.snapshot.paramMap.get('id');
    if (this.paramId) {
      this.isEditMode = true;
      this.loadParam();
    }
  }

  loadMethods(): void {
    this.methodService.getMethods().subscribe({
      next: (data: any) => {
        const methods = data.$values || data;
        this.methods = methods;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading methods:', error);
        this.errorMessage = 'Error al cargar los métodos.';
      }
    });
  }

  loadParam(): void {
    if (!this.paramId) return;
    
    this.paramService.getParam(this.paramId).subscribe({
      next: (data: ParamModel) => {
        this.paramForm.patchValue({
          name: data.name,
          type: data.type || 'object',
          methodId: data.methodId
        });
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading param:', error);
        this.errorMessage = 'Error al cargar el parámetro.';
      }
    });
  }

  onSubmit(): void {
    if (this.paramForm.valid) {
      this.loading = true;
      this.errorMessage = null;
      this.successMessage = null;

      const paramData: ParamModel = {
        name: this.paramForm.value.name,
        type: this.paramForm.value.type,
        methodId: String(this.paramForm.value.methodId)
      };

      if (this.isEditMode && this.paramId) {
        this.paramService.updateParam(this.paramId, paramData).subscribe({
          next: () => {
            this.successMessage = '¡Parámetro actualizado exitosamente!';
            this.loading = false;
            setTimeout(() => this.router.navigate(['/params']), 2000);
          },
          error: (error: HttpErrorResponse) => {
            console.error('Error updating param:', error);
            this.errorMessage = this.getErrorMessage(error);
            this.loading = false;
          }
        });
      } else {
        this.paramService.createParam(paramData).subscribe({
          next: () => {
            this.successMessage = '¡Parámetro creado exitosamente!';
            this.loading = false;
            setTimeout(() => this.router.navigate(['/params']), 2000);
          },
          error: (error: HttpErrorResponse) => {
            console.error('Error creating param:', error);
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
    return `Error al ${this.isEditMode ? 'actualizar' : 'crear'} el parámetro`;
  }
}
