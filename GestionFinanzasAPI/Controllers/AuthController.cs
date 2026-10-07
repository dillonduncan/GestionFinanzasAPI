using FluentValidation;
using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionFinanzasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<LoginDTO> _loginValidator;
        private readonly IValidator<UsuarioCreateDTO> _createValidator;
        public AuthController(IAuthService authService, IUsuarioService usuarioService, IValidator<LoginDTO> loginValidator, IValidator<UsuarioCreateDTO> createValidator)
        {
            _authService = authService;
            _usuarioService = usuarioService;
            _loginValidator = loginValidator;
            _createValidator = createValidator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);

            if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

            var respuesta = await _authService.Login(loginDto);

            if (!respuesta.Exito)
            {
                return Unauthorized(new { mensaje = respuesta.Mensaje });
            }

            return Ok(respuesta);
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<IActionResult> Registro([FromBody] UsuarioCreateDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

            if (await _usuarioService.ExisteCorreo(dto.CorreoUsuario)) return BadRequest(new { mensaje = "El correo ya esta registrado." });

            var usuarioNuevo = await _usuarioService.Insert(dto);

            if (usuarioNuevo == null)
            {
                return BadRequest(new { mensaje = "Ocurrio un error al intentar registrar el usuario." });
            }

            return Ok(new { exito = true, mensaje = "Registro exitoso. Ya puedes iniciar sesion." });
        }
    }
}
