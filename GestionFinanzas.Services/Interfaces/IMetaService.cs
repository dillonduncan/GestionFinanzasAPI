using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;

namespace GestionFinanzas.Services.Interfaces
{
    public interface IMetaService
    {
        Task<IEnumerable<MetaResponseDTO>> GetAll(int idUsuario);
        Task<MetaResponseDTO> GetById(int idMeta, int idUsuario);
        Task<MetaResponseDTO> Insert(MetaCreateDTO meta);
        Task<MetaResponseDTO> Update(int idMeta, MetaUpdateDTO meta);
        Task<bool> Delete(int idMeta, int idUsuario);
    }
}
