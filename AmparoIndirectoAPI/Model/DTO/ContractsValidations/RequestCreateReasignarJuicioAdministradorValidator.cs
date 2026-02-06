using System.Text.RegularExpressions;
using FluentValidation;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateReasignarJuicioAdministradorValidator : AbstractValidator<RequestCreateReasignarJuicioAdministrador>
    {
        public RequestCreateReasignarJuicioAdministradorValidator()
        {
            //RuleFor(c => c.id)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Es requerido seleccionar el juicio a reasignar");
            RuleFor(c => c.motivo_reasignacion)
                .NotNull()
                .NotEmpty()
                .WithMessage("Es requerido el motivo de reasignación");
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
