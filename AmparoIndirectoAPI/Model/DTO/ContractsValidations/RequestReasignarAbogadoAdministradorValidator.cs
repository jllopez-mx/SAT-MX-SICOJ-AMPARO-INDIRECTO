using System.Text.RegularExpressions;
using FluentValidation;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestReasignarAbogadoAdministradorValidator : AbstractValidator<RequestReasignarAbogadoAdministrador>
    {
        public RequestReasignarAbogadoAdministradorValidator()
        {
            RuleFor(c => c.id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio amparo indirecto es requerido");
            RuleFor(c => c.id_administracion_central)
                .NotNull()
                .NotEmpty()
                .WithMessage("Administración es requerido");
            RuleFor(c => c.id_subadministracion)
                .NotNull()
                .NotEmpty()
                .WithMessage("Subadministración es requerido");
            RuleFor(c => c.id_abogado)
                .NotNull()
                .NotEmpty()
                .WithMessage("El Abogado es obligatorio.")
                .MinimumLength(12)
                .WithMessage("RFC requiere minimo 12 caracteres")
                .MaximumLength(13)
                .WithMessage("RFC requiere maximo 13 caracteres")
                .Matches(@"^(?<pf>[A-Z]{4}\d{6}[A-Z0-9]{3})|(?<pm>[A-Z]{3}\d{6}[A-Z0-9]{3})$")
                .WithMessage("El RFC  del abogado no es válido");

        }

        private bool ValidateCaracteres(string input)
        {
            string patron = @"^[^#""'\[\]{}äëïöü´¨ÄËÏÖÜ@]*$";
            return Regex.IsMatch(input, patron);
        }
    }
}
