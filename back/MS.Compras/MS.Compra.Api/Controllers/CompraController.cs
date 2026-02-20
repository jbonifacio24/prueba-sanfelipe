using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MS.Compra.Application.Facades;
using MS.Compra.Domain.Exceptions;
using MS.Compra.Application.UseCases;

namespace MS.Compra.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/compra")]
    public class CompraController : ControllerBase
    {
        private readonly OrderFacade _facade;

        public CompraController(OrderFacade facade)
        {
            _facade = facade;
        }

        [HttpGet("test")]
        public IActionResult Index()
        {
            return Ok("API Compra funcionando correctamente"); 
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOrderCabCommand input)
        {
            try
            {
                var id = await _facade.CreateOrderAsync(input);
                return Ok(new { IdCompraCab = id, Message = "Compra creada exitosamente" });
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
                var output = await _facade.GetAllOrdersAsync();
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
