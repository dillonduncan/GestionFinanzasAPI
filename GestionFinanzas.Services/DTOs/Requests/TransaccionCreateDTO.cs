namespace GestionFinanzas.Services.DTOs.Requests
{
    public class TransaccionCreateDTO
    {
        public decimal Monto { get; set; }
        public string DescripcionTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; } = DateTime.Now;
        public int IdCategoria { get; set; }
    }
}
