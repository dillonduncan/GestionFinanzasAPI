using System.ComponentModel.DataAnnotations;

namespace GestionFinanzasAPI.DTOs.Requests
{
    public class MetaCreateDTO
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "La descripción de la meta es obligatoria")]
        public string NombreMeta { get; set; }

        [Required(ErrorMessage = "El monto objetivo es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto objetivo debe ser mayor a cero")]
        public decimal MontoObjetivo { get; set; }

        public decimal SaldoActual { get; set; } = 0;

        public DateTime? FechaLimite { get; set; }
    }
}
