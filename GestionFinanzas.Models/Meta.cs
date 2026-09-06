using System;
using System.Collections.Generic;

namespace GestionFinanzasAPI.Models;

public partial class Meta
{
    public int IdMeta { get; set; }

    public int IdUsuario { get; set; }

    public string? NombreMeta { get; set; }

    public decimal? MontoObjetivo { get; set; }

    public decimal? SaldoActual { get; set; }

    public DateOnly? FechaLimite { get; set; }

    public bool EstadoActivoMeta { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
