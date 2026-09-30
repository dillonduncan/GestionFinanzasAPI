using System.ComponentModel.DataAnnotations;

namespace GestionFinanzas.Services.DTOs.Requests
{
    public class TransaccionUpdateDTO
    {
        [Required]
        public int IdUsuario { get; set; }
        public decimal? MontoTransaccion { get; set; }
        public string? DescripcionTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime? FechaTransaccion { get; set; }
        public int IdCategoria { get; set; }
    }
}
