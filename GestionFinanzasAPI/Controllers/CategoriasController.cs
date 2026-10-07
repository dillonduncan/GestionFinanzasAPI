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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IValidator<CategoriaCreateDTO> _createValidator;
        private readonly IValidator<CategoriaUpdateDTO> _updateValidator;
        public CategoriasController(ICategoriaService categoriaService, IValidator<CategoriaCreateDTO> createValidator, IValidator<CategoriaUpdateDTO> updateValidator)
        {
            _categoriaService = categoriaService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        private int ObtenerIdUsuarioToken()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim)) throw new UnauthorizedAccessException("El token no trae el ID del usuario.");

            return int.Parse(idClaim);
        }

        [HttpGet("mis-categorias")]
        public async Task<IActionResult> GetAll()
        {
            int idUsuario = ObtenerIdUsuarioToken();
            var categorias = await _categoriaService.GetAll(idUsuario);
            return Ok(categorias);
        }

        [HttpGet("{idCategoria}")]
        public async Task<IActionResult> GetById(int idCategoria)
        {
            var idUsuario = ObtenerIdUsuarioToken();
            var categoria = await _categoriaService.GetById(idUsuario, idCategoria);
            if (categoria == null)
            {
                return NotFound(new { mensaje = "Categoría no encontrada o no tienes permisos para verla." });
            }

            return Ok(categoria);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaCreateDTO categoriaDto)
        {
            var validationResult = await _createValidator.ValidateAsync(categoriaDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

            int idUsuario = ObtenerIdUsuarioToken();

            var categoriaCreada = await _categoriaService.Insert(idUsuario, categoriaDto);

            return CreatedAtAction(nameof(GetById), new { idCategoria = categoriaCreada.IdCategoria }, categoriaCreada);
        }

        [HttpPut("{idCategoria}")]
        public async Task<IActionResult> Update(int idCategoria, [FromBody] CategoriaUpdateDTO categoriaDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(categoriaDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

            int idUsuario = ObtenerIdUsuarioToken();

            var categoriaActualizada = await _categoriaService.Update(idCategoria, idUsuario, categoriaDto);

            if (categoriaActualizada == null) return NotFound(new { mensaje = "La categoría no existe." });
            return Ok(categoriaActualizada);
        }

        [HttpDelete("{idCategoria}")]
        public async Task<IActionResult> Delete(int idCategoria)
        {
            int idUsuario = ObtenerIdUsuarioToken();

            var catEliminada = await _categoriaService.Delete(idUsuario, idCategoria);

            if (!catEliminada)
            {
                return NotFound(new { mensaje = "La categoria no existe o no tienes permisos para eliminarla" });
            }
            return NoContent();
        }
    }
}
