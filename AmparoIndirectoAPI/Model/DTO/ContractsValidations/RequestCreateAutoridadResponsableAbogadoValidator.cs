using FluentValidation;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateAutoridadResponsableAbogadoValidator : AbstractValidator<RequestCreateAutoridadResponsableAbogado>
    {
        public RequestCreateAutoridadResponsableAbogadoValidator () 
        {
            RuleFor(c => c.id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio de Amparo es requerido");
        }
    }
}
