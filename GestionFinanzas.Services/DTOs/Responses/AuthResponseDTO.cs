namespace GestionFinanzas.Services.DTOs.Responses
{
    public class AuthResponseDTO
    {
        public bool Exito { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}
