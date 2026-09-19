namespace GestionFinanzasAPI.DTOs.Responses
{
    public class MetaResponseDTO
    {
        public int IdMeta { get; set; }
        public string DescripcionMeta { get; set; }
        public decimal MontoObjetivo { get; set; }
        public decimal MontoActual { get; set; }
        public DateTime? FechaLimite { get; set; }
    }
}
