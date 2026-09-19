using System.ComponentModel.DataAnnotations;

namespace GestionFinanzasAPI.DTOs.Requests
{
    public class UsuarioUpdateDTO
    {
        public string? NumeroIdentificacion { get; set; }

        public string? NombreUsuario { get; set; }

        public string? ApellidoUsuario { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string? CorreoUsuario { get; set; }

        public string? ContraseñaUsuario { get; set; }
    }
}
