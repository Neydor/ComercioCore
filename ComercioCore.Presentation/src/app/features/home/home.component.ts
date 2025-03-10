import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ComerciantesListComponent } from '@features/comerciantes-list/comerciantes-list.component';
import { HeaderComponent } from '@layout/header/header.component';
import { ConfirmationService } from 'primeng/api';
import { TableModule } from 'primeng/table';

@Component({
  imports: [
    CommonModule,
    HeaderComponent,
    TableModule,
    ComerciantesListComponent
  ],
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [ConfirmationService]
})
export class HomeComponent {

  constructor(
  ) {
  }

}
