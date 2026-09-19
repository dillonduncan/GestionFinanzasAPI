using System.ComponentModel.DataAnnotations;

namespace GestionFinanzasAPI.DTOs.Requests
{
    public class MetaCreateDTO
    {
        [Required(ErrorMessage = "La descripción de la meta es obligatoria")]
        public string DescripcionMeta { get; set; }

        [Required(ErrorMessage = "El monto objetivo es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto objetivo debe ser mayor a cero")]
        public decimal MontoObjetivo { get; set; }

        public decimal MontoActual { get; set; } = 0;

        public DateTime? FechaLimite { get; set; }
    }
}
