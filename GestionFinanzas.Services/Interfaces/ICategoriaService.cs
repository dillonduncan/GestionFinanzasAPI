using GestionFinanzas.Data;

namespace GestionFinanzas.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAll();
        Task<Categoria> Insert(Categoria categoria);
    }
}
