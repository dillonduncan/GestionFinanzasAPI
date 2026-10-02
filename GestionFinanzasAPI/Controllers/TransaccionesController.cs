using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestionFinanzasAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionesService _transaccionesService;
        public TransaccionesController(ITransaccionesService transaccionesService)
        {
            _transaccionesService = transaccionesService;
        }
        private int ObtenerIdUsuarioToken()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim)) throw new UnauthorizedAccessException("El token no trae el ID del usuario.");
            return int.Parse(idClaim);
        }

        [HttpGet("mis-transacciones")]
        public async Task<IActionResult> GetAll()
        {
            var idUsuario = ObtenerIdUsuarioToken();

            var transacciones = await _transaccionesService.GetAll(idUsuario);

            if (transacciones == null || !transacciones.Any())
            {
                return NotFound(new { mensaje = $"No se encontraron transaciones registradas para el usuario con ID: {idUsuario}" });
            }
            return Ok(transacciones);
        }

        [HttpGet("categoria/{idCategoria}")]
        public async Task<IActionResult> GetForCategoria(int idCategoria)
        {
            var idUsuario = ObtenerIdUsuarioToken();

            var transacciones = await _transaccionesService.GetForCategoria(idUsuario, idCategoria);

            if (transacciones == null || !transacciones.Any())
            {
                return NotFound(new { mensaje = $"No se encontraron transacciones en esta categoría para el usuario con ID: {idUsuario}" });
            }

            return Ok(transacciones);
        }

        [HttpGet("{idTransaccion}")]
        public async Task<IActionResult> GetById(int idTransaccion)
        {
            var idUsuario = ObtenerIdUsuarioToken();

            var transaccion = await _transaccionesService.GetById(idTransaccion, idUsuario);
            if (transaccion == null)
            {
                return NotFound(new { mensaje = $"No se encontro ninguna transaccion con ID: {idTransaccion} para el usuario con ID: {idUsuario}" });
            }

            return Ok(transaccion);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransaccionCreateDTO transaccionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var idUsuario = ObtenerIdUsuarioToken();

            var transaccionCreada = await _transaccionesService.Insert(idUsuario, transaccionDto);

            return CreatedAtAction(nameof(GetById),
                new { idTransaccion = transaccionCreada.IdTransaccion },
                transaccionCreada);
        }

        [HttpPut("{idTransaccion}")]
        public async Task<IActionResult> Update(int idTransaccion, [FromBody] TransaccionUpdateDTO transaccionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUsuario = ObtenerIdUsuarioToken();

            var transaccionActualizada = await _transaccionesService.Update(idTransaccion, idUsuario, transaccionDto);

            if (transaccionActualizada == null)
            {
                return NotFound(new { mensaje = "La transaccion no existe." });
            }
            return Ok(transaccionActualizada);
        }

        [HttpDelete("{idTransaccion}")]
        public async Task<IActionResult> Delete(int idTransaccion)
        {
            var idUsuario = ObtenerIdUsuarioToken();

            var transaccionEliminada = await _transaccionesService.Delete(idTransaccion, idUsuario);
            if (!transaccionEliminada)
            {
                return NotFound(new { mensaje = $"La transaccion no existe o no tienes permiso para eliminarla" });
            }
            return NoContent();
        }
    }
}
