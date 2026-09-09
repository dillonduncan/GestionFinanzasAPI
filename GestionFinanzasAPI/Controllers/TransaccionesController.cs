using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionFinanzasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionesService _transaccionesService;
        public TransaccionesController(ITransaccionesService transaccionesService)
        {
            _transaccionesService = transaccionesService;
        }
        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetAll(int idUsuario)
        {
            var transacciones = await _transaccionesService.GetAll(idUsuario);
            return Ok(transacciones);
        }

        [HttpGet("{idTransaccion}/usuario/{idUsuario}")]
        public async Task<IActionResult> GetById(int idTransaccion, int idUsuario)
        {
            var transaccion = await _transaccionesService.GetById(idTransaccion, idUsuario);
            if (transaccion == null)
            {
                return NotFound(new { mensaje = $"No se encontro ninguna transaccion con ID: {idTransaccion} para el usuario con ID: {idUsuario}" });
            }

            return Ok(transaccion);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transaccione transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevaTransaccion = await _transaccionesService.Insert(transaccion);
            return CreatedAtAction(nameof(GetById), new { idUsuario = nuevaTransaccion.IdUsuario }, nuevaTransaccion);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Transaccione transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaccionActualizada = await _transaccionesService.Update(transaccion);
            return Ok(transaccionActualizada);
        }

        [HttpDelete("{idTransaccion}/usuario/{idUsuario}")]
        public async Task<IActionResult> Delete(int idTransaccion, int idUsuario)
        {
            var transaccionEliminada = await _transaccionesService.Delete(idTransaccion, idUsuario);
            if (!transaccionEliminada)
            {
                return NotFound(new { mensaje = $"La transaccion no existe o no tienes permiso para eliminarla" });
            }
            return NoContent();
        }
    }
}
