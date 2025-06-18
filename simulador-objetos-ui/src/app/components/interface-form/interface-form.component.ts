import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { InterfaceService } from '../../services/interface.service';
import { CommonModule } from '@angular/common';
import { InterfaceModel } from '../../models/interface.model';

@Component({
  selector: 'app-interface-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header">
              <h2 class="mb-0">{{ isEditMode ? 'Editar Interface' : 'Crear Nueva Interface' }}</h2>
            </div>
            <div class="card-body">
              <div *ngIf="successMessage" class="alert alert-success">
                <i class="bi bi-check-circle"></i> {{ successMessage }}
              </div>
              <div *ngIf="errorMessage" class="alert alert-danger">
                <i class="bi bi-exclamation-triangle"></i> {{ errorMessage }}
              </div>

              <form [formGroup]="interfaceForm" (ngSubmit)="onSubmit()">
                <div class="mb-3">
                  <label for="name" class="form-label">Nombre</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="name" 
                    formControlName="name"
                    [ngClass]="{'is-invalid': interfaceForm.get('name')?.invalid && interfaceForm.get('name')?.touched}"
                  >
                  <div class="invalid-feedback" *ngIf="interfaceForm.get('name')?.invalid && interfaceForm.get('name')?.touched">
                    El nombre es requerido
                  </div>
                </div>

                <div class="d-flex gap-2">
                  <button type="submit" class="btn btn-primary" [disabled]="interfaceForm.invalid || loading">
                    <span *ngIf="!loading">{{ isEditMode ? 'Actualizar' : 'Crear' }}</span>
                    <span *ngIf="loading" class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                  </button>
                  <button type="button" class="btn btn-secondary" (click)="router.navigate(['/interfaces'])" [disabled]="loading">
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
export class InterfaceFormComponent implements OnInit {
  interfaceForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  isEditMode = false;
  interfaceId: number | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private interfaceService: InterfaceService,
    public router: Router,
    private route: ActivatedRoute
  ) {
    this.interfaceForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  ngOnInit(): void {
    this.interfaceId = this.route.snapshot.params['id'];
    if (this.interfaceId) {
      this.isEditMode = true;
      this.loadInterfaceData();
    }
  }

  loadInterfaceData(): void {
    if (!this.interfaceId) return;
    this.loading = true;
    this.interfaceService.getInterface(this.interfaceId).subscribe({
      next: (interfaceData) => {
        this.interfaceForm.patchValue(interfaceData);
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Error al cargar la interface.';
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.interfaceForm.invalid) {
      return;
    }

    this.loading = true;
    this.successMessage = null;
    this.errorMessage = null;

    const interfaceData: InterfaceModel = {
      ...this.interfaceForm.value,
      id: this.interfaceId || 0
    };

    if (this.isEditMode) {
      this.interfaceService.updateInterface(interfaceData).subscribe({
        next: () => {
          this.successMessage = '¡Interface actualizada exitosamente!';
          this.loading = false;
          setTimeout(() => this.router.navigate(['/interfaces']), 2000);
        },
        error: (error: Error) => {
          this.errorMessage = `Error al actualizar la interface: ${error.message}`;
          this.loading = false;
        }
      });
    } else {
      this.interfaceService.createInterface(interfaceData).subscribe({
        next: () => {
          this.successMessage = '¡Interface creada exitosamente!';
          this.loading = false;
          setTimeout(() => this.router.navigate(['/interfaces']), 2000);
        },
        error: (error: Error) => {
          this.errorMessage = `Error al crear la interface: ${error.message}`;
          this.loading = false;
        }
      });
    }
  }
}
