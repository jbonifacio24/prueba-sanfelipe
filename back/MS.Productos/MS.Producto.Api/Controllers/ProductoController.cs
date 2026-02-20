using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Producto.Application.Facades;
using MS.Producto.Application.UseCases;
using MS.Producto.Domain.Entities;
using MS.Producto.Domain.Exceptions;

namespace MS.Producto.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductFacade _facade;

        public ProductoController(ProductFacade facade)
        {
            _facade = facade;
        }
        [HttpGet("test")]
        public IActionResult Index()
        {
            return Ok("API producto funcionando correctamente");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateProductCommand input)
        {
            try
            {
                var output = await _facade.CreateProductAsync(input);
                return Ok();
            }
            catch (DomainException dex)
            {
                // Si tienes una excepción de dominio personalizada
                return BadRequest(new { Message = "Error en la regla de negocio", Error = dex.Message });
            }
            catch (ApplicationException aex)
            {
                // Para errores de aplicación
                return StatusCode(500, new { Message = "Error de aplicación", Error = aex.Message });
            }
            catch (Exception ex)
            {
                // Para cualquier otro error inesperado
                return StatusCode(500, new { Message = "Error inesperado", Error = ex.Message });
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var output = await _facade.GetAllProductsAsync();
                return Ok(output); // Aquí devuelves la lista

            }
            catch (DomainException dex)
            {
                // Si tienes una excepción de dominio personalizada
                return BadRequest(new { Message = "Error en la regla de negocio", Error = dex.Message });
            }
            catch (ApplicationException aex)
            {
                // Para errores de aplicación
                return StatusCode(500, new { Message = "Error de aplicación", Error = aex.Message });
            }
            catch (Exception ex)
            {
                // Para cualquier otro error inesperado
                return StatusCode(500, new { Message = "Error inesperado", Error = ex.Message });
            }
        }
    }
}
