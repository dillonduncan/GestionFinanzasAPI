using GestionFinanzas.Data;
using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionFinanzas.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly GestionFinanzasContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(GestionFinanzasContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<AuthResponseDTO> Login(LoginDTO loginDto)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.CorreoUsuario == loginDto.CorreoUsuario);

            if (usuario == null)
            {
                return new AuthResponseDTO
                {
                    Exito = false,
                    Mensaje = "Credenciales incorrectas."
                };
            }

            bool esPasswordCorrecta = BCrypt.Net.BCrypt.Verify(loginDto.ContraseñaUsuario, usuario.ContraseñaUsuario);

            if (!esPasswordCorrecta)
            {
                return new AuthResponseDTO
                {
                    Exito = false,
                    Mensaje = "Credenciales incorrectas."
                };
            }

            var roles = usuario.UsuarioRoles.Select(ur => ur.Rol.NombreRol).ToList();
            string token = GenerarJwtToken(usuario.IdUsuario.ToString(), usuario.CorreoUsuario, roles);

            return new AuthResponseDTO
            {
                Exito = true,
                Mensaje = "Inicio de Sesion Exitoso.",
                Token = token
            };
        }
        private string GenerarJwtToken(string idUuario, string correoUsuario, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, idUuario),
                new Claim(ClaimTypes.Email, correoUsuario)
            };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            //Traer la clave secreta desde el appsettings.json y convertirla a bytes
            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            //Definir el algoritmo de firma digital (HMAC-SHA256)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireMinutes = Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
