using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionFinanzasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : Controller
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
            if (categorias == null)
            {
                return NotFound(new { mensaje = $"No se encontro ningun usuario con ID: {idUsuario}" });
            }

            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nuevaCategoria = await _categoriaService.Insert(categoria);

            return CreatedAtAction(nameof(GetAll), new { idUsuario = nuevaCategoria.IdUsuario }, nuevaCategoria);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var catActualizada = await _categoriaService.Update(categoria);
            return Ok(catActualizada);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idCategoria, int idUsuario)
        {
            var catEliminada = await _categoriaService.Delete(idUsuario, idCategoria);

            if (!catEliminada)
            {
                return NotFound(new { mensajke = "La categoria no existe o no tienes permisos para eliminarla" });
            }
            return NoContent();
        }
    }
}
