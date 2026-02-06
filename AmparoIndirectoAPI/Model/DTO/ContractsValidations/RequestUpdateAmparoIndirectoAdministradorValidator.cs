using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestUpdateAmparoIndirectoAdministradorValidator : AbstractValidator<RequestUpdateAmparoIndirectoAdministrador>
    {
        public RequestUpdateAmparoIndirectoAdministradorValidator()
        {
            RuleFor(c => c.numero_asunto)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio de Amparo es requerido")
                .Must(ValidateJuicio)
                .WithMessage("Juicio de Amparo no tiene el formato válido.");

            RuleFor(c => c.numero_expediente)
                .NotNull()
                .NotEmpty()
                .WithMessage("Número de expediente es requerido")
                .Must(ValidateExpediente)
                .WithMessage("Número de Expediente no es válido");

            RuleFor(c => c.fecha_recepcion_demanda.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha recepción es requerida.")
                .Must(FluentValidationGuard.BeValidateDateFormat)
                .WithMessage("Fecha recepción requiere formato dd/mm/yyyy.");

            //RuleFor(c => c.fecha_vencimiento.ToString())
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Fecha vencimiento es requerida.")
            //    .Must(FluentValidationGuard.BeValidateDateFormat)
            //    .WithMessage("Fecha vencimiento requiere formato dd/mm/yyyy.");

            RuleFor(c => c.nombre_quejoso)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nombre del quejoso es requerido")
                .Must(ValidateCaracteres)
                .WithMessage("Nombre del Quejoso contiene carácteres NO permitidos");

            RuleFor(c => c.despacho)
                .NotNull()
                .NotEmpty()
                .WithMessage("Despacho es requerido")
                .Must(ValidateCaracteres)
                .WithMessage("Despacho contiene caracteres NO permitidos");

            RuleFor(c => c.id_juzgado)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juzgado es requerido");

            RuleFor(c => c.id_materia)
                .NotNull()
                .NotEmpty()
                .WithMessage("Materia es requerido");

            RuleFor(c => c.id_submateria)
                .NotNull()
                .NotEmpty()
                .WithMessage("Submateria es requerido");

            RuleFor(c => c.id_tipo_acto)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tipo de acto es requerido");
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
    }
}
