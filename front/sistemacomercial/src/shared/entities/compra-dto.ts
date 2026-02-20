
export interface ICompraCabDto {
  subTotal: number;
  igv: number;
  total: number;
  det: CompraDetDto[];
}

export class CompraCabDto implements ICompraCabDto {
  subTotal: number  = 0;
  igv: number = 0;
  total: number = 0;
  det: CompraDetDto[] = [];

  constructor(data?: ICompraCabDto) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property))
          (this as any)[property] = (data as any)[property];
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.subTotal = data["subTotal"];
      this.igv = data["igv"];
      this.total = data["total"];
      this.det = Array.isArray(data["det"]) ? data["det"].map((d: any) => CompraDetDto.fromJS(d)) : [];
    }
  }

  static fromJS(data: any): CompraCabDto {
    data = typeof data === 'object' ? data : {};
    let result = new CompraCabDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["subTotal"] = this.subTotal;
    data["igv"] = this.igv;
    data["total"] = this.total;
    data["det"] = this.det ? this.det.map(d => d.toJSON()) : [];
    return data;
  }
}

export interface ICompraDetDto {
  idProducto: number;
  cantidad: number;
  precio: number;
  subTotal: number;
  igv: number;
  total: number;
}

export class CompraDetDto implements ICompraDetDto {
  idProducto: number = 0;
  cantidad: number = 0;
  precio: number = 0;
  subTotal: number = 0;
  igv: number = 0;
  total: number = 0;

  constructor(data?: ICompraDetDto) {
    if (data) {
      for (const property in data) {
        if (data.hasOwnProperty(property))
          (this as any)[property] = (data as any)[property];
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.idProducto = data["idProductoDet"] ?? 0;
      this.cantidad = data["cantidadDet"] ?? 0;
      this.precio = data["precioDet"] ?? 0;
      this.subTotal = data["subTotalDet"] ?? 0;
      this.igv = data["igvDet"] ?? 0;
      this.total = data["totalDet"] ?? 0;
    }
  }

  static fromJS(data: any): CompraDetDto {
    data = typeof data === 'object' ? data : {};
    let result = new CompraDetDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["idProductoDet"] = this.idProducto ?? 0;
    data["cantidadDet"] = this.cantidad ?? 0;
    data["precioDet"] = this.precio ?? 0;
    data["subTotalDet"] = this.subTotal ?? 0;
    data["igvDet"] = this.igv ?? 0;
    data["totalDet"] = this.total ?? 0;
    return data;
  }
}
