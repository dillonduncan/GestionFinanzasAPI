using GestionFinanzas.Data;

namespace GestionFinanzas.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAll(int idUsuario);
        Task<Categoria> Insert(Categoria categoria);
        Task<Categoria> Update(Categoria categoria);
        Task<bool> Delete(int idUsuario, int idCategoria);
    }
}
