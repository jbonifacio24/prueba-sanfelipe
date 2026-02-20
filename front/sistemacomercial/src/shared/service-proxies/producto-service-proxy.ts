import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, pipe } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';

export interface ProductoDto {
  idProducto: number;
  nombre: string;
  precio: number;
}

@Injectable({ providedIn: 'root' })
export class ProductoServiceProxy {
  constructor(private http: HttpClient) {}

  cargarConfig(): Observable<any> {
    return this.http.get('/assets/appconfig.json');
  }

  login(authUrl: string, username: string, password: string): Observable<any> {
    return this.http.post<any>(authUrl, { username, password });
  }

  obtenerProductos(productoUrl: string, token: string): Observable<ProductoDto[]> {
    const headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
    return this.http.get<ProductoDto[]>(productoUrl, { headers });
  }

  _registrarProducto(productoUrl: string, token: string, producto: ProductoDto): Observable<any> {
    const headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
    return this.http.post(productoUrl, producto, { headers, observe: 'response' }).pipe(
          map((response: HttpResponse<any>) => {
              if (response.status === 200 || response.status === 201 || response.status === 204) {
                  if (response.body) {
                      try {
                          return JSON.parse(response.body);
                      } catch {
                          return response.body;
                      }
                  }
                  return {};
              } else {
                  throw new Error('Error al registrar producto: Código de estado ' + response.status);
              }
          }),
      catchError(err => {
        let msg = 'Error desconocido al registrar producto';
        if (err.status && err.error && err.error.message) {
          msg = err.error.message;
        } else if (err.message) {
          msg = err.message;
        }
        return throwError(() => new Error(msg));
      })
    );
  }
}
