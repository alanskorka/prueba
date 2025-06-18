import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MethodCallService } from '../../services/method-call.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { MethodCallModel } from '../../models/method-call.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-method-call-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './method-call-detail.component.html',
  styleUrl: './method-call-detail.component.scss'
})
export class MethodCallDetailComponent implements OnInit {
  call: MethodCallModel | null = null;
  methods: MethodModel[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private methodCallService: MethodCallService,
    private methodService: MethodService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loading = true;
      this.methodCallService.getMethodCall(id).subscribe({
        next: (data) => {
          this.call = data;
          this.loading = false;
        },
        error: () => {
          this.errorMessage = 'Error al cargar la llamada.';
          this.loading = false;
        }
      });
      this.methodService.getMethods().subscribe({
        next: (data) => this.methods = data,
        error: () => this.errorMessage = 'Error al cargar los métodos.'
      });
    }
  }

  getMethodName(methodId?: string): string {
    if (!methodId) return 'N/A';
    return this.methods.find(m => m.id === methodId)?.name || 'Método no encontrado';
  }

  goBack(): void {
    this.router.navigate(['/method-calls']);
  }
}
