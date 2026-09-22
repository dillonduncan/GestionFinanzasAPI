namespace GestionFinanzasAPI.DTOs.Responses
{
    public class TransaccionResponseDTO
    {
        public int IdTransaccion { get; set; }
        public decimal? Monto { get; set; }
        public string? DescripcionTransaccion { get; set; }
        public string? TipoTransaccion { get; set; }
        public DateTime? FechaTransaccion { get; set; }
        public int? IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
    }
}
