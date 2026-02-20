using  MS.Producto.Domain.Interfaces;
using MS.Producto.Domain.Entities;
using MS.Producto.Domain.Services;
using MS.Producto.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Producto.Domain.Exceptions;

namespace MS.Producto.Application.UseCases
{
    public class CreateProductHandler
    {
        private readonly ICreateProductRepository _repository;
        private readonly CreateProductService _service;

        public CreateProductHandler(ICreateProductRepository repository, CreateProductService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<int> HandleAsync(CreateProductCommand input)
        {
            try
            {
                var idProducto = input.IdProducto;
                var nombreProducto = new NombreProducto(input.NombreProducto ?? string.Empty);
                var nroLote = new NroLote(input.NroLote ?? 0);
                var fecRegistro = DateTime.Now;
                var costo = new Costo(input.Costo);
                var precioVenta = new PrecioVenta(input.PrecioVenta);

                var product = new Product
                {
                    IdProducto = idProducto,
                    NombreProducto = nombreProducto,
                    NroLote = nroLote,
                    FecRegistro = fecRegistro,
                    Costo = costo,
                    PrecioVenta = precioVenta
                };

                //validar reglas de negocio antes de crear el producto
                _service.CreateProduct(product);

                // Si la validación es exitosa, se procede a crear el producto en el repositorio
                await _repository.CreateProductAsync(product);

                return 1;
            }
            catch (DomainException )
            {

                throw;
            }
            catch (InvalidOperationException ioex)
            {
                // Excepción de infraestructura (por ejemplo, error de conexión)
                throw new ApplicationException($"Error de infraestructura: {ioex.Message}", ioex);
            }
            catch (Exception ex)
            {
                // Cualquier otro error inesperado
                throw new ApplicationException($"Error inesperado al crear el producto: {ex.Message}", ex);
            }

        }
    }
}
