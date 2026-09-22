using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using GestionFinanzasAPI.DTOs.Requests;
using GestionFinanzasAPI.DTOs.Responses;
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

            if (transacciones == null || !transacciones.Any())
            {
                return NotFound(new { mensaje = $"No se encontraron transaciones registradas para el usuario con ID: {idUsuario}" });
            }

            var transaccionDto = transacciones.Select(t => MapearTransaccionResponseDTO(t)).ToList();
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

            return Ok(MapearTransaccionResponseDTO(transaccion));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransaccionCreateDTO transaccionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevaTransaccion = new Transaccione
            {
                IdTransaccion = transaccionDto.IdTransaccion,
                IdUsuario = transaccionDto.IdUsuario,
                Monto = transaccionDto.Monto,
                TipoTransaccion = transaccionDto.TipoTransaccion,
                FechaTransaccion = transaccionDto.FechaTransaccion == default(DateTime) ? DateTime.Now : transaccionDto.FechaTransaccion,
                DescripcionTransaccion = transaccionDto.DescripcionTransaccion,
                EstadoActivoTransaccion = true,
                IdCategoria = transaccionDto.IdCategoria
            };

            var transaccionCreada = await _transaccionesService.Insert(nuevaTransaccion);
            var respuestaDto = MapearTransaccionResponseDTO(transaccionCreada);
            return CreatedAtAction(nameof(GetById),
                new { idTransaccion = transaccionCreada.IdTransaccion, idUsuario = nuevaTransaccion.IdUsuario },
                respuestaDto);
        }

        [HttpPut("{idTransaccion")]
        public async Task<IActionResult> Update(int idTransaccion, [FromBody] TransaccionUpdateDTO transaccionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaccionActualizar = new Transaccione
            {
                IdTransaccion = idTransaccion,
                IdCategoria = transaccionDto.IdCategoria,
                Monto = transaccionDto.MontoTransaccion,
                TipoTransaccion = transaccionDto.TipoTransaccion,
                FechaTransaccion = transaccionDto.FechaTransaccion,
                DescripcionTransaccion = transaccionDto.DescripcionTransaccion
            };

            var transaccionActualizada = await _transaccionesService.Update(transaccionActualizar);

            if (transaccionActualizada == null)
            {
                return NotFound(new { mensaje = "La transaccion no existe." });
            }
            return Ok(MapearTransaccionResponseDTO(transaccionActualizada));
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

        private TransaccionResponseDTO MapearTransaccionResponseDTO(Transaccione transaccion)
        {
            return new TransaccionResponseDTO
            {
                IdTransaccion = transaccion.IdTransaccion,
                Monto = transaccion.Monto,
                TipoTransaccion = transaccion.TipoTransaccion,
                FechaTransaccion = transaccion.FechaTransaccion,
                DescripcionTransaccion = transaccion.DescripcionTransaccion,
                IdCategoria = transaccion.IdCategoria,
                NombreCategoria = transaccion.IdCategoriaNavigation?.NombreCategoria ?? "Sin Categoria"
            };
        }
    }
}
