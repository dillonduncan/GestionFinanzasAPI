using FluentValidation;
using GestionFinanzas.Services.DTOs.Requests;

namespace GestionFinanzas.Services.Validators.Transaccion
{
    public class TransaccionUpdateDTOValidator : AbstractValidator<TransaccionUpdateDTO>
    {
        public TransaccionUpdateDTOValidator()
        {
            RuleFor(r => r.MontoTransaccion)
               .NotEmpty().WithMessage("EL monto es obligatorio.")
               .GreaterThan(0).WithMessage("El monto debe ser mayor a 0.")
               .PrecisionScale(18, 2, false).WithMessage("El monto no puede tener mas de los decimales.");

            RuleFor(r => r.DescripcionTransaccion)
                .MaximumLength(200).WithMessage("La descripcion no puede pasar de 200 caracteres, compae.");

            RuleFor(r => r.TipoTransaccion)
                .NotEmpty().WithMessage("El tipo de transaccion es obligatorio.")
                .Must(ValidarTipoTransaccion).WithMessage("El tipo puede ser 'Ingreso' o 'Gasto'.");

            RuleFor(r => r.FechaTransaccion)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("No pueds registrar transacciones del futuro.");

            RuleFor(r => r.IdCategoria)
                .GreaterThan(0).WithMessage("Debes asignar alguna categoria valida.");
        }

        private bool ValidarTipoTransaccion(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo)) return false;

            var tipoLimpio = tipo.Trim().ToUpper();
            return tipoLimpio == "INGRESO" || tipoLimpio == "GASTO" || tipoLimpio == "SALIDA" || tipoLimpio == "EGRESO" || tipoLimpio == "ENTRADA";
        }
    }
}
