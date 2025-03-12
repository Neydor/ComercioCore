import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import { routes } from './app.routes';

import Aura from '@primeng/themes/aura';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from '@core/interceptors/auth.interceptor';
import { SuccessCheckInterceptor } from '@core/interceptors/success.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([authInterceptor])
    ),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
          preset: Aura,
          options: {
             darkModeSelector: 'false',
             cssLayer: {
                name: 'primeng',
                order: 'theme, base, primeng'
            }
          }
      }
  }),
    provideHttpClient(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SuccessCheckInterceptor,
      multi: true
    }
  ],
};
