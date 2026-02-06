using FluentValidation;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestRecursoQuejaIncidentalValidator : AbstractValidator<RequestRecursoQuejaIncidental>
    {
        public RequestRecursoQuejaIncidentalValidator()
        {
            RuleFor(c => c.id_numero_asunto)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio amparo indirecto es requerido");
        }
    }
}