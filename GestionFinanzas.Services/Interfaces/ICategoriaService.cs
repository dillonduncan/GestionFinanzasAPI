using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;

namespace GestionFinanzas.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaResponseDTO>> GetAll(int idUsuario);
        Task<CategoriaResponseDTO> GetById(int idCategoria, int idUsuario);
        Task<CategoriaResponseDTO> Insert(CategoriaCreateDTO categoria);
        Task<CategoriaResponseDTO> Update(int idCategoria, CategoriaUpdateDTO categoria);
        Task<bool> Delete(int idUsuario, int idCategoria);
    }
}
