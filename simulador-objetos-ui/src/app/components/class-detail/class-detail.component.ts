import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClassService, ClassModel } from '../../services/class.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-class-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './class-detail.component.html',
  styleUrl: './class-detail.component.scss'
})
export class ClassDetailComponent implements OnInit {
  classData: ClassModel | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private classService: ClassService,
    public router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.classService.getClass(id).subscribe({
        next: (data) => this.classData = data,
        error: () => this.errorMessage = 'Error al cargar la clase.'
      });
    } else {
      this.errorMessage = 'ID de clase no especificado.';
    }
  }
}
