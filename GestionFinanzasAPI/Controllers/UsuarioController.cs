using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using GestionFinanzasAPI.DTOs.Requests;
using GestionFinanzasAPI.DTOs.Responses;
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
            var usuariosDTO = usuarios.Select(u => MapearUsuarioResponseDTO(u)).ToList();
            return Ok(usuariosDTO);
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> GetById(int idUsuario)
        {
            var usuario = await _usuarioService.GetById(idUsuario);

            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontro ninggun usuario con ID: {idUsuario}" });
            }

            return Ok(MapearUsuarioResponseDTO(usuario));
        }

        [HttpGet("identificacion/{numIdentificacion}")]
        public async Task<IActionResult> GetByNI(string numIdentificacion)
        {
            var usuario = await _usuarioService.GetByNI(numIdentificacion);
            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontro un usuario con este numero de identificacion: {numIdentificacion}" });
            }
            return Ok(MapearUsuarioResponseDTO(usuario));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nuevoUsuario = new Usuario
            {
                NumeroIdentificacion = usuarioDTO.NumeroIdentificacion,
                NombreUsuario = usuarioDTO.NombreUsuario,
                ApellidoUsuario = usuarioDTO.ApellidoUsuario,
                CorreoUsuario = usuarioDTO.CorreoUsuario,
                ContraseñaUsuario = usuarioDTO.ContraseñaUsuario,
                EstadoActivoUsuario = true
            };
            var usuarioCreado = await _usuarioService.Insert(nuevoUsuario);
            var respuestaDto = MapearUsuarioResponseDTO(usuarioCreado);

            return CreatedAtAction(nameof(GetById), new { idUsuario = respuestaDto.IdUsuario }, respuestaDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioActualizado = await _usuarioService.Update(usuario);
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

        private UsuarioResponseDTO MapearUsuarioResponseDTO(Usuario usuario)
        {
            return new UsuarioResponseDTO
            {
                IdUsuario = usuario.IdUsuario,
                NumeroIdentificacion = usuario.NumeroIdentificacion,
                NombreUsuario = usuario.NombreUsuario,
                ApellidoUsuario = usuario.ApellidoUsuario,
                CorreoUsuario = usuario.CorreoUsuario
            };
        }
    }
}
