using FluentValidation;
using GestionFinanzas.Services.DTOs.Requests;

namespace GestionFinanzas.Services.Validators.Usuario
{
    public class UsuarioCreateDTOValidator : AbstractValidator<UsuarioCreateDTO>
    {
        public UsuarioCreateDTOValidator()
        {
            RuleFor(r => r.NumeroIdentificacion)
                .NotEmpty().WithMessage("El numero de identificacion es obligatorio.");

            RuleFor(r => r.NombreUsuario)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio compae.")
                .MaximumLength(50).WithMessage("El nombre no puede tener mas de 50 caracteres.");

            RuleFor(r => r.ApellidoUsuario)
                .NotEmpty().WithMessage("El apellido es obligarotio, mi hecmano.");

            RuleFor(r => r.CorreoUsuario)
                .NotEmpty().WithMessage("El correo es obligatorio, compa.")
                .EmailAddress().WithMessage("El formato del correo no es valido.");

            RuleFor(r => r.ContraseñaUsuario)
                .MinimumLength(6).WithMessage("La contraseña debe tener minimo 6 caracteres.")
                .When(r => !string.IsNullOrEmpty(r.ContraseñaUsuario.ToString()));
        }

    }
}
