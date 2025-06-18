import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ParamService, ParamModel } from '../../services/param.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { CommonModule } from '@angular/common';

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
              <h2 class="mb-0">Crear Nuevo Parámetro</h2>
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
                  >
                  <div class="invalid-feedback" *ngIf="paramForm.get('name')?.invalid && paramForm.get('name')?.touched">
                    El nombre es requerido
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
                    <option *ngFor="let method of methods" [value]="method.id">{{ method.name }}</option>
                  </select>
                  <div class="invalid-feedback" *ngIf="paramForm.get('methodId')?.invalid && paramForm.get('methodId')?.touched">
                    El método es requerido
                  </div>
                </div>

                <div class="d-flex gap-2">
                  <button type="submit" class="btn btn-primary" [disabled]="paramForm.invalid">
                    <i class="bi bi-plus-circle"></i> Crear Parámetro
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
    .card { box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075); }
    .card-header { background-color: #f8f9fa; border-bottom: 1px solid rgba(0, 0, 0, 0.125); }
    .form-label { font-weight: 500; }
  `]
})
export class ParamFormComponent implements OnInit {
  paramForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  methods: MethodModel[] = [];
  isEdit = false;
  paramId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private paramService: ParamService,
    public router: Router,
    private methodService: MethodService,
    private route: ActivatedRoute
  ) {
    this.paramForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      type: ['', [Validators.required]],
      methodId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.methodService.getMethods().subscribe({
      next: (data) => this.methods = data,
      error: () => this.errorMessage = 'Error al cargar los métodos.'
    });
    this.paramId = this.route.snapshot.paramMap.get('id');
    if (this.paramId) {
      this.isEdit = true;
      this.paramService.getParam(this.paramId).subscribe({
        next: (data) => this.paramForm.patchValue(data),
        error: () => this.errorMessage = 'Error al cargar el parámetro.'
      });
    }
  }

  onSubmit(): void {
    if (this.paramForm.valid) {
      const paramData: ParamModel = {
        name: this.paramForm.value.name,
        type: this.paramForm.value.type,
        methodId: String(this.paramForm.value.methodId)
      };
      if (this.isEdit && this.paramId) {
        this.paramService.updateParam(this.paramId, paramData).subscribe({
          next: () => {
            this.successMessage = '¡Parámetro actualizado exitosamente!';
            this.errorMessage = null;
            this.router.navigate(['/params']);
          },
          error: (error: Error) => {
            this.errorMessage = `Error al actualizar el parámetro: ${error.message}`;
            this.successMessage = null;
          }
        });
      } else {
        this.paramService.createParam(paramData).subscribe({
          next: () => {
            this.successMessage = '¡Parámetro creado exitosamente!';
            this.errorMessage = null;
            this.router.navigate(['/params']);
          },
          error: (error: Error) => {
            this.errorMessage = `Error al crear el parámetro: ${error.message}`;
            this.successMessage = null;
          }
        });
      }
    }
  }
}
