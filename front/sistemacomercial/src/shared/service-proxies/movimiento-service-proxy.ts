import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MovimientoCabDto,MovimientoDetDto } from '../entities/movimiento-dto';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
@Injectable({ providedIn: 'root' })
export class MovimientoServiceProxy {
  constructor(private http: HttpClient) {}

  cargarConfig(): Observable<any> {
    return this.http.get('/assets/appconfig.json');
  }

  login(authUrl: string, username: string, password: string): Observable<any> {
    return this.http.post<any>(authUrl, { username, password });
  }

    _registrarMovimiento(movimientoUrl: string, token: string, movimiento: MovimientoCabDto): Observable<any> {
    const headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
    return this.http.post(movimientoUrl, movimiento, { headers, observe: 'response' }).pipe(
      map((response: HttpResponse<any>) => {
        let bodyObj;
        if (response.body) {
          try {
            bodyObj = typeof response.body === 'string' ? JSON.parse(response.body) : response.body;
          } catch {
            bodyObj = response.body;
          }
        }
        // Detectar error de negocio aunque status sea 200/201/204
        if (response.status === 200 || response.status === 201 || response.status === 204) {
          if (bodyObj && bodyObj.Message) {
            let errorMsg = bodyObj.Message;
            if (bodyObj.Error) {
              errorMsg += ' (' + bodyObj.Error + ')';
            }
            throw new Error(errorMsg);
          }
          return bodyObj || {};
        } else {
          let errorMsg = 'Error al registrar movimiento: Código de estado ' + response.status;
          if (bodyObj && bodyObj.Message) {
            errorMsg = bodyObj.Message;
            if (bodyObj.Error) {
              errorMsg += ' (' + bodyObj.Error + ')';
            }
          }
          throw new Error(errorMsg);
        }
      }),
      catchError(err => {
        let msg = 'Error desconocido al registrar movimiento';
        if (err.error) {
          let bodyObj;
          try {
            bodyObj = typeof err.error === 'string' ? JSON.parse(err.error) : err.error;
          } catch {
            bodyObj = err.error;
          }
          if (bodyObj && bodyObj.Message) {
            msg = bodyObj.Message;
            if (bodyObj.Error) {
              msg += ' (' + bodyObj.Error + ')';
            }
          }
        } else if (err.message) {
          msg = err.message;
        }
        return throwError(() => new Error(msg));
      })
    );
  }
}