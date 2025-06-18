import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MethodCallService } from '../../services/method-call.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { MethodCallModel } from '../../models/method-call.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-method-call-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './method-call-form.component.html'
})
export class MethodCallFormComponent implements OnInit {
  callForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  methods: MethodModel[] = [];
  loading = false;
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
      methodName: ['', Validators.required],
      referenceType: ['This', Validators.required],
      referenceName: [''],
      parentMethodId: ['', Validators.required],
      concreteParameterTypes: [[]]
    });
  }

  ngOnInit(): void {
    console.log('MethodCallFormComponent: ngOnInit started');
    this.loading = true;
    this.loadMethods();
    this.checkEditMode();
  }

  loadMethods(): void {
    console.log('Loading methods...');
    this.methodService.getMethods().subscribe({
      next: (data) => {
        console.log('Methods loaded successfully:', data);
        console.log('Methods array length:', data.length);
        console.log('Methods array:', JSON.stringify(data));
        this.methods = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading methods:', error);
        console.error('Error details:', JSON.stringify(error));
        this.errorMessage = 'Error al cargar los métodos.';
        this.loading = false;
      }
    });
  }

  checkEditMode(): void {
    this.callId = this.route.snapshot.paramMap.get('id');
    if (this.callId) {
      console.log('Edit mode detected, loading method call with ID:', this.callId);
      this.isEdit = true;
      this.loadMethodCall();
    }
  }

  loadMethodCall(): void {
    if (!this.callId) return;
    
    this.methodCallService.getMethodCall(this.callId).subscribe({
      next: (data: MethodCallModel) => {
        console.log('Method call loaded for edit:', data);
        this.callForm.patchValue(data);
      },
      error: (error) => {
        console.error('Error loading method call:', error);
        this.errorMessage = 'Error al cargar la llamada.';
      }
    });
  }

  onSubmit(): void {
    console.log('Form submitted, valid:', this.callForm.valid);
    console.log('Form values:', this.callForm.value);
    
    if (this.callForm.valid) {
      const callData: MethodCallModel = this.callForm.value;
      console.log('Submitting call data:', callData);
      
      if (this.isEdit && this.callId) {
        this.methodCallService.updateMethodCall(this.callId, callData).subscribe({
          next: () => {
            this.successMessage = '¡Llamada actualizada exitosamente!';
            this.errorMessage = null;
            setTimeout(() => this.router.navigate(['/method-calls']), 1500);
          },
          error: (error: Error) => {
            console.error('Error updating method call:', error);
            this.errorMessage = `Error al actualizar la llamada: ${error.message}`;
            this.successMessage = null;
          }
        });
      } else {
        this.methodCallService.createMethodCall(callData).subscribe({
          next: () => {
            this.successMessage = '¡Llamada creada exitosamente!';
            this.errorMessage = null;
            setTimeout(() => this.router.navigate(['/method-calls']), 1500);
          },
          error: (error: Error) => {
            console.error('Error creating method call:', error);
            this.errorMessage = `Error al crear la llamada: ${error.message}`;
            this.successMessage = null;
          }
        });
      }
    } else {
      console.log('Form is invalid');
      this.markFormGroupTouched();
    }
  }

  markFormGroupTouched(): void {
    Object.keys(this.callForm.controls).forEach(key => {
      const control = this.callForm.get(key);
      control?.markAsTouched();
    });
  }
}
