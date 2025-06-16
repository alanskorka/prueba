import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClassService } from '../../services/class.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-class-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './class-list.component.html',
  styleUrl: './class-list.component.scss'
})
export class ClassListComponent implements OnInit {
  classes: any[] = [];

  constructor(private classService: ClassService) { }

  ngOnInit(): void {
    this.classService.getClasses().subscribe(data => {
      this.classes = data;
    });
  }
}
