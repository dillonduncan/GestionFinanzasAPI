namespace GestionFinanzas.Services.DTOs.Requests
{
    public class MetaCreateDTO
    {
        public string NombreMeta { get; set; }
        public decimal MontoObjetivo { get; set; }
        public decimal SaldoActual { get; set; } = 0;
        public DateTime? FechaLimite { get; set; }
    }
}
