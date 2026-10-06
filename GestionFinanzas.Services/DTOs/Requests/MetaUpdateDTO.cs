namespace GestionFinanzas.Services.DTOs.Requests
{
    public class MetaUpdateDTO
    {
        public string? NombreMeta { get; set; }
        public decimal MontoObjetivo { get; set; }
        public decimal SaldoActual { get; set; }
        public DateTime? FechaLimite { get; set; }
    }
}
