import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { InterfaceService } from '../../services/interface.service';
import { InterfaceModel } from '../../models/interface.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-interface-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './interface-detail.component.html',
  styleUrl: './interface-detail.component.scss'
})
export class InterfaceDetailComponent implements OnInit {
  interfaceData: InterfaceModel | null = null;
  loading = true;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private interfaceService: InterfaceService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.interfaceService.getInterface(id).subscribe({
        next: (data) => {
          this.interfaceData = data;
          this.loading = false;
        },
        error: (err) => {
          this.errorMessage = 'Failed to load interface details.';
          this.loading = false;
        }
      });
    }
  }
}
