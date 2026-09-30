using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;

namespace GestionFinanzas.Services.Interfaces
{
    public interface ITransaccionesService
    {
        Task<IEnumerable<TransaccionResponseDTO>> GetAll(int idUsuario);
        Task<IEnumerable<TransaccionResponseDTO>> GetForCategoria(int idCategoria, int idUsuario);
        Task<TransaccionResponseDTO> GetById(int id, int idUsuario);
        Task<TransaccionResponseDTO> Insert(TransaccionCreateDTO transaccion);
        Task<TransaccionResponseDTO> Update(int idTransaccion, TransaccionUpdateDTO transaccion);
        Task<bool> Delete(int idTransaccion, int idUsuario);
    }
}
