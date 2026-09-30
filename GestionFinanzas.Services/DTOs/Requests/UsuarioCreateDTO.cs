using System.ComponentModel.DataAnnotations;

namespace GestionFinanzas.Service.DTOs.Requests
{
    public class UsuarioCreateDTO
    {
        [Required(ErrorMessage = "El numero de identificacion es obligatorio.")]
        public string NumeroIdentificacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string ApellidoUsuario { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress]
        public string CorreoUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string ContraseñaUsuario { get; set; }
    }
}
