import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable()
export class SuccessCheckInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      map((event: HttpEvent<any>) => {
        // Solo procesar respuestas HTTP (no eventos como HttpProgressEvent)
        if (event instanceof HttpResponse) {
          const body = event.body;

          // Verificar si la respuesta tiene la estructura esperada y success es false
          if (body && body.hasOwnProperty('success') && body.success === false) {
            // Transformar la respuesta no exitosa en un error
            const errorMessage = body.message || 'Error desconocido en la respuesta';
            throw new Error(errorMessage);
          }
        }
        return event;
      }),
      catchError((error) => {
        // Para errores HTTP o errores que acabamos de crear
        let errorMessage = 'Error en la petición';

        if (error instanceof HttpErrorResponse) {
          errorMessage = error.message;
        } else if (error instanceof Error) {
          errorMessage = error.message;
        }

        console.error('Error interceptado:', errorMessage);
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}
