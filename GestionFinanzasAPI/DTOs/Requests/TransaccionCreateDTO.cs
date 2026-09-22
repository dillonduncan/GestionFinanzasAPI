using System.ComponentModel.DataAnnotations;

namespace GestionFinanzasAPI.DTOs.Requests
{
    public class TransaccionCreateDTO
    {
        [Required]
        public int IdTransaccion { get; set; }
        [Required]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El monto de la transacción es obligatorio")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string DescripcionTransaccion { get; set; }

        [Required(ErrorMessage = "El tipo de transaccion es obligatorio")]
        public string TipoTransaccion { get; set; }

        public DateTime FechaTransaccion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debe asociar la transacción a una categoría")]
        public int IdCategoria { get; set; }
    }
}
