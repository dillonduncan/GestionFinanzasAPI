using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.Interfaces;
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

            return Ok(metas);
        }

        [HttpGet("{idMeta}/usuario/{idUsuario}")]
        public async Task<IActionResult> GetById(int idMeta, int idUsuario)
        {
            var meta = await _metaService.GetById(idMeta, idUsuario);

            if (meta == null)
            {
                return NotFound(new { mensaje = $"No se encontro ningun usuario con ID: {idUsuario}" });
            }
            return Ok(meta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MetaCreateDTO metaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var metaCreada = await _metaService.Insert(metaDto);
            return CreatedAtAction(nameof(GetById), new { idMeta = metaCreada.IdMeta }, metaCreada);
        }

        [HttpPut("{idMeta}")]
        public async Task<IActionResult> Update(int idMeta, [FromBody] MetaUpdateDTO metaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var metaActualizada = await _metaService.Update(idMeta, metaDto);

            if (metaActualizada == null)
            {
                return NotFound(new { mensaje = $"La meta de ahoro no existe." });
            }
            return Ok(metaActualizada);
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
    }
}
