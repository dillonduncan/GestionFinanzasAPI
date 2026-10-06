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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        private int ObtenerIdUsuarioToken()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim)) throw new UnauthorizedAccessException("El token no trae el ID del usuario.");
            return int.Parse(idClaim);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.GetById(id);

            if (usuario == null) return NotFound(new { mensaje = $"No se encontro ningun usuario con ID: {id}." });
            return Ok(usuario);
        }

        [HttpGet("mi-perfil")]
        public async Task<IActionResult> GetMiPerfil()
        {
            var idUsuario = ObtenerIdUsuarioToken();
            var usuario = await _usuarioService.GetById(idUsuario);

            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontro ningun usuario con ID: {idUsuario}" });
            }

            return Ok(usuario);
        }

        [HttpGet("identificacion/{numIdentificacion}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByNI(string numIdentificacion)
        {
            var usuario = await _usuarioService.GetByNI(numIdentificacion);
            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontro un usuario con este numero de identificacion: {numIdentificacion}" });
            }
            return Ok(usuario);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usuarioCreado = await _usuarioService.Insert(usuarioDTO);

            return Ok(new { exito = true, mensaje = "Usuario creado exitosamente, Admin." });
        }



        [HttpPut("mi-perfil")]
        public async Task<IActionResult> Update([FromBody] UsuarioUpdateDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int idUsuario = ObtenerIdUsuarioToken();


            var usuarioActualizado = await _usuarioService.Update(idUsuario, usuarioDTO);
            if (usuarioActualizado == null)
            {
                return NotFound(new { mensaje = $"El usuario con ID {idUsuario} no existe." });
            }
            return Ok(usuarioActualizado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UsuarioUpdateDTO dto)
        {
            var usuarioActualizado = await _usuarioService.Update(id, dto);

            if (usuarioActualizado == null) return NotFound(new { mensaje = $"No se encontro el usuario con ID {id}." });

            return Ok(new { exito = true, mensaje = "Datos actualizados correctamente." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var usuarioExiste = await _usuarioService.Delete(id);
            if (!usuarioExiste) return NotFound(new { mensaje = $"El usuario con ID: {id} no existe o ya fue eliminado." });
            return NoContent();
        }


        [HttpDelete("mi-perfil")]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var idUsuario = ObtenerIdUsuarioToken();
            var exito = await _usuarioService.Delete(idUsuario);

            if (!exito)
            {
                return NotFound(new { mensaje = $"El usuario con ID: {idUsuario} no existe o ya fue eliminado" });
            }

            return NoContent();
        }
    }
}
