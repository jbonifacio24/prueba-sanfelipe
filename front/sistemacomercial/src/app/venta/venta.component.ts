  // ...existing code...
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Producto {
  id: number;
  nombre: string;
  precioVenta: number;
  stock: number;
}

interface ProductoVenta {
  producto: Producto;
  cantidad: number;
}

@Component({
  selector: 'app-venta',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './venta.component.html',
  styleUrls: ['./venta.component.css']
})
export class VentaComponent {
    getTotal(): number {
      return this.productosVenta.reduce((acc, pv) => acc + pv.cantidad * pv.producto.precioVenta, 0);
    }
  productos: Producto[] = [
    { id: 1, nombre: 'Producto A', precioVenta: 10, stock: 5 },
    { id: 2, nombre: 'Producto B', precioVenta: 20, stock: 3 },
    { id: 3, nombre: 'Producto C', precioVenta: 15, stock: 8 }
  ];
  productosVenta: ProductoVenta[] = [];
  productoSeleccionadoId: number | null = null;
  cantidad: number = 1;
  mensajeError: string = '';

  agregarProducto() {
    this.mensajeError = '';
    const producto = this.productos.find(p => p.id === this.productoSeleccionadoId);
    if (!producto) return;
    if (this.cantidad > producto.stock) {
      this.mensajeError = 'La cantidad no debe ser mayor al stock disponible.';
      return;
    }
    const existente = this.productosVenta.find(pv => pv.producto.id === producto.id);
    if (existente) {
      if (existente.cantidad + this.cantidad > producto.stock) {
        this.mensajeError = 'La cantidad total no debe ser mayor al stock disponible.';
        return;
      }
      existente.cantidad += this.cantidad;
    } else {
      this.productosVenta.push({ producto, cantidad: this.cantidad });
    }
    this.cantidad = 1;
    this.productoSeleccionadoId = null;
  }

  guardarVenta() {
    // Aquí iría la lógica para guardar la venta
    alert('Venta registrada correctamente');
    this.productosVenta = [];
  }
}
