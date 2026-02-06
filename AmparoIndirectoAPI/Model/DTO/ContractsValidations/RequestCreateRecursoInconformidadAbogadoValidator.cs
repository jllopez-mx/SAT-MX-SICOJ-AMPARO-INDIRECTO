using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateRecursoInconformidadAbogadoValidator : AbstractValidator<RequestCreateRecursoInconformidadConstitucional>
    {
        public RequestCreateRecursoInconformidadAbogadoValidator()
        {
            RuleFor(c => c.fechaNotificacionResolucionInconformidad)
                //.NotNull()
                //.NotEmpty()
                //.WithMessage("Fecha de notificación de recurso de resolución de inconformidad es requerida.")
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de la fecha de notificación de recurso de resolución de inconformidad no es válido.")
                .When(x => x.fechaNotificacionResolucionInconformidad != "undefined");
            RuleFor(c => c.notificacionAdmisionRecursInconformidad)
               .NotNull()
               .NotEmpty()
               .WithMessage("Notificación de admision de recurso de inconformidad es requerida.")
               .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
               .WithMessage("El formato de la notificación de admision de recurso de inconformidad no es válido.")
               .When(x => x.notificacionAdmisionRecursInconformidad != "undefined");

            //RuleFor(c => c.numero_recurso_inconformidad)
            //    .NotEmpty()
            //    .WithMessage("No ha indicado el parámetro de número de recurso de inconformidad.")
            //    .Matches(@"^[A-Z0-9_\-,/]+(\s[A-Z0-9_\-,/]+)*{1,40}$")
            //    .WithMessage("El número de recurso de inconformidad contiene caracteres no permitidos. Solo se permiten letras, números, diagonal (/), guion medio (-) y paréntesis ().");
            RuleFor(c => c.idOrganoRadicacioInconformidad)
                .NotNull()
                .NotEmpty()
                .WithMessage("Organo radicación es requerido");

        }

        private bool ValidateRecurso(string input)
        {
            //string patron = @"^[A-Z0-9_\-./]{1,40}$";
            string patron = @"^[A-Za-z0-9_\-./]{1,40}$";
            return Regex.IsMatch(input, patron);
        }

        private bool ValidateComunicacion(string input)
        {
            //string patron = @"^[A-Z0-9_\-./]{1,40}$";
            string patron = @"^[A-Za-z0-9_\-./]{1,40}$";
            return Regex.IsMatch(input, patron);
        }


        //private bool ValidateFecha(string input)
        //{
        //    string pattern = @"^\d{2}-\d{2}-\d{4}$";
        //    return Regex.IsMatch(input, pattern);
        //}
        private bool ValidateCaracteres(string input)
        {
            string patron = @"^[^#""'\[\]{}áéíóúü]*$";
            return Regex.IsMatch(input, patron);
        }
    }
}
