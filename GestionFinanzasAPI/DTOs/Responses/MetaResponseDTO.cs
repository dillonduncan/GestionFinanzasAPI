namespace GestionFinanzasAPI.DTOs.Responses
{
    public class MetaResponseDTO
    {
        public int IdMeta { get; set; }
        public string? NombreMeta { get; set; }
        public decimal? MontoObjetivo { get; set; }
        public decimal? SaldoActual { get; set; }
        public DateTime? FechaLimite { get; set; }
        public decimal? PorcentajeActual { get; set; }
    }
}
