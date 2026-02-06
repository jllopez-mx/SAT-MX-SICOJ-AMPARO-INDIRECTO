using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateRemitirAsuntoAdministradorValidator : AbstractValidator<RequestCreateCanalizarAsuntoAdministrador>
    {
        public RequestCreateRemitirAsuntoAdministradorValidator()
        {

            RuleFor(c => c.numero_oficio_canalizacion)
                .NotNull()
                .NotEmpty()
                .WithMessage("Número de oficio de remisión es requerida.")
                .Must(ValidateOficio)
                .WithMessage("Número de Oficio de remisión no es válido");

            RuleFor(c => c.fecha_canalizacion.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha remisión es requerida.")
                .Must(FluentValidationGuard.BeValidateDateFormat)
                .WithMessage("Fecha remisión requiere formato dd/mm/yyyy.");

            RuleFor(c => c.motivo_canalizacion)
                .NotNull()
                .NotEmpty()
                .WithMessage("Motivo canalización es requerido.")
                .Must(ValidateCaracteres)
                .WithMessage("Motivo canalización contiene carácteres NO permitidos");

            RuleFor(c => c.unidad_administrativa_canaliza)
               .NotNull()
               .NotEmpty()
               .WithMessage("Unidad administrativa que remite el asunto es requerido");
            RuleFor(c => c.unidad_administrativa_recibe)
               .NotNull()
               .NotEmpty()
               .WithMessage("Unidad administrativa que recibe el asunto es requerido");
        }

        private bool ValidateOficio(string input)
        {
            string patron = @"^[A-Z0-9_\-./]{1,40}$";
            return Regex.IsMatch(input, patron);
        }
        private bool ValidateCaracteres(string input)
        {
            string patron = @"^[^#""'\[\]{}áéíóúü]*$";
            return Regex.IsMatch(input, patron);
        }
    }
}
