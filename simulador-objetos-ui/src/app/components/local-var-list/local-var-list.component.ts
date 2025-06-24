import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { LocalVarService } from '../../services/local-var.service';
import { MethodService, MethodModel } from '../../services/method.service';
import { LocalVarModel } from '../../models/local-var.model';

@Component({
  selector: 'app-local-var-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './local-var-list.component.html',
  styleUrls: ['./local-var-list.component.scss']
})
export class LocalVarListComponent implements OnInit {
  localVars: LocalVarModel[] = [];
  filteredLocalVars: LocalVarModel[] = [];
  methods: MethodModel[] = [];
  loading = false;
  errorMessage: string | null = null;
  searchTerm: string = '';
  methodIdToNameMap: { [key: string]: string } = {};

  constructor(private localVarService: LocalVarService, private methodService: MethodService) {}

  ngOnInit(): void {
    this.loadLocalVars();
    this.loadMethods();
  }

  loadMethods(): void {
    this.methodService.getMethods().subscribe({
      next: (data) => {
        this.methods = data;
        this.methodIdToNameMap = {};
        data.forEach(method => {
          if (method.id) {
            this.methodIdToNameMap[method.id] = method.name;
          }
        });
      },
      error: () => this.errorMessage = 'Error al cargar los métodos.'
    });
  }

  loadLocalVars(): void {
    this.loading = true;
    this.errorMessage = null;
    this.localVarService.getLocalVars().subscribe({
      next: (data) => {
        this.localVars = data;
        this.filteredLocalVars = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar las variables locales';
        this.loading = false;
      }
    });
  }

  getMethodName(methodId?: string): string {
    if (!methodId) return 'N/A';
    return this.methods.find(m => m.id === methodId)?.name || 'Método no encontrado';
  }

  filterLocalVars(): void {
    if (!this.searchTerm.trim()) {
      this.filteredLocalVars = this.localVars;
      return;
    }

    const search = this.searchTerm.toLowerCase();
    this.filteredLocalVars = this.localVars.filter(localVar => 
      localVar.name?.toLowerCase().includes(search) ||
      localVar.type?.toLowerCase().includes(search) ||
      (this.methodIdToNameMap[localVar.methodId]?.toLowerCase() || 'método no encontrado').includes(search)
    );
  }

  deleteLocalVar(localVar: LocalVarModel): void {
    if (!localVar.id) return;

    if (confirm(`¿Seguro que deseas eliminar la variable local "${localVar.name}"?`)) {
      this.loading = true;
      this.localVarService.deleteLocalVar(localVar.id).subscribe({
        next: () => {
          this.loadLocalVars();
        },
        error: () => {
          this.errorMessage = 'Error al eliminar la variable local';
          this.loading = false;
        }
      });
    }
  }
}
