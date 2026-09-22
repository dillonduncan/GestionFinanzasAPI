namespace GestionFinanzasAPI.DTOs.Requests
{
    public class TransaccionUpdateDTO
    {
        public decimal? MontoTransaccion { get; set; }
        public string? DescripcionTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime? FechaTransaccion { get; set; }
        public int IdCategoria { get; set; }
    }
}
