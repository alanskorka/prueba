import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ClassService } from '../../services/class.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-class-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './class-form.component.html',
  styleUrls: ['./class-form.component.scss']
})
export class ClassFormComponent {
  classForm: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private classService: ClassService,
    private router: Router
  ) {
    this.classForm = this.fb.group({
      name: ['', Validators.required],
      isAbstract: [false],
      isSealed: [false]
    });
  }

  onSubmit(): void {
    if (this.classForm.valid) {
      this.classService.createClass(this.classForm.value).subscribe({
        next: () => {
          this.successMessage = '¡Clase creada exitosamente!';
          this.errorMessage = null;
          setTimeout(() => this.router.navigate(['/classes']), 2000);
        },
        error: (err) => {
          this.errorMessage = `Error al crear la clase: ${err.message}`;
          this.successMessage = null;
        }
      });
    }
  }
}
