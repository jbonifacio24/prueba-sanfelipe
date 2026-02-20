using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MS.Venta.Application.Facades;
using MS.Venta.Domain.Exceptions;
using MS.Venta.Application.UseCases;

namespace MS.Venta.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/venta")]
    public class VentaController : ControllerBase
    {
        private readonly SaleFacade _facade;

        public VentaController(SaleFacade facade)
        {
            _facade = facade;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("API Venta funcionando correctamente"); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleCabCommand input)
        {
            try
            {
                var output = await _facade.CreateSaleAsync(input);
                return Ok("Venta creado exitosamente");
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
