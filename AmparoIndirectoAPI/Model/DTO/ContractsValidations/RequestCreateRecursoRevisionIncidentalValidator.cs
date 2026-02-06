using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateRecursoRevisionIncidentalValidator : AbstractValidator<RequestCreateRecursoRevisionIncidental>
    {
        public RequestCreateRecursoRevisionIncidentalValidator()
        {
            RuleFor(c => c.id_numero_asunto)
                .NotNull()
                .NotEmpty()
                .WithMessage("El juicio de amparo indirecto es requerido.");

           
        }
    }
}
