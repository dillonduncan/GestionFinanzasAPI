using System.ComponentModel.DataAnnotations;

namespace GestionFinanzasAPI.DTOs.Requests
{
    public class UsuarioUpdateDTO
    {
        [Required(ErrorMessage = "El ID usuario es obligatorio para actualizar.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El numero de identificacion es obligatorio.")]
        public string NumeroIdentificacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string ApellidoUsuario { get; set; }

        [Required, EmailAddress]
        public string CorreoUsuario { get; set; }

        public string? ContraseñaUsuario { get; set; }
    }
}
