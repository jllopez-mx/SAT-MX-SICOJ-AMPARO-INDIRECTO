using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestSolicitudTransparenciaValidator : AbstractValidator<RequestCreateSolicitudTransparencia>
    {
        public RequestSolicitudTransparenciaValidator()
        {
            RuleFor(c => c.id)
                .NotNull()
                .NotEmpty()
                .WithMessage("El amparo para la Solicitud es requerido");
                
            RuleFor(c => c.numero_solicitud)
                .NotNull()
                .NotEmpty()
                .WithMessage("El Número de Solicitud es requerido")
                .Must(ValidateSolicitud)
                .WithMessage("Número de Solicitud no es válido");

            RuleFor(c => c.fecha_solicitud.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha solicitud es requerida.")
                .Must(FluentValidationGuard.BeValidateDateFormat)
                .WithMessage("Fecha recepción requiere formato dd/mm/yyyy.");
        }

        private bool ValidateSolicitud(string input)
        {
            string patron = @"^[A-Z0-9_\-.,/]{1,40}$";
            return Regex.IsMatch(input, patron);
        }
    }
}
