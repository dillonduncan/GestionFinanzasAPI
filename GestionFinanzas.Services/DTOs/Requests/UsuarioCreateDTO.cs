namespace GestionFinanzas.Services.DTOs.Requests
{
    public class UsuarioCreateDTO
    {
        public string NumeroIdentificacion { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ContraseñaUsuario { get; set; }
    }
}
