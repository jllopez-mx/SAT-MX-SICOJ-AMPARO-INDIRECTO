using FluentValidation;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestAsignarAbogadoAdministadorValidator : AbstractValidator<RequestAsignarAbogadoAdministrador>
    {
        public RequestAsignarAbogadoAdministadorValidator()
        {
            RuleFor(c => c.id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio amparo indirecto es requerido");
            RuleFor(c => c.id_abogado)
                .NotNull()
                .NotEmpty()
                .WithMessage("Abogado es obligatorio.")
                .MinimumLength(12)
                .WithMessage("RFC requiere minimo 12 caracteres")
                .MaximumLength(13)
                .WithMessage("RFC requiere maximo 13 caracteres")
                .Matches(@"^(?<pf>[A-Z]{4}\d{6}[A-Z0-9]{3})|(?<pm>[A-Z]{3}\d{6}[A-Z0-9]{3})$")
                .WithMessage("El RFC no es válido");
        }
    }
}
