import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalVarService } from '../../services/local-var.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { LocalVarModel } from '../../models/local-var.model';

@Component({
  selector: 'app-local-var-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './local-var-detail.component.html',
  styleUrls: ['./local-var-detail.component.scss']
})
export class LocalVarDetailComponent implements OnInit {
  localVar: LocalVarModel | null = null;
  methods: MethodModel[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(
    private localVarService: LocalVarService,
    private methodService: MethodService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadMethods();
    this.loadLocalVar();
  }

  loadMethods(): void {
    this.methodService.getMethods().subscribe({
      next: (data) => this.methods = data,
      error: () => this.errorMessage = 'Error al cargar los métodos.'
    });
  }

  loadLocalVar(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.errorMessage = 'ID de variable local no válido';
      return;
    }

    this.loading = true;
    this.localVarService.getLocalVar(id).subscribe({
      next: (data) => {
        this.localVar = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading local var:', error);
        this.errorMessage = 'Error al cargar la variable local';
        this.loading = false;
      }
    });
  }

  getMethodName(methodId?: string): string {
    if (!methodId) return 'N/A';
    return this.methods.find(m => m.id === methodId)?.name || 'Método no encontrado';
  }

  deleteLocalVar(): void {
    if (!this.localVar?.id) return;

    if (confirm(`¿Seguro que deseas eliminar la variable local "${this.localVar.name}"?`)) {
      this.loading = true;
      this.localVarService.deleteLocalVar(this.localVar.id).subscribe({
        next: () => {
          this.router.navigate(['/local-vars']);
        },
        error: (error) => {
          console.error('Error deleting local var:', error);
          this.errorMessage = 'Error al eliminar la variable local';
          this.loading = false;
        }
      });
    }
  }
}
