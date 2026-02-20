

import Swal from 'sweetalert2';
import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CompraServiceProxy } from '../../shared/service-proxies/compra-service-proxy';
import { finalize } from 'rxjs';
import { ProductoServiceProxy, ProductoDto } from '../../shared/service-proxies/producto-service-proxy';
import { RegistroProductoComponent } from '../../shared/components/registrar-productos/registro-producto.component';
import { CompraCabDto,CompraDetDto } from '../../shared/entities/compra-dto';
import { MovimientoCabDto, MovimientoDetDto } from '../../shared/entities/movimiento-dto';
import { MovimientoServiceProxy } from '../../shared/service-proxies/movimiento-service-proxy';
@Component({
  selector: 'app-compra',
  standalone: true,
  imports: [CommonModule, FormsModule, RegistroProductoComponent],
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css']
})
export class CompraComponent implements OnInit, AfterViewInit {
      calcularTotal() {
        const cantidad = parseFloat(this.detalleActual.cantidad) || 0;
        const precio = parseFloat(this.detalleActual.precio) || 0;
        this.detalleActual.total = (cantidad * precio).toFixed(2);
      }
    onRegistroSeleccionado(registro: any) {
      // Aquí puedes manejar el registro seleccionado
      console.log('Registro seleccionado:', registro);
      // Ejemplo: mostrar un modal, actualizar campos, etc.
    }
  appConfig: any = null;
  // NOTA: Debes inyectar un servicio de notificación real, por ejemplo ToastrService o similar
  // Aquí se asume que existe un notify con método success. Si no, reemplaza por tu servicio real.
  constructor(
    private compraService: CompraServiceProxy,
    private productoService: ProductoServiceProxy,
    private movimientoService: MovimientoServiceProxy,
    private cdr: ChangeDetectorRef
  ) {}
  // notify = { success: (msg: string) => alert(msg) }; // Descomenta si no tienes un servicio real
  // Cabecera de la compra
  compraCab: any = {
    idCompraCab: 0,
    fecRegistro: this.getFechaActual(),
    subTotal: 0,
    igv: 0,
    total: 0
  };

  CompraCabDto: new () => CompraCabDto = CompraCabDto;
    
  getFechaActual(): string {
    const hoy = new Date();
    const dia = hoy.getDate().toString().padStart(2, '0');
    const mes = (hoy.getMonth() + 1).toString().padStart(2, '0');
    const anio = hoy.getFullYear();
    return `${dia}/${mes}/${anio}`;
  }

  // Lista de detalles de la compra
  compraDet: any[] = [];
  detalleActual: any = {
    idCompraDet: 0,
    idCompraCab: 0,
    idProducto: '', // Para que el combo inicie en blanco
    cantidad: 0,
    precio: 0,
    subTotal: 0,
    igv: 0,
    total: 0
  };
  editandoDetalle: number | null = null;

  productos: ProductoDto[] = [];
  mostrarModalProducto = false;
  productoNuevo: any = {
    idProducto: 0,
    nombreProducto: '',
    nroLote: 0,
    costo: 0,
    precioVenta: 0
  };

  @ViewChild('inputNombreProducto') inputNombreProducto!: ElementRef;
  @ViewChild('inputCantidadDetalle') inputCantidadDetalle!: ElementRef;

  // Manejar cierre con ESC
  handleKeydown = (event: KeyboardEvent) => {
    if (this.mostrarModalProducto && event.key === 'Escape') {
      this.cerrarModalProducto();
    }
  };

  ngOnInit() {
    this.cargarProductos();
    window.addEventListener('keydown', this.handleKeydown);
  }

  ngOnDestroy() {
    window.removeEventListener('keydown', this.handleKeydown);
  }

    getNombreProducto(id: number): string {
    const prod = this.productos.find(p => p.idProducto == id);
    return prod ? prod.nombre : id.toString();
  }

  cargarProductos() {
    // Primero autenticarse igual que en registrarCompra
    this.productoService.login("http://localhost:5001/login", 'admin', 'password').subscribe({
      next: (resp) => {
        const token = resp.token;
        this.productoService.obtenerProductos("http://localhost:5004/api/producto/get", token).subscribe({
          next: (productos) => {
            // Adaptar la estructura recibida a la que espera el frontend
            this.productos = productos.map((p: any) => ({
              idProducto: p.idProducto,
              nombre: p.nombreProducto?.name ?? '',
              precio: p.precioVenta?.amount ?? 0
            }));
            this.cdr.detectChanges();
          },
          error: (err) => {

            if(err.status === 400 || err.status === 500) {
            Swal.fire({
              icon: 'error',
              title: 'Error al obtener productos',
              text: err.message || 'Ocurrió un error inesperado.',
              confirmButtonText: 'Cerrar'
            });
          } else if (err.status === 403 || err.status === 401) {
            Swal.fire({
              icon: 'error',
              title: 'Acceso denegado',
              text: 'No tienes permisos para acceder a los productos.',
              confirmButtonText: 'Cerrar'
            });
          } else {
            Swal.fire({
              icon: 'error',
              title: 'Error de red',
              text: 'No se pudo conectar al servidor. Por favor, inténtelo de nuevo más tarde.',
              confirmButtonText: 'Cerrar'
            });
          }
        }
        });
      },
      error: (err) => {
        Swal.fire({
              icon: 'error',
              title: 'Error al obtener productos',
              text: err.message || 'Ocurrió un error inesperado.',
              confirmButtonText: 'Cerrar'
            });
      }
    });
  }



  cerrarModalProducto() {
    this.mostrarModalProducto = false;
    setTimeout(() => {
      if (this.inputCantidadDetalle) {
        this.inputCantidadDetalle.nativeElement.focus();
      }
    }, 50);
  }

  

  agregarDetalle() {
    this.compraDet.push({ ...this.detalleActual });
 
    this.actualizarTotalCabecera();
    this.limpiarDetalle();
  }

  editarDetalle(index: number) {
    this.detalleActual = { ...this.compraDet[index] };
    this.compraDet.splice(index, 1);
  }

  eliminarDetalle(index: number) {
    this.compraDet.splice(index, 1);
    this.actualizarTotalCabecera();
  }

  limpiarDetalle() {
    this.detalleActual = {
      idCompraDet: 0,
      idCompraCab: 0,
      idProducto: 0,
      cantidad: 0,
      precio: 0,
      subTotal: 0,
      igv: 0,
      total: 0
    };
  }

  registrarCompra() {

    let compraDto = new CompraCabDto()  ;
    compraDto = this.compraCab ;
    compraDto.det = this.compraDet.map(det => {
      let detDto = new CompraDetDto();
      detDto.idProducto = det.idProducto;
      detDto.cantidad = det.cantidad;
      detDto.precio = det.precio;
      detDto.subTotal = det.subTotal;
      detDto.igv = det.igv;
      detDto.total = det.total;
      return detDto;
    }   );

    this.compraService.login("http://localhost:5001/login", 'admin', 'password').subscribe({
      next: (resp) => {
        const token = resp.token;
        localStorage.setItem('token', token);
        // 2. Enviar la compra al microservicio de compras
        this.compraService._registrarCompra(
          "http://localhost:5002/api/compra/create",
          token,
          compraDto
        )
        .pipe(finalize(() => {}))
        .subscribe({
          next: (a) => {
            const cab = { ...this.compraCab, idCompraCab: a.idCompraCab };
            this.registrarMovimiento(cab, this.compraDet, token);
          },
          error: (err) => {
            Swal.fire({
              icon: 'error',
              title: 'Error al registrar compra',
              text: err.message || 'Intentelo de nuevo.',
              confirmButtonText: 'Cerrar'
            });
          }
        });
      },
      error: (err) => {
        Swal.fire({
              icon: 'error',
              title: 'Error al registrar compra',
              text: err.message || 'Ocurrió un error inesperado.',
              confirmButtonText: 'Cerrar'
            });
      }
    });
    
  }

   registrarMovimiento(cab: any, det: any[], token: string) {
    let movimientoCabDto = new MovimientoCabDto();
    movimientoCabDto.idTipoMovimiento = 1; 
    movimientoCabDto.idDocumentoOrigen = cab.idCompraCab;
    movimientoCabDto.det = det.map(det => {
      let detDto = new MovimientoDetDto();
      detDto.idProducto = det.idProducto;
      detDto.cantidad = det.cantidad;
      return detDto;
    });

    this.movimientoService._registrarMovimiento(
      "http://localhost:5003/api/movimiento/create",
      token,
      movimientoCabDto
    ).subscribe({
      next: () => {
         Swal.fire({
              icon: 'success',
              title: '¡Registro exitoso!',
              text: 'La compra fue registrada correctamente.',
              confirmButtonText: 'Aceptar'
            }).then(() => {
              this.limpiarTodo();
            });
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Error al registrar el movimiento',
          text: err.message || 'Intentelo de nuevo.',
          confirmButtonText: 'Cerrar'
        });
      }
    });
   }

  actualizarTotalCabecera() {
    let sumaSubTotal = 0;
    let sumaIgv = 0;
    let sumaTotal = 0;
    this.compraDet.forEach(det => {
      sumaTotal += parseFloat(det.total) || 0;
    });
    this.compraCab.subTotal = 0;
    this.compraCab.igv = 0;
    this.compraCab.total = sumaTotal ? sumaTotal.toFixed(2) : '';
  }

  ngAfterViewInit() {
    // Si el modal de producto está abierto al iniciar, enfoca
    if (this.mostrarModalProducto && this.inputNombreProducto) {
      setTimeout(() => this.inputNombreProducto.nativeElement.focus(), 0);
    }
  }
  limpiarTodo() {
    // Limpiar cabecera (forzar string vacío y fecha actual)
    this.compraCab.idCompraCab = '';
    this.compraCab.fecRegistro = this.getFechaActual();
    this.compraCab.subTotal = '';
    this.compraCab.igv = '';
    this.compraCab.total = '';

    // Limpiar detalle actual
    this.limpiarDetalle();

    // Limpiar datatable de detalles (array)
    this.compraDet = [];
    this.cdr.detectChanges();
  }


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

   // Setea el precio de compra del producto seleccionado en el detalle
  setearPrecioCompra(idProducto: number) {
    const prod = this.productos.find(p => p.idProducto == idProducto);
    if (prod) {
      this.detalleActual.precio = prod.precio;
    } else {
      this.detalleActual.precio = '';
    }
  }

    onProductoRegistrado(producto: any) {
    this.cargarProductos();
    setTimeout(() => {
      // Selecciona automáticamente el producto recién creado si tiene id
      if (producto && producto.idProducto) {
        this.detalleActual.idProducto = producto.idProducto;
      }
    }, 500);
  }
}
