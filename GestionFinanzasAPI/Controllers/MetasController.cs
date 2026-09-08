using GestionFinanzas.Data;
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
        public async Task<IActionResult> Create([FromBody] Meta meta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevaMeta = await _metaService.Insert(meta);
            return CreatedAtAction(nameof(GetAll), new { idUsuario = nuevaMeta.IdUsuario }, nuevaMeta);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Meta meta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var metaActualizada = await _metaService.Update(meta);
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
