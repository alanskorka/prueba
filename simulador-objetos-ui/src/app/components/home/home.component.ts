import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="container mt-5">
      <div class="row justify-content-center">
        <div class="col-md-8">
          <div class="card shadow-lg">
            <div class="card-body text-center">
              <h1 class="display-4 mb-3">¡Bienvenido al Simulador de Objetos!</h1>
              <p class="lead mb-4">
                Esta aplicación te permite crear, visualizar y administrar Clases, Interfaces, Namespaces, Atributos y mucho más.<br>
                Utiliza el menú superior para navegar entre las distintas entidades del sistema.
              </p>
              <div class="d-flex flex-wrap justify-content-center gap-3 mt-4">
                <a routerLink="/classes" class="btn btn-outline-primary btn-lg">Clases</a>
                <a routerLink="/interfaces" class="btn btn-outline-secondary btn-lg">Interfaces</a>
                <a routerLink="/namespaces" class="btn btn-outline-success btn-lg">Namespaces</a>
                <a routerLink="/attributes" class="btn btn-outline-info btn-lg">Atributos</a>
                <a routerLink="/methods" class="btn btn-outline-warning btn-lg">Métodos</a>
              </div>
              <div class="mt-5 text-muted">
                <small>Desarrollado para la materia DA2 - 2024</small>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .card {
      border-radius: 1rem;
    }
    .btn-lg {
      min-width: 160px;
    }
    .display-4 {
      font-weight: 700;
    }
    .lead {
      font-size: 1.25rem;
    }
  `]
})
export class HomeComponent {}
