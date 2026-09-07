using GestionFinanzas.Data;

namespace GestionFinanzas.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(int id);
        Task<Usuario> GetByNI(string numIdentificacion);
        Task<Usuario> Insert(Usuario usuario);
        Task<Usuario> Update(Usuario usuario);
        Task<bool> Delete(int idUsuario);

    }
}
