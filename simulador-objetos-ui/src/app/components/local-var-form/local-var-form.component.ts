import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalVarService } from '../../services/local-var.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { LocalVarModel } from '../../models/local-var.model';

@Component({
  selector: 'app-local-var-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './local-var-form.component.html',
  styleUrls: ['./local-var-form.component.scss']
})
export class LocalVarFormComponent implements OnInit {
  localVarForm: FormGroup;
  methods: MethodModel[] = [];
  loading = false;
  isEdit = false;
  localVarId: string | null = null;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private localVarService: LocalVarService,
    private methodService: MethodService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.localVarForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      type: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      methodId: ['', Validators.required],
      concreteTypeId: ['']
    });
  }

  ngOnInit(): void {
    this.loadMethods();
    this.checkEditMode();
  }

  loadMethods(): void {
    this.methodService.getMethods().subscribe({
      next: (data) => {
        this.methods = data;
        console.log('Methods loaded:', this.methods);
      },
      error: (error) => {
        console.error('Error loading methods:', error);
        this.errorMessage = 'Error al cargar los métodos';
      }
    });
  }

  checkEditMode(): void {
    this.localVarId = this.route.snapshot.paramMap.get('id');
    if (this.localVarId) {
      this.isEdit = true;
      this.loadLocalVar();
    }
  }

  loadLocalVar(): void {
    if (!this.localVarId) return;

    this.loading = true;
    this.localVarService.getLocalVar(this.localVarId).subscribe({
      next: (data) => {
        this.localVarForm.patchValue({
          name: data.name,
          type: data.type,
          methodId: data.methodId,
          concreteTypeId: data.concreteTypeId || ''
        });
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading local var:', error);
        this.errorMessage = 'Error al cargar la variable local';
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.localVarForm.invalid) {
      this.markFormGroupTouched();
      return;
    }

    this.loading = true;
    this.errorMessage = null;
    this.successMessage = null;

    const formData = this.localVarForm.value;
    const localVarData: LocalVarModel = {
      name: formData.name,
      type: formData.type,
      methodId: formData.methodId,
      concreteTypeId: formData.concreteTypeId || undefined
    };

    if (this.isEdit && this.localVarId) {
      this.localVarService.updateLocalVar(this.localVarId, localVarData).subscribe({
        next: () => {
          this.successMessage = 'Variable local actualizada exitosamente';
          this.loading = false;
          setTimeout(() => {
            this.router.navigate(['/local-vars']);
          }, 1500);
        },
        error: (error) => {
          console.error('Error updating local var:', error);
          this.errorMessage = 'Error al actualizar la variable local';
          this.loading = false;
        }
      });
    } else {
      this.localVarService.createLocalVar(localVarData).subscribe({
        next: () => {
          this.successMessage = 'Variable local creada exitosamente';
          this.loading = false;
          setTimeout(() => {
            this.router.navigate(['/local-vars']);
          }, 1500);
        },
        error: (error) => {
          console.error('Error creating local var:', error);
          this.errorMessage = 'Error al crear la variable local';
          this.loading = false;
        }
      });
    }
  }

  private markFormGroupTouched(): void {
    Object.keys(this.localVarForm.controls).forEach(key => {
      const control = this.localVarForm.get(key);
      control?.markAsTouched();
    });
  }

  getErrorMessage(controlName: string): string {
    const control = this.localVarForm.get(controlName);
    if (control?.hasError('required')) {
      return 'Este campo es requerido';
    }
    if (control?.hasError('minlength')) {
      return `Mínimo ${control.errors?.['minlength'].requiredLength} caracteres`;
    }
    if (control?.hasError('maxlength')) {
      return `Máximo ${control.errors?.['maxlength'].requiredLength} caracteres`;
    }
    return '';
  }
}
