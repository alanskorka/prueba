import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ParamService, ParamModel } from '../../services/param.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-param-detail',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container mt-4">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card">
            <div class="card-header d-flex justify-content-between align-items-center">
              <h2 class="mb-0">Detalle del Parámetro</h2>
              <button class="btn btn-secondary btn-sm" (click)="router.navigate(['/params'])">
                <i class="bi bi-arrow-left"></i> Volver
              </button>
            </div>
            <div class="card-body" *ngIf="param">
              <ul class="list-group mb-3">
                <li class="list-group-item"><strong>ID:</strong> {{ param.id }}</li>
                <li class="list-group-item"><strong>Nombre:</strong> {{ param.name }}</li>
                <li class="list-group-item"><strong>Tipo:</strong> {{ param.type }}</li>
                <li class="list-group-item"><strong>Método:</strong> {{ getMethodName(param.methodId) }}</li>
              </ul>
              <button class="btn btn-warning me-2" (click)="router.navigate(['/params', param.id, 'edit'])">
                <i class="bi bi-pencil"></i> Editar
              </button>
              <button class="btn btn-secondary" (click)="router.navigate(['/params'])">
                <i class="bi bi-x-circle"></i> Volver a la lista
              </button>
            </div>
            <div class="card-body" *ngIf="!param">
              <div class="alert alert-danger">No se encontró el parámetro.</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ParamDetailComponent implements OnInit {
  param: ParamModel | null = null;
  methods: MethodModel[] = [];

  constructor(
    private route: ActivatedRoute,
    private paramService: ParamService,
    private methodService: MethodService,
    public router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.paramService.getParam(id).pipe(takeUntil(this.destroy$)).subscribe({
        next: (data) => this.param = data,
        error: () => this.param = null
      });
      this.methodService.getMethods().pipe(takeUntil(this.destroy$)).subscribe({
        next: (data) => this.methods = data
      });
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  getMethodName(methodId: string): string {
    return this.methods.find(m => m.id === methodId)?.name || '';
  }
}
