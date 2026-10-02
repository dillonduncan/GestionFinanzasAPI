using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;

namespace GestionFinanzas.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaResponseDTO>> GetAll(int idUsuario);
        Task<CategoriaResponseDTO> GetById(int idCategoria, int idUsuario);
        Task<CategoriaResponseDTO> Insert(int idUsuario, CategoriaCreateDTO categoria);
        Task<CategoriaResponseDTO> Update(int idCategoria, int idUsuario, CategoriaUpdateDTO categoria);
        Task<bool> Delete(int idUsuario, int idCategoria);
    }
}
