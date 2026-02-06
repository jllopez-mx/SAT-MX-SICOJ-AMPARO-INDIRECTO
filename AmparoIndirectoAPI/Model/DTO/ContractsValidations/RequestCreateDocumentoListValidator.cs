using FluentValidation;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateDocumentoListValidator : AbstractValidator<RequestCreateDocumentoList>
    {
        public RequestCreateDocumentoListValidator()
        {
            RuleFor(c => c.id).NotNull().NotEmpty().WithMessage("Juicio de Amparo es requerido");
        }
    }
}
