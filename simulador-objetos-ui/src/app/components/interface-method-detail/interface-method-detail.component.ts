import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { InterfaceMethodService } from '../../services/interface-method.service';
import { InterfaceService } from '../../services/interface.service';
import { InterfaceMethodModel } from '../../models/interface-method.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-interface-method-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './interface-method-detail.component.html',
  styleUrls: ['./interface-method-detail.component.scss']
})
export class InterfaceMethodDetailComponent implements OnInit {
  method: InterfaceMethodModel | null = null;
  interfaces: any[] = [];
  loading = false;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private interfaceMethodService: InterfaceMethodService,
    private interfaceService: InterfaceService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loading = true;
      this.interfaceMethodService.getInterfaceMethod(id).subscribe({
        next: (data) => {
          this.method = data;
          this.loading = false;
        },
        error: () => {
          this.errorMessage = 'Error al cargar el método.';
          this.loading = false;
        }
      });
      this.interfaceService.getInterfaces().subscribe({
        next: (data) => this.interfaces = data,
        error: () => this.errorMessage = 'Error al cargar las interfaces.'
      });
    }
  }

  getInterfaceName(interfaceId?: string): string {
    if (!interfaceId) return 'N/A';
    return this.interfaces.find(i => i.id === interfaceId)?.name || 'Interfaz no encontrada';
  }

  goBack(): void {
    this.router.navigate(['/interface-methods']);
  }
}
