export interface IMovimientoCabDto {
  idTipoMovimiento: number;
  idDocumentoOrigen: number;
  det: MovimientoDetDto[];
}

export class MovimientoCabDto implements IMovimientoCabDto {
  idTipoMovimiento: number = 0;
  idDocumentoOrigen: number = 0;
  det: MovimientoDetDto[] = [];

  constructor(data?: IMovimientoCabDto) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property))
          (this as any)[property] = (data as any)[property];
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.idTipoMovimiento = data["idTipoMovimiento"];
      this.idDocumentoOrigen = data["idDocumentoOrigen"];
      this.det = Array.isArray(data["det"]) ? data["det"].map((d: any) => MovimientoDetDto.fromJS(d)) : [];
    }
  }

  static fromJS(data: any): MovimientoCabDto {
    data = typeof data === 'object' ? data : {};
    let result = new MovimientoCabDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["idTipoMovimiento"] = this.idTipoMovimiento;
    data["idDocumentoOrigen"] = this.idDocumentoOrigen;
    data["det"] = this.det ? this.det.map(d => d.toJSON()) : [];
    return data;
  }
}

export interface IMovimientoDetDto {
  idProducto: number;
  cantidad: number;
 
}

export class MovimientoDetDto implements IMovimientoDetDto {
  idProducto: number = 0;
  cantidad: number = 0;
 

  constructor(data?: IMovimientoDetDto) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property))
          (this as any)[property] = (data as any)[property];
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.idProducto = data["idProducto"];
      this.cantidad = data["cantidad"];
 
    }
  }

  static fromJS(data: any): MovimientoDetDto {
    data = typeof data === 'object' ? data : {};
    let result = new MovimientoDetDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["idProducto"] = this.idProducto;
    data["cantidad"] = this.cantidad;
  
    return data;
  }
}
