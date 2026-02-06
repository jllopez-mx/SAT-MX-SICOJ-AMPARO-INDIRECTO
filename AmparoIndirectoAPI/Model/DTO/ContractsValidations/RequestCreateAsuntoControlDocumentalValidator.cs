using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateAsuntoControlDocumentalValidator : AbstractValidator<RequestCreateAsuntoControlDocumental>
    {
        public RequestCreateAsuntoControlDocumentalValidator()
        {
            RuleFor(c => c.NumeroAsunto)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio de Amparo es requerido")
                .Must(ValidateJuicio)
                .WithMessage("Juicio de Amparo no tiene el formato válido.");

            //RuleFor(c => c.NumeroExpediente)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Número de expediente es requerido")
            //    .Must(ValidateExpediente)
            //    .WithMessage("Número de Expediente no es válido");

            RuleFor(c => c.FechaRecepcionDemanda.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha recepción es requerida.")
                .Must(FluentValidationGuard.BeValidateDateFormat)
                .WithMessage("Fecha recepción requiere formato dd/mm/yyyy.");

            RuleFor(c => c.RfcQuejoso)
                .NotNull()
                .NotEmpty()
                .WithMessage("RFC es obligatorio.")
                .MinimumLength(12)
                .WithMessage("RFC requiere minimo 12 caracteres")
                .MaximumLength(13)
                .WithMessage("RFC requiere maximo 13 caracteres")
                .Matches(@"^(?<pf>[A-Z]{4}\d{6}[A-Z0-9]{3})|(?<pm>[A-Z]{3}\d{6}[A-Z0-9]{3})$")
                .WithMessage("El RFC no es válido");

            RuleFor(c => c.NombreQuejoso)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nombre del quejoso es requerido")
                .Must(ValidateCaracteres)
                .WithMessage("Nombre del Quejoso contiene carácteres NO permitidos");

            //RuleFor(c => c.Despacho)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Despacho es requerido")
            //    .Must(ValidateCaracteres)
            //    .WithMessage("Despacho contiene caracteres NO permitidos");

            //RuleFor(c => c.IdJuzgado)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Juzgado es requerido");

            //RuleFor(c => c.IdMateria)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Materia es requerido");

            //RuleFor(c => c.IdSubmateria)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Submateria es requerido");

            //RuleFor(c => c.IdTipoActo)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Tipo de acto es requerido");
            //RuleFor(c => c.id_autoridad_responsable)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Autoridad responsable es requerido");

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

        private bool ValidateCaracteres(string input)
        {
            string patron = @"^[^#""'\[\]{}äëïöü´¨ÄËÏÖÜ@]*$";
            return Regex.IsMatch(input, patron);
        }

        private bool BeValidateDateFormat(string dateString)
        {
            return DateTime.TryParse(dateString, out _);
        }


    }
}
