namespace GestionFinanzasAPI.DTOs.Responses
{
    public class UsuarioResponseDTO
    {
        public int IdUsuario { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
    }
}
