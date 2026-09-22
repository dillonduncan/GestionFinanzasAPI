using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using GestionFinanzasAPI.DTOs.Requests;
using GestionFinanzasAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GestionFinanzasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetasController : ControllerBase
    {
        private readonly IMetaService _metaService;
        public MetasController(IMetaService metaService)
        {
            _metaService = metaService;
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetAll(int idUsuario)
        {
            var metas = await _metaService.GetAll(idUsuario);
            if (metas == null || !metas.Any())
            {
                return NotFound(new { mensaje = $"No se encontraron metas de ahorro para el usuario con ID: {idUsuario}" });
            }

            var metasDto = metas.Select(m => MapearMetaResponseDTO(m)).ToList();
            return Ok(metasDto);
        }

        [HttpGet("{idMeta}/usuario/{idUsuario}")]
        public async Task<IActionResult> GetById(int idMeta, int idUsuario)
        {
            var meta = await _metaService.GetById(idMeta, idUsuario);

            if (meta == null)
            {
                return NotFound(new { mensaje = $"No se encontro ningun usuario con ID: {idUsuario}" });
            }
            return Ok(MapearMetaResponseDTO(meta));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MetaCreateDTO metaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevaMeta = new Meta
            {
                IdUsuario = metaDto.IdUsuario,
                NombreMeta = metaDto.NombreMeta,
                MontoObjetivo = metaDto.MontoObjetivo,
                SaldoActual = metaDto.SaldoActual,
                FechaLimite = metaDto.FechaLimite,
                EstadoActivoMeta = true
            };

            var metaCreada = await _metaService.Insert(nuevaMeta);
            var respuestaDto = MapearMetaResponseDTO(metaCreada);
            return CreatedAtAction(nameof(GetById), new { idUsuario = nuevaMeta.IdUsuario }, respuestaDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int idMeta, [FromBody] MetaUpdateDTO metaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var metaActualizar = new Meta
            {
                IdMeta = metaDto.IdMeta,
                NombreMeta = metaDto.NombreMeta,
                MontoObjetivo = metaDto.MontoObjetivo,
                SaldoActual = metaDto.SaldoActual,
                FechaLimite = metaDto.FechaLimite
            };

            var metaActualizada = await _metaService.Update(metaActualizar);

            if (metaActualizada == null)
            {
                return NotFound(new { mensaje = $"La meta de ahoro no existe." });
            }
            return Ok(MapearMetaResponseDTO(metaActualizada));
        }

        [HttpDelete("{idMeta}/usuario/{idUsuario}")]
        public async Task<IActionResult> Delete(int idMeta, int idUsuario)
        {
            var metaEliminado = await _metaService.Delete(idMeta, idUsuario);
            if (!metaEliminado)
            {
                return NotFound(new { mensaje = $"La meta no existe o no tienes permiso para eliminarla." });
            }
            return NoContent();
        }

        private MetaResponseDTO MapearMetaResponseDTO(Meta meta)
        {
            return new MetaResponseDTO
            {
                IdMeta = meta.IdMeta,
                NombreMeta = meta.NombreMeta,
                MontoObjetivo = meta.MontoObjetivo,
                SaldoActual = meta.SaldoActual,
                FechaLimite = meta.FechaLimite,
                PorcentajeActual = meta.MontoObjetivo > 0 ? Math.Round((meta.SaldoActual / meta.MontoObjetivo) * 100, 2) : 0


            };
        }
    }
}
