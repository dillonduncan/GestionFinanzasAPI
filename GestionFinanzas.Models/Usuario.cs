using System;
using System.Collections.Generic;

namespace GestionFinanzasAPI.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public string? NombreUsuario { get; set; }

    public string? ApellidoUsuario { get; set; }

    public string CorreoUsuario { get; set; } = null!;

    public string ContraseñaUsuario { get; set; } = null!;

    public bool EstadoActivoUsuario { get; set; }

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();

    public virtual ICollection<Meta> Meta { get; set; } = new List<Meta>();

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
