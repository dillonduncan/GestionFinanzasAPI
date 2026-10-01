using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionFinanzasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> GetById(int idUsuario)
        {
            var usuario = await _usuarioService.GetById(idUsuario);

            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontro ninggun usuario con ID: {idUsuario}" });
            }

            return Ok(usuario);
        }

        [HttpGet("identificacion/{numIdentificacion}")]
        public async Task<IActionResult> GetByNI(string numIdentificacion)
        {
            var usuario = await _usuarioService.GetByNI(numIdentificacion);
            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontro un usuario con este numero de identificacion: {numIdentificacion}" });
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuarioCreado = await _usuarioService.Insert(usuarioDTO);

            return CreatedAtAction(nameof(GetById), new { idUsuario = usuarioCreado.IdUsuario }, usuarioCreado);
        }

        [HttpPut("{idUsuario}")]
        public async Task<IActionResult> Update(int idUsuario, [FromBody] UsuarioUpdateDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioActualizado = await _usuarioService.Update(idUsuario, usuarioDTO);
            if (usuarioActualizado == null)
            {
                return NotFound(new { mensaje = $"El usuario con ID {idUsuario} no existe." });
            }
            return Ok(usuarioActualizado);
        }

        [HttpDelete("{idUsuario}")]
        public async Task<IActionResult> Delete(int idUsuario)
        {
            var exito = await _usuarioService.Delete(idUsuario);

            if (!exito)
            {
                return NotFound(new { mensaje = $"El usuario con ID: {idUsuario} no existe o ya fue eliminado" });
            }

            return NoContent();
        }
    }
}
