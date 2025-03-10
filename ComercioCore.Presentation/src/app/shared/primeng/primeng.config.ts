import { provideAnimations } from '@angular/platform-browser/animations';
import { MessageService } from 'primeng/api';

export const PrimeNGConfig = {
  providers: [provideAnimations(), MessageService]
};
