import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InterfaceMethodService } from '../../services/interface-method.service';
import { InterfaceService } from '../../services/interface.service';
import { InterfaceMethodModel } from '../../models/interface-method.model';

@Component({
  selector: 'app-interface-method-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './interface-method-list.component.html',
  styleUrls: ['./interface-method-list.component.scss']
})
export class InterfaceMethodListComponent implements OnInit {
  interfaceMethods: InterfaceMethodModel[] = [];
  filteredInterfaceMethods: InterfaceMethodModel[] = [];
  interfaces: any[] = [];
  loading = false;
  errorMessage: string | null = null;
  searchTerm: string = '';

  constructor(private interfaceMethodService: InterfaceMethodService, private interfaceService: InterfaceService) {}

  ngOnInit(): void {
    this.loadInterfaceMethods();
    this.loadInterfaces();
  }

  loadInterfaces(): void {
    this.interfaceService.getInterfaces().subscribe({
      next: (data) => this.interfaces = data,
      error: () => this.errorMessage = 'Error al cargar las interfaces.'
    });
  }

  loadInterfaceMethods(): void {
    this.loading = true;
    this.errorMessage = null;
    this.interfaceMethodService.getInterfaceMethods().subscribe({
      next: (data) => {
        this.interfaceMethods = data;
        this.filteredInterfaceMethods = data;
        this.loading = false;
      },
      error: (error: Error) => {
        this.errorMessage = 'Error al cargar los métodos de interfaz';
        this.loading = false;
      }
    });
  }

  getInterfaceName(interfaceId?: string): string {
    if (!interfaceId) return 'N/A';
    return this.interfaces.find(i => i.id === interfaceId)?.name || 'Interfaz no encontrada';
  }

  filterInterfaceMethods(): void {
    if (!this.searchTerm.trim()) {
      this.filteredInterfaceMethods = this.interfaceMethods;
      return;
    }

    const search = this.searchTerm.toLowerCase();
    this.filteredInterfaceMethods = this.interfaceMethods.filter(method => 
      method.name?.toLowerCase().includes(search) ||
      method.returnType?.toLowerCase().includes(search) ||
      this.getInterfaceName(method.interfaceId).toLowerCase().includes(search)
    );
  }

  deleteInterfaceMethod(method: InterfaceMethodModel): void {
    if (confirm(`¿Seguro que deseas eliminar el método "${method.name}"?`)) {
      this.loading = true;
      this.interfaceMethodService.deleteInterfaceMethod(method.id!).subscribe({
        next: () => {
          this.loadInterfaceMethods();
        },
        error: () => {
          this.errorMessage = 'Error al eliminar el método de interfaz';
          this.loading = false;
        }
      });
    }
  }
}
