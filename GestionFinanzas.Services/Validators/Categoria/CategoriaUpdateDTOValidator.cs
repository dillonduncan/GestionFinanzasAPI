using FluentValidation;
using GestionFinanzas.Services.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFinanzas.Services.Validators.Categoria
{
    public class CategoriaUpdateDTOValidator:AbstractValidator<CategoriaUpdateDTO>
    {
        public CategoriaUpdateDTOValidator()
        {
            RuleFor(r => r.NombreCategoria)
                .NotEmpty().WithMessage("El nombre de la categoria es obligatoria mi hecma.")
                .MaximumLength(50).WithMessage("El nombre no puede tener mas de 50 caracteres.");

            RuleFor(r => r.TipoCategoria)
                .NotEmpty().WithMessage("El tipo de categoria no puede ir vacio.")
                .Must(ValidarTipoCategoria).WithMessage("El tipo de categoria puede ser ´Ingreso´ o 'Gasto'.");
        }
        private bool ValidarTipoCategoria(string? tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo)) return false;

            var tipoLimpio = tipo.Trim().ToUpper();
            return tipoLimpio == "INGRESO" || tipoLimpio == "GASTO" || tipoLimpio == "SALIDA" || tipoLimpio == "EGRESO" || tipoLimpio == "ENTRADA";
        }
    }
}
