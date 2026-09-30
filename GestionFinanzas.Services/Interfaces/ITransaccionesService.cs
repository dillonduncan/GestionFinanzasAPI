using GestionFinanzas.Data;

namespace GestionFinanzas.Services.Interfaces
{
    public interface ITransaccionesService
    {
        Task<IEnumerable<Transaccion>> GetAll(int idUsuario);
        Task<IEnumerable<Transaccion>> GetForCategoria(int idUsuario, int idCategoria);
        Task<Transaccion> GetById(int id, int idUsuario);
        Task<Transaccion> Insert(Transaccion transaccion);
        Task<Transaccion> Update(Transaccion transaccione);
        Task<bool> Delete(int idTransaccion, int idUsuario);
    }
}
