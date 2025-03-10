// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { AuthGuard } from '@core/auth/guards/auth.guard';
import { LoginComponent } from '@features/auth/login/login.component';
import { ComerciantesListComponent } from '@features/comerciantes-list/comerciantes-list.component';
import { HomeComponent } from '@features/home/home.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'comerciantes-list',
    component: ComerciantesListComponent,
    canActivate: [AuthGuard] // Protege la ruta con AuthGuard
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard] // Protege la ruta con AuthGuard
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];
