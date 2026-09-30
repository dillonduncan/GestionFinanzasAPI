using System.ComponentModel.DataAnnotations;

namespace GestionFinanzas.Services.DTOs.Requests
{
    public class MetaUpdateDTO
    {
        [Required]
        public int IdUsuario { get; set; }
        public string? NombreMeta { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto objetivo debe ser mayor a cero")]
        public decimal MontoObjetivo { get; set; }

        public decimal SaldoActual { get; set; }

        public DateTime? FechaLimite { get; set; }
    }
}
