using System.ComponentModel.DataAnnotations;

namespace GestionFinanzasAPI.DTOs.Requests
{
    public class TransaccionCreateDTO
    {
        [Required(ErrorMessage = "El monto de la transacción es obligatorio")]
        public decimal MontoTransaccion { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string DescripcionTransaccion { get; set; }

        public DateTime FechaTransaccion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debe asociar la transacción a una categoría")]
        public int IdCategoria { get; set; }
    }
}
