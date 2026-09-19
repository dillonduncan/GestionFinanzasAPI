using System.ComponentModel.DataAnnotations;

namespace GestionFinanzasAPI.DTOs.Requests
{
    public class MetaUpdateDTO
    {
        public string? DescripcionMeta { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto objetivo debe ser mayor a cero")]
        public decimal? MontoObjetivo { get; set; }

        public decimal? MontoActual { get; set; }

        public DateTime? FechaLimite { get; set; }
    }
}
