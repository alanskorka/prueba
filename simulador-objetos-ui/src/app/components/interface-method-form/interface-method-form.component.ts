import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { InterfaceMethodService } from '../../services/interface-method.service';
import { InterfaceService } from '../../services/interface.service';
import { InterfaceMethodModel } from '../../models/interface-method.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-interface-method-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './interface-method-form.component.html',
  styleUrls: ['./interface-method-form.component.scss']
})
export class InterfaceMethodFormComponent implements OnInit {
  methodForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  interfaces: any[] = [];
  loading = false;
  isEdit = false;
  methodId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private interfaceMethodService: InterfaceMethodService,
    public router: Router,
    private interfaceService: InterfaceService,
    private route: ActivatedRoute
  ) {
    this.methodForm = this.fb.group({
      name: ['', Validators.required],
      returnType: ['', Validators.required],
      interfaceId: ['', Validators.required],
      parameters: [[]]
    });
  }

  ngOnInit(): void {
    this.loading = true;
    this.loadInterfaces();
    this.checkEditMode();
  }

  loadInterfaces(): void {
    this.interfaceService.getInterfaces().subscribe({
      next: (data) => {
        this.interfaces = data;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar las interfaces.';
        this.loading = false;
      }
    });
  }

  checkEditMode(): void {
    this.methodId = this.route.snapshot.paramMap.get('id');
    if (this.methodId) {
      this.isEdit = true;
      this.loadInterfaceMethod();
    }
  }

  loadInterfaceMethod(): void {
    if (!this.methodId) return;
    
    this.interfaceMethodService.getInterfaceMethod(this.methodId).subscribe({
      next: (data: InterfaceMethodModel) => {
        this.methodForm.patchValue(data);
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar el método.';
      }
    });
  }

  onSubmit(): void {
    if (this.methodForm.valid) {
      const methodData: InterfaceMethodModel = this.methodForm.value;
      
      if (this.isEdit && this.methodId) {
        this.interfaceMethodService.updateInterfaceMethod(this.methodId, methodData).subscribe({
          next: () => {
            this.successMessage = '¡Método actualizado exitosamente!';
            this.errorMessage = null;
            setTimeout(() => this.router.navigate(['/interface-methods']), 1500);
          },
          error: (error: Error) => {
            this.errorMessage = `Error al actualizar el método: ${error.message}`;
            this.successMessage = null;
          }
        });
      } else {
        this.interfaceMethodService.createInterfaceMethod(methodData).subscribe({
          next: () => {
            this.successMessage = '¡Método creado exitosamente!';
            this.errorMessage = null;
            setTimeout(() => this.router.navigate(['/interface-methods']), 1500);
          },
          error: (error: Error) => {
            this.errorMessage = `Error al crear el método: ${error.message}`;
            this.successMessage = null;
          }
        });
      }
    } else {
      this.markFormGroupTouched();
    }
  }

  markFormGroupTouched(): void {
    Object.keys(this.methodForm.controls).forEach(key => {
      const control = this.methodForm.get(key);
      control?.markAsTouched();
    });
  }
}
