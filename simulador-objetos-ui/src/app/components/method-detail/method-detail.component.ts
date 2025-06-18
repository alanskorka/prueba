import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MethodService, MethodModel } from '../../services/method.service';
import { ClassService, ClassModel } from '../../services/class.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-method-detail',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header d-flex justify-content-between align-items-center">
              <h2 class="mb-0">Detalle del Método</h2>
              <button class="btn btn-secondary btn-sm" (click)="router.navigate(['/methods'])">
                <i class="bi bi-arrow-left"></i> Volver
              </button>
            </div>
            <div class="card-body" *ngIf="method">
              <ul class="list-group mb-3">
                <li class="list-group-item"><strong>ID:</strong> {{ method.id }}</li>
                <li class="list-group-item"><strong>Nombre:</strong> {{ method.name }}</li>
                <li class="list-group-item"><strong>Tipo de Retorno:</strong> {{ method.returnType }}</li>
                <li class="list-group-item"><strong>Clase:</strong> {{ getClassName(method.classId) }}</li>
                <li class="list-group-item"><strong>Virtual:</strong> {{ method.isVirtual ? 'Sí' : 'No' }}</li>
                <li class="list-group-item"><strong>Static:</strong> {{ method.isStatic ? 'Sí' : 'No' }}</li>
                <li class="list-group-item"><strong>Override:</strong> {{ method.isOverride ? 'Sí' : 'No' }}</li>
              </ul>
              <button class="btn btn-warning me-2" (click)="router.navigate(['/methods', method.id, 'edit'])">
                <i class="bi bi-pencil"></i> Editar
              </button>
              <button class="btn btn-secondary" (click)="router.navigate(['/methods'])">
                <i class="bi bi-x-circle"></i> Volver a la lista
              </button>
            </div>
            <div class="card-body" *ngIf="!method">
              <div class="alert alert-danger">No se encontró el método.</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class MethodDetailComponent implements OnInit {
  method: MethodModel | null = null;
  classes: ClassModel[] = [];

  constructor(
    private route: ActivatedRoute,
    private methodService: MethodService,
    private classService: ClassService,
    public router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.methodService.getMethod(id).subscribe({
        next: (data) => this.method = data,
        error: () => this.method = null
      });
      this.classService.getClasses().subscribe({
        next: (data) => this.classes = data
      });
    }
  }

  getClassName(classId: string): string {
    return this.classes.find(c => c.id === classId)?.name || '';
  }
}
