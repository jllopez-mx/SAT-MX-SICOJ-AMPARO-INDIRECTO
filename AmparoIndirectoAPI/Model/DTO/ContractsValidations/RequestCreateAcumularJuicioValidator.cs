using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateAcumularJuicioValidator : AbstractValidator<RequestCreateAcumularJuicio>
    {
        public RequestCreateAcumularJuicioValidator()
        {
            RuleFor(c => c.numero_oficio)
                .Must(ValidateOficio)
                .WithMessage("Número de oficio no es válido");

            RuleFor(c => c.id_juicio_padre)
                .NotNull()
                .NotEmpty()
                .WithMessage("Es requerido el juicio al que se desea acumular");

            RuleFor(c => c.id_juicio_acumulado)
                .NotNull()
                .NotEmpty()
                .WithMessage("Es requerido seleccionar juicios a acumular");
        }
      
        private bool ValidateOficio(string input)
        {
            string patron = @"^[A-Z0-9_\-./]{1,40}$";
            return Regex.IsMatch(input, patron);
        }
    }
}
