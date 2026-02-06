using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateSentenciaIncidentalValidator : AbstractValidator<RequestCreateSentenciaIncidental>
    {
        public RequestCreateSentenciaIncidentalValidator()
        {
            //Oficio de comunicación
            //RuleFor(c => c.oficio_comunicacion_area_correspondiente)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Oficio de Comunicación es requerida.")
            //    .Must(ValidateComunicacion)
            //    .WithMessage("Oficio de Comunicación no es válido");

            RuleFor(c => c.id_numero_asunto)
                .NotNull()
                .NotEmpty()
                .WithMessage("El Juicio de Amparo es requerido");

            RuleFor(c => c.id_sentido_suspencion_definitiva)
                .NotNull()
                .NotEmpty()
                .WithMessage("Sentido de suspención definitiva es requerido");
            RuleFor(c => c.id_tipo_sentido_general_asunto)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tipo sentido general es requerido");
            RuleFor(c => c.id_tipo_sentido_general_asunto)
               .NotNull()
               .NotEmpty()
               .WithMessage("Sentido de general del asunto es requerido");


            //RuleFor(c => c.dictamen_no_revision_incidental)
            //     .NotNull()
            //     .NotEmpty()
            //     .WithMessage("El dictamen de no revisión incidental es requerido");

            RuleFor(c => c.id_autoridad_responsable)
                  .NotNull()
                  .NotEmpty()
                  .WithMessage("La autoridad responsable es requerido");

            RuleFor(c => c.id_otorgamiento_garatia)
                  .NotNull()
                  .NotEmpty()
                  .WithMessage("El otorgamiento de garantía es requerido");

            RuleFor(c => c.fecha_notificacion_sentencia.ToString())
                  .NotNull()
                  .NotEmpty()
                  .WithMessage("Fecha Comunicación es requerida.")
                  .Must(FluentValidationGuard.BeValidateDateFormat)
                  .WithMessage("Fecha Comunicación requiere formato dd/mm/yyyy.");
            //RuleFor(c => c.fecha_comunicacion_suspencion.ToString())
            //      .NotNull()
            //      .NotEmpty()
            //      .WithMessage("Fecha Comunicación es requerida.")
            //      .Must(FluentValidationGuard.BeValidateDateFormat)
            //      .WithMessage("Fecha Comunicación requiere formato dd/mm/yyyy.");

        }

        private bool ValidateComunicacion(string input)
        {
            //string patron = @"^[A-Z0-9_\-./]{1,40}$";
            string patron = @"^[A-Za-z0-9_\-./]{1,40}$";
            return Regex.IsMatch(input, patron);
        }

        private bool ValidateCaracteres(string input)
        {
            string patron = @"^[^#""'\[\]{}áéíóúü]*$";
            return Regex.IsMatch(input, patron);
        }
    }
}