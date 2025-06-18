import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MethodCallService } from '../../services/method-call.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { MethodCallModel } from '../../models/method-call.model';

@Component({
  selector: 'app-method-call-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './method-call-list.component.html',
  styleUrls: ['./method-call-list.component.scss']
})
export class MethodCallListComponent implements OnInit {
  methodCalls: MethodCallModel[] = [];
  filteredMethodCalls: MethodCallModel[] = [];
  methods: MethodModel[] = [];
  loading = false;
  errorMessage: string | null = null;
  searchTerm: string = '';

  constructor(private methodCallService: MethodCallService, private methodService: MethodService) {}

  ngOnInit(): void {
    this.loadMethodCalls();
    this.loadMethods();
  }

  loadMethods(): void {
    this.methodService.getMethods().subscribe({
      next: (data) => this.methods = data,
      error: () => this.errorMessage = 'Error al cargar los métodos.'
    });
  }

  loadMethodCalls(): void {
    this.loading = true;
    this.errorMessage = null;
    this.methodCallService.getMethodCalls().subscribe({
      next: (data) => {
        this.methodCalls = data;
        this.filteredMethodCalls = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar las llamadas a métodos';
        this.loading = false;
      }
    });
  }

  getMethodName(methodId?: string): string {
    if (!methodId) return 'N/A';
    return this.methods.find(m => m.id === methodId)?.name || 'Método no encontrado';
  }

  filterMethodCalls(): void {
    if (!this.searchTerm.trim()) {
      this.filteredMethodCalls = this.methodCalls;
      return;
    }

    const search = this.searchTerm.toLowerCase();
    this.filteredMethodCalls = this.methodCalls.filter(call => 
      call.methodName?.toLowerCase().includes(search) ||
      call.referenceType?.toLowerCase().includes(search) ||
      call.referenceName?.toLowerCase().includes(search) ||
      this.getMethodName(call.parentMethodId).toLowerCase().includes(search)
    );
  }

  deleteMethodCall(call: MethodCallModel): void {
    if (confirm(`¿Seguro que deseas eliminar la llamada "${call.methodName}"?`)) {
      this.loading = true;
      this.methodCallService.deleteMethodCall(call.id!).subscribe({
        next: () => {
          this.loadMethodCalls();
        },
        error: () => {
          this.errorMessage = 'Error al eliminar la llamada';
          this.loading = false;
        }
      });
    }
  }
}
