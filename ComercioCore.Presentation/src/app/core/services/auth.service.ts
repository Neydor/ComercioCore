import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { environment } from '@env/environment';

export interface User {
  id: number;
  nombre: string;
  correo: string;
  rol: 'Administrador' | 'Auxiliar de Registro';
}

interface AuthResponse {
  token: string;
  user: User;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private tokenExpirationTimer: any;

  private currentUserSubject = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
    this.autoLogin();
  }

  login(CorreoElectronico: string, Contrasena: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      `${environment.apiUrl}/api/auth/login`,
      { CorreoElectronico, Contrasena }
    ).pipe(
      tap(response => this.handleAuthentication(response)),
      catchError(error => {
        this.clearAuthData();
        return throwError(() => error);
      })
    );
  }

  logout(): void {
    this.clearAuthData();
    this.router.navigate(['/auth/login']);
  }

  isAdmin(): boolean {
    return this.currentUserSubject.value?.rol === 'Administrador';
  }

  getToken(): string | null {
    return localStorage.getItem('auth_token');
  }

  private autoLogin(): void {
    const token = this.getToken();
    if (!token) return;

    try {
      const decodedToken: any = jwtDecode(token);
      if (this.isTokenValid(decodedToken)) {
        const user: User = {
          id: decodedToken.sub,
          nombre: decodedToken.nombre,
          correo: decodedToken.email,
          rol: decodedToken.role
        };
        this.currentUserSubject.next(user);
        this.setAutoLogout(decodedToken.exp * 1000 - Date.now());
      }
    } catch (error) {
      this.logout();
    }
  }

  private handleAuthentication(response: AuthResponse): void {
    const decodedToken: any = jwtDecode(response.token);
    const user: User = {
      id: decodedToken.sub,
      nombre: decodedToken.nombre,
      correo: decodedToken.email,
      rol: decodedToken.role
    };
    localStorage.setItem('auth_token', response.token);

    this.currentUserSubject.next(user);
    this.setAutoLogout(decodedToken.exp * 1000 - Date.now());
  }

  private setAutoLogout(expirationDuration: number): void {
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.tokenExpirationTimer = setTimeout(() => {
      this.logout();
    }, expirationDuration);
  }

  private isTokenValid(decodedToken: any): boolean {
    return decodedToken && decodedToken.exp * 1000 > Date.now();
  }

  private clearAuthData(): void {
    localStorage.removeItem('auth_token');
    this.currentUserSubject.next(null);
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
  }
}
