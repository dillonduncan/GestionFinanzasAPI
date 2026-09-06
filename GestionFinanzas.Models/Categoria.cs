using System;
using System.Collections.Generic;

namespace GestionFinanzasAPI.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public int? IdUsuario { get; set; }

    public string? NombreCategoria { get; set; }

    public bool EstadoActivoCategoria { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
