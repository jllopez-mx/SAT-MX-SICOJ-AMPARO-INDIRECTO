using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateInformePrevioIncidentalValidator : AbstractValidator<RequestCreateInformePrevioIncidental>
    {
        public RequestCreateInformePrevioIncidentalValidator()
        {
            RuleFor(c => c.id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio de Amparo es requerido");

            RuleFor(c => c.numeroOficioInformePrevio)
                .NotNull()
                .NotEmpty()
                .WithMessage("Número de Oficio de informe previo es requerida.")
                .Must(ValidateExpediente)
                .WithMessage("Número de Oficio de informe previo no es válido");
            RuleFor(c => c.fechaAperturaIncidente)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha apertura de informe previo es requerida.")
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de fecha apertura de incidente no es válido.")
                .When(c => c.fechaAperturaIncidente != "undefined");
            RuleFor(c => c.fechaPresentacionInformPrevio)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha presentacion de informe previo es requerida.")
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de Fecha presentación de informe previo no es válido.")
                .When(c => c.fechaPresentacionInformPrevio != "undefined");
        }
        private bool ValidateExpediente(string input)
        {
            string patron = @"^[A-Z0-9_\-./]{1,30}$";
            return Regex.IsMatch(input, patron);
        }
    }
}