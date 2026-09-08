using GestionFinanzas.Data;

namespace GestionFinanzas.Services.Interfaces
{
    public interface ITransaccionesService
    {
        Task<IEnumerable<Transaccione>> GetAll(int idUsuario);
        Task<IEnumerable<Transaccione>> GetForCategoria(int idUsuario, int idCategoria);
        Task<Transaccione> GetById(int id, int idUsuario);
        Task<Transaccione> Insert(Transaccione transaccion);
        Task<Transaccione> Update(Transaccione transaccione);
        Task<bool> Delete(int idTransaccion, int idUsuario);
    }
}
