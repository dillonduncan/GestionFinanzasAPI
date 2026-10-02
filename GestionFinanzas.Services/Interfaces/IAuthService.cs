using GestionFinanzas.Services.DTOs.Requests;

namespace GestionFinanzas.Services.Interfaces
{
    public interface IAuthService
    {
        Task Login(LoginDTO loginDto);
    }
}
