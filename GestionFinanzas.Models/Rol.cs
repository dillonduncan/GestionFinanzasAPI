namespace GestionFinanzas.Models
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
    }
}
