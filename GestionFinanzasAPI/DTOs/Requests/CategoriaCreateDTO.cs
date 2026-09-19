using System.ComponentModel.DataAnnotations;

namespace GestionFinanzasAPI.DTOs.Requests
{
    public class CategoriaCreateDTO
    {
        [Required(ErrorMessage = "EL nombre de la categoria es obligatorio.")]
        public string NombreCategoria { get; set; }

        [Required(ErrorMessage = "EL tipo de categoria es obligatorio (Ingreso/Egreso).")]
        public string TipoCategoria { get; set; }
    }
}
