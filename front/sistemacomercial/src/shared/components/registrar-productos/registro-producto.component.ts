
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component, OnInit, ViewChild, ElementRef, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import { ProductoServiceProxy, ProductoDto } from '../../service-proxies/producto-service-proxy';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-registro-producto',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './registro-producto.component.html',
  styleUrls: ['./registro-producto.component.css']
})
export class RegistroProductoComponent implements OnInit {
    @Output() cerrarModal = new EventEmitter<void>();
    @Output() productoRegistrado = new EventEmitter<any>();
    close() {
      this.cerrarModal.emit();
    }
  ngOnInit(): void {}
 
constructor(

    private productoService: ProductoServiceProxy,
    private cdr: ChangeDetectorRef
  ) {}

   productoNuevo: any = {
    idProducto: 0,
    nombreProducto: '',
    nroLote: 0,
    costo: 0,
    precioVenta: 0
  };
  
  limitarDecimales(event: any, decimales: number, objeto: string, propiedad: string) {
    let valor = event.target.value;
    // Limitar a 99999
    let num = parseFloat(valor);
    if (!isNaN(num) && num > 99999) {
      valor = '99999';
      event.target.value = valor;
      (this as any)[objeto][propiedad] = valor;
      return;
    }
    // Limitar decimales
    if (valor && valor.includes('.')) {
      const partes = valor.split('.');
      if (partes[1].length > decimales) {
        valor = partes[0] + '.' + partes[1].slice(0, decimales);
        event.target.value = valor;
        // Actualiza el modelo
        (this as any)[objeto][propiedad] = valor;
      }
    }
  }

  registrarProducto() {
    // Aquí deberías hacer el POST al microservicio de productos, autenticando igual que en cargarProductos
    this.productoService.login("http://localhost:5001/login", 'admin', 'password').subscribe({
      next: (resp) => {

        const token = resp.token;
        this.productoService._registrarProducto("http://localhost:5004/api/producto/create", token, this.productoNuevo).subscribe({
          next: () => {
            this.productoRegistrado.emit(this.productoNuevo);
            this.close();
          },
          error: (err) => {

            Swal.fire({
              icon: 'error',
              title: 'Error al registrar producto',
              text: err.message || 'Ocurrió un error inesperado.',
              confirmButtonText: 'Aceptar'
            });
          }
        });
           
        
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Error de autenticación',
          text: err.message || 'Ocurrió un error inesperado.',
          confirmButtonText: 'Aceptar'
        });
      }
    });
  }

}
