using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MS.Movimiento.Application.Facades;
using MS.Movimiento.Domain.Exceptions;
using MS.Movimiento.Application.UseCases;

namespace MS.Movimiento.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/movimiento")]
    public class MovimientoController : ControllerBase
    {
        private readonly MovementFacade _facade;
        public MovimientoController(MovementFacade facade)
        {
            _facade = facade;
        }
        [HttpGet("test")]
        public IActionResult Index()
        {
            return Ok("API Movimiento funcionando correctamente"); // <-- Retorna un resultado de API
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateMovementCabCommand input)
        {
            try
            {
                await _facade.CreateMovementAsync(input);
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
    }
}
