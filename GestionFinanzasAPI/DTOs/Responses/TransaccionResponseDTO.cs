namespace GestionFinanzasAPI.DTOs.Responses
{
    public class TransaccionResponseDTO
    {
        public int IdTransaccion { get; set; }
        public decimal MontoTransaccion { get; set; }
        public string DescripcionTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; }

        // Incluimos información básica de la categoría relacionada para que el frontend la muestre sin hacer peticiones extra
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
    }
}
