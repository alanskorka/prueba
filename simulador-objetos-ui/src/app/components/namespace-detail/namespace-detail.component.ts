import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NamespaceService, NamespaceModel } from '../../services/namespace.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-namespace-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './namespace-detail.component.html',
  styleUrl: './namespace-detail.component.scss'
})
export class NamespaceDetailComponent implements OnInit {
  namespaceData: NamespaceModel | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private namespaceService: NamespaceService,
    public router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.namespaceService.getNamespace(id).subscribe({
        next: (data) => this.namespaceData = data,
        error: () => this.errorMessage = 'Error al cargar el namespace.'
      });
    } else {
      this.errorMessage = 'ID de namespace no especificado.';
    }
  }
}
