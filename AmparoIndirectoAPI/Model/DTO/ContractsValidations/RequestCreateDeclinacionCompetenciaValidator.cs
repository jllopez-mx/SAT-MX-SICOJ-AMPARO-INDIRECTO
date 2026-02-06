using System.Text.RegularExpressions;
using FluentValidation;
using Sicoj.Utils;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateDeclinacionCompetenciaValidator : AbstractValidator<RequestCreateDeclinacionCompetencia>
    {
        public RequestCreateDeclinacionCompetenciaValidator()
        {
            RuleFor(c => c.numero_asunto_destino)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio de Amparo es requerido")
                .Must(ValidateJuicio)
                .WithMessage("Juicio de Amparo no tiene el formato válido.");

            RuleFor(c => c.numero_oficio)
                .NotNull()
                .NotEmpty()
                .WithMessage("Número de Oficio es requerida.")
                .Must(ValidateExpediente)
                .WithMessage("Número de Oficio no es válido");

            RuleFor(c => c.fecha_recepcion_declinacion.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha recepción es requerida.")
                .Must(FluentValidationGuard.BeValidateDateFormat)
                .WithMessage("Fecha recepción requiere formato dd/mm/yyyy.");

            RuleFor(c => c.id_juzgado_destino)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juzgado destino es requerido");

            RuleFor(c => c.id_numero_asunto)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio a declinar es requerido");
        }
        private bool ValidateJuicio(string input)
        {
            string patron = (@"^[a-zA-Z0-9\/]{5}(\/\d{4})?$");
            return Regex.IsMatch(input, patron);
        }

        private bool ValidateExpediente(string input)
        {
            string patron = @"^[A-Z0-9_\-./]{1,30}$";
            return Regex.IsMatch(input, patron);
        }
    }   
}
