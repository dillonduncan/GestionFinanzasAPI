using FluentValidation;
using GestionFinanzas.Services.DTOs.Requests;

namespace GestionFinanzas.Services.Validators.Auth
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(r => r.CorreoUsuario)
                .NotEmpty().WithMessage("El correo es obligatorio, compa.")
                .EmailAddress().WithMessage("El formato del correo no es valido.");

            RuleFor(r => r.ContraseñaUsuario)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener minimo 6 caracteres.")
                .When(r => !string.IsNullOrEmpty(r.ContraseñaUsuario));
        }
    }
}
