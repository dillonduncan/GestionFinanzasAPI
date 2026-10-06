using FluentValidation;
using GestionFinanzas.Services.DTOs.Requests;

namespace GestionFinanzas.Services.Validators.Meta
{
    public class MetaCreateDTOValidator : AbstractValidator<MetaCreateDTO>
    {
        public MetaCreateDTOValidator()
        {
            RuleFor(r => r.NombreMeta)
                .NotEmpty().WithMessage("Compae, el nombre de la meta es obligarotia.")
                .MaximumLength(100).WithMessage("El nombre no puede pasar de 100 caracteres.");

            RuleFor(r => r.MontoObjetivo)
                .GreaterThanOrEqualTo(0).WithMessage("El monto objetivo debe ser mayor a cero.")
                .PrecisionScale(18, 2, false).WithMessage("El monto no puede tener mas de 2 decimales.");

            RuleFor(r => r.SaldoActual)
                .GreaterThanOrEqualTo(0).WithMessage("El saldo actual no puede ser negativo")
                .LessThanOrEqualTo(r => r.MontoObjetivo).WithMessage("Pilla compae, el saldo actual no deberia ser mayor al Objetivo.");

            RuleFor(r => r.FechaLimite)
                .GreaterThan(DateTime.Now).WithMessage("La fecha limite tiene que ser en el futuro.")
                .When(r => r.FechaLimite.HasValue);
        }
    }
}
