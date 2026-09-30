using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;

namespace GestionFinanzas.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDTO>> GetAll();
        Task<UsuarioResponseDTO> GetById(int id);
        Task<UsuarioResponseDTO> GetByNI(string numIdentificacion);
        Task<UsuarioResponseDTO> Insert(UsuarioCreateDTO usuario);
        Task<UsuarioResponseDTO> Update(int idUsuario, UsuarioUpdateDTO usuario);
        Task<bool> Delete(int idUsuario);

    }
}
