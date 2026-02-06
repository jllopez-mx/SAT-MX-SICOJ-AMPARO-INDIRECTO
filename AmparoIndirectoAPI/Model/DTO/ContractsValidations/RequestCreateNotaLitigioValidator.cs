using FluentValidation;
using Sicoj.Utils;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateNotaLitigioValidator : AbstractValidator<RequestCreateNotaLitigio>
    {
        public RequestCreateNotaLitigioValidator()
        {
            RuleFor(c => c.fechaRegistroNota.ToString())
                 .NotNull()
                 .NotEmpty()
                 .WithMessage("Fecha notificación de admisión de recurso es requerida.")
                 .Must(FluentValidationGuard.BeValidateDateFormat)
                 .WithMessage("Fecha notificación de admisión de recurso requiere formato dd/mm/yyyy.");
        }
    }
}
