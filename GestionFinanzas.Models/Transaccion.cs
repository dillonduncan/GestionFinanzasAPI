namespace GestionFinanzas.Data;

public partial class Transaccion
{
    public int IdTransaccion { get; set; }

    public int IdUsuario { get; set; }

    public int IdCategoria { get; set; }

    public string? TipoTransaccion { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public string? DescripcionTransaccion { get; set; }

    public bool EstadoActivoTransaccion { get; set; } = true;

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
