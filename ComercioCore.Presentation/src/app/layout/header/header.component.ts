import { Component, inject, Input, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';
import { ButtonModule } from 'primeng/button';
import { AvatarModule } from 'primeng/avatar';
import { MenuModule } from 'primeng/menu';
import { Router, RouterLink } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { MenuItem } from 'primeng/api';
import { Subscription } from 'rxjs';
import { StepperModule } from 'primeng/stepper';
import { SplitterModule } from 'primeng/splitter';
import { DividerModule } from 'primeng/divider';
import { ImageModule } from 'primeng/image';
@Component({
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    AvatarModule,
    MenuModule,
    MenubarModule,
    StepperModule,
    SplitterModule,
    DividerModule,
    ImageModule
  ],
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  opcionesUsuario: MenuItem[] = [];
  isAuthenticated = false;
  steppers:any = []
  items:any = []
  currentUser: any = null;
  @Input() steperValue: number = 1;
  private userSub: Subscription | undefined;
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userSub = this.authService.currentUser$.subscribe(user => {
      this.isAuthenticated = !!user;
      console.log("aasda",user);
      this.currentUser = user;
    });
    this.opcionesUsuario = [
      {
        label: 'Lista de comercios',
        icon: 'pi pi-list',
        command: () => {
          this.router.navigate(['/home']);
        }
      },
      {
        label: 'Cerrar Sesión',
        icon: 'pi pi-sign-out',
        command: () => {
          this.cerrarSesion();
        }
      }
    ];
    this.steppers = [
      {
        label: 'Lista Formulario',
        value: 1
      },
      {
        label: 'Crear Formulario',
        value: 2
      },
    ];
    this.items = [
      this.steppers
    ]


  }
  ngOnDestroy(): void {
    if (this.userSub) {
      this.userSub.unsubscribe();
    }
  }

  cerrarSesion(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
