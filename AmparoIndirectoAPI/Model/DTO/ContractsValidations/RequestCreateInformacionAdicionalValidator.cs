using FluentValidation;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateInformacionAdicionalValidator : AbstractValidator<RequestCreateInformacionAdicional>
    {
        public RequestCreateInformacionAdicionalValidator()
        {

            RuleFor(c => c.id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Es requerido seleccionar el juicio de amparo indirecto");
        }

        //private bool ValidateOficio(string input)
        //{
        //    string patron = @"^[A-Z0-9_\-./]{1,40}$";
        //    return Regex.IsMatch(input, patron);
        //}
    }
}