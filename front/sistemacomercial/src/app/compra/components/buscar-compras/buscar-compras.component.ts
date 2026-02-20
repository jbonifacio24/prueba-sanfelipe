import { Output, EventEmitter } from '@angular/core';
import { Component } from '@angular/core';
import { CommonModule, DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-buscar-compras',
  standalone: true,
  imports: [CommonModule, FormsModule, DecimalPipe],
  templateUrl: './buscar-compras.component.html',
  styleUrls: ['./buscar-compras.component.css']
})
export class BuscarComprasComponent {
    @Output() registroSeleccionado = new EventEmitter<any>();
  mostrarModal = false;
  busqueda = '';
  resultados: any[] = [];

  abrirModal() {
    this.mostrarModal = true;
  }

  cerrarModal() {
    this.mostrarModal = false;
  }

  buscar() {
    // Aquí va la lógica de búsqueda (mock)
    this.resultados = [
      { local: 'EESS ARAMBURU', articulo: 'DIESEL B5 S50 UV', precio: 18.00 }
    ];
  }

  seleccionarRegistro(registro: any) {
    this.registroSeleccionado.emit(registro);
    this.cerrarModal();
  }
}
