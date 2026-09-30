using System.ComponentModel.DataAnnotations;

namespace GestionFinanzas.Services.DTOs.Requests
{
    public class CategoriaUpdateDTO
    {
        [Required]
        public int IdUsuario { get; set; }
        public string? NombreCategoria { get; set; }
        public string? TipoCategoria { get; set; }
    }
}
