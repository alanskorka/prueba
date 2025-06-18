import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MethodCallService } from '../../services/method-call.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-method-call-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header">
              <h2 class="mb-0">{{ isEdit ? 'Editar Llamada a Método' : 'Nueva Llamada a Método' }}</h2>
            </div>
            <div class="card-body">
              <div *ngIf="successMessage" class="alert alert-success">
                <i class="bi bi-check-circle"></i> {{ successMessage }}
              </div>
              <div *ngIf="errorMessage" class="alert alert-danger">
                <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
              </div>

              <form [formGroup]="callForm" (ngSubmit)="onSubmit()">
                <div class="mb-3">
                  <label for="sourceMethodId" class="form-label">Método Origen</label>
                  <select class="form-select" id="sourceMethodId" formControlName="sourceMethodId" required>
                    <option value="">Seleccione un método</option>
                    <option *ngFor="let m of methods" [value]="m.id">{{ m.name }}</option>
                  </select>
                </div>
                <div class="mb-3">
                  <label for="targetMethodId" class="form-label">Método Destino</label>
                  <select class="form-select" id="targetMethodId" formControlName="targetMethodId" required>
                    <option value="">Seleccione un método</option>
                    <option *ngFor="let m of methods" [value]="m.id">{{ m.name }}</option>
                  </select>
                </div>
                <div class="mb-3">
                  <label for="callType" class="form-label">Tipo de Llamada</label>
                  <input type="text" class="form-control" id="callType" formControlName="callType" required>
                </div>
                <div class="d-flex gap-2 mt-3">
                  <button type="submit" class="btn btn-primary" [disabled]="callForm.invalid">
                    <i class="bi bi-plus-circle"></i> {{ isEdit ? 'Actualizar' : 'Crear' }} Llamada
                  </button>
                  <button type="button" class="btn btn-secondary" (click)="router.navigate(['/method-calls'])">
                    <i class="bi bi-x-circle"></i> Cancelar
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class MethodCallFormComponent implements OnInit {
  callForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  methods: MethodModel[] = [];
  isEdit = false;
  callId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private methodCallService: MethodCallService,
    public router: Router,
    private methodService: MethodService,
    private route: ActivatedRoute
  ) {
    this.callForm = this.fb.group({
      sourceMethodId: ['', Validators.required],
      targetMethodId: ['', Validators.required],
      callType: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.methodService.getMethods().subscribe({
      next: (data) => this.methods = data,
      error: () => this.errorMessage = 'Error al cargar los métodos.'
    });
    this.callId = this.route.snapshot.paramMap.get('id');
    if (this.callId) {
      this.isEdit = true;
      this.methodCallService.getMethodCall(this.callId).subscribe({
        next: (data) => this.callForm.patchValue(data),
        error: () => this.errorMessage = 'Error al cargar la llamada.'
      });
    }
  }

  onSubmit(): void {
    if (this.callForm.valid) {
      const callData = this.callForm.value;
      if (this.isEdit && this.callId) {
        this.methodCallService.updateMethodCall(this.callId, callData).subscribe({
          next: () => {
            this.successMessage = '¡Llamada actualizada exitosamente!';
            this.errorMessage = null;
            this.router.navigate(['/method-calls']);
          },
          error: (error: Error) => {
            this.errorMessage = `Error al actualizar la llamada: ${error.message}`;
            this.successMessage = null;
          }
        });
      } else {
        this.methodCallService.createMethodCall(callData).subscribe({
          next: () => {
            this.successMessage = '¡Llamada creada exitosamente!';
            this.errorMessage = null;
            this.router.navigate(['/method-calls']);
          },
          error: (error: Error) => {
            this.errorMessage = `Error al crear la llamada: ${error.message}`;
            this.successMessage = null;
          }
        });
      }
    }
  }
}
