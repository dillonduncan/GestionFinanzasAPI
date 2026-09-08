using GestionFinanzas.Data;

namespace GestionFinanzas.Services.Interfaces
{
    public interface IMetaService
    {
        Task<IEnumerable<Meta>> GetAll(int idUsuario);
        Task<Meta> GetById(int idMeta, int idUsuario);
        Task<Meta> Insert(Meta meta);
        Task<Meta> Update(Meta meta);
        Task<bool> Delete(int idMeta, int idUsuario);
    }
}
