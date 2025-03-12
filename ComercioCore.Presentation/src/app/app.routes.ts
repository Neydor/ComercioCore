import { Routes } from '@angular/router';
import { AuthGuard } from '@core/auth/guards/auth.guard';
import { LoginComponent } from '@features/auth/login/login.component';
import { ComercianteFormComponent } from '@features/comerciante-form/comerciante-form.component';
import { HomeComponent } from '@features/home/home.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'comerciantes-form/:id', // Ruta para editar comerciante
    component: ComercianteFormComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'comerciantes-form', // Ruta para crear nuevo comerciante
    component: ComercianteFormComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard]
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];