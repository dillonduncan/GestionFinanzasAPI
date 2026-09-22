namespace GestionFinanzasAPI.DTOs.Requests
{
    public class CategoriaUpdateDTO
    {
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public string? TipoCategoria { get; set; }
    }
}
