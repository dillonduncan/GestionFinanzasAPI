using System.ComponentModel.DataAnnotations;

namespace GestionFinanzas.Services.DTOs.Requests
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Hey, el correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Compa, el formato de correo no es valido.")]
        public string CorreoUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria, compae.")]
        public string ContraseñaUsuario { get; set; }
    }
}
