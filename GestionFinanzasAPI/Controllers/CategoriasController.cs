using GestionFinanzas.Data;
using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionFinanzasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetAll(int idUsuario)
        {
            var categorias = await _categoriaService.GetAll(idUsuario);
            return Ok(categorias);
        }

        [HttpGet("{idCategoria}/usuario/{idUsuario}")]
        public async Task<IActionResult> GetById(int idCategoria, int idUsuario)
        {
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoriaCreada = await _categoriaService.Insert(categoriaDto);

            return CreatedAtAction(nameof(GetById), new { idUsuario = nuevaCategoria.IdUsuario }, respuestaDto);
        }

        [HttpPut("{idCategoria}")]
        public async Task<IActionResult> Update(int idCategoria, [FromBody] CategoriaUpdateDTO categoriaDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var categoriaActualizar = new Categoria
            {
                IdCategoria = idCategoria,
                NombreCategoria = categoriaDto.NombreCategoria
            };

            var catActualizada = await _categoriaService.Update(categoriaActualizar);
            if (catActualizada == null) return NotFound(new { mensaje = "La categoría no existe." });
            return Ok(MapearACategoriaResponseDTO(catActualizada));
        }

        [HttpDelete("{idCategoria}/usuario/{idUsuario}")]
        public async Task<IActionResult> Delete(int idCategoria, int idUsuario)
        {
            var catEliminada = await _categoriaService.Delete(idUsuario, idCategoria);

            if (!catEliminada)
            {
                return NotFound(new { mensaje = "La categoria no existe o no tienes permisos para eliminarla" });
            }
            return NoContent();
        }
    }
}
