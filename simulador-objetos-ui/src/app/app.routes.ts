import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ClassListComponent } from './components/class-list/class-list.component';
import { ClassDetailComponent } from './components/class-detail/class-detail.component';
import { ClassFormComponent } from './components/class-form/class-form.component';
import { InterfaceListComponent } from './components/interface-list/interface-list.component';
import { InterfaceDetailComponent } from './components/interface-detail/interface-detail.component';
import { InterfaceFormComponent } from './components/interface-form/interface-form.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'classes', component: ClassListComponent },
  { path: 'classes/new', component: ClassFormComponent },
  { path: 'classes/:id', component: ClassDetailComponent },
  { path: 'classes/:id/edit', component: ClassFormComponent },
  { path: 'interfaces', component: InterfaceListComponent },
  { path: 'interfaces/new', component: InterfaceFormComponent },
  { path: 'interfaces/:id', component: InterfaceDetailComponent },
  { path: 'interfaces/:id/edit', component: InterfaceFormComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];
