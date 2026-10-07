using FluentValidation;
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
    public class MetasController : ControllerBase
    {
        private readonly IMetaService _metaService;
        private readonly IValidator<MetaCreateDTO> _createValidator;
        private readonly IValidator<MetaUpdateDTO> _updateValidator;
        public MetasController(IMetaService metaService, IValidator<MetaCreateDTO> createValidator, IValidator<MetaUpdateDTO> updateValidator)
        {
            _metaService = metaService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        private int ObtenerIdUsuarioToken()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim)) throw new UnauthorizedAccessException("El token no trae el ID del usuario.");
            return int.Parse(idClaim);
        }

        [HttpGet("mis-metas")]
        public async Task<IActionResult> GetAll()
        {
            var idUsuario = ObtenerIdUsuarioToken();
            var metas = await _metaService.GetAll(idUsuario);

            return Ok(metas);
        }

        [HttpGet("{idMeta}")]
        public async Task<IActionResult> GetById(int idMeta)
        {
            var idUsuario = ObtenerIdUsuarioToken();
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
            var validationResult = await _createValidator.ValidateAsync(metaDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

            var idUsuario = ObtenerIdUsuarioToken();

            var metaCreada = await _metaService.Insert(idUsuario, metaDto);
            return CreatedAtAction(nameof(GetById), new { idMeta = metaCreada.IdMeta }, metaCreada);
        }

        [HttpPut("{idMeta}")]
        public async Task<IActionResult> Update(int idMeta, [FromBody] MetaUpdateDTO metaDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(metaDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

            var idUsuario = ObtenerIdUsuarioToken();


            var metaActualizada = await _metaService.Update(idMeta, idUsuario, metaDto);

            if (metaActualizada == null)
            {
                return NotFound(new { mensaje = $"La meta de ahoro no existe." });
            }
            return Ok(metaActualizada);
        }

        [HttpDelete("{idMeta}")]
        public async Task<IActionResult> Delete(int idMeta)
        {
            var idUsuario = ObtenerIdUsuarioToken();

            var metaEliminado = await _metaService.Delete(idMeta, idUsuario);
            if (!metaEliminado)
            {
                return NotFound(new { mensaje = $"La meta no existe o no tienes permiso para eliminarla." });
            }
            return NoContent();
        }
    }
}
