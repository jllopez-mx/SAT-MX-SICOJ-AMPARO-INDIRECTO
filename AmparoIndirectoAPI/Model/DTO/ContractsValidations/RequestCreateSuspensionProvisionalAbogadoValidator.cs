using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateSuspensionProvisionalAbogadoValidator : AbstractValidator<RequestCreateSuspensionProvisionalAbogado>
    {
        public RequestCreateSuspensionProvisionalAbogadoValidator()
        {
            RuleFor(c => c.oficio_comunicacion)
                .NotNull()
                .NotEmpty()
                .WithMessage("Oficio de Comunicación es requerida.")
                .Must(ValidateComunicacion)
                .WithMessage("Oficio de Comunicación no es válido");

            RuleFor(c => c.fecha_comunicacion.ToString())
               .NotNull()
               .NotEmpty()
               .WithMessage("Fecha Comunicación es requerida.")
               .Must(FluentValidationGuard.BeValidateDateFormat)
               .WithMessage("Fecha Comunicación requiere formato dd/mm/yyyy.");

        }

        private bool ValidateComunicacion(string input)
        {
            string patron = @"^[A-Za-z0-9_\-./]{1,40}$";
            return Regex.IsMatch(input, patron);
        }
    }
}
