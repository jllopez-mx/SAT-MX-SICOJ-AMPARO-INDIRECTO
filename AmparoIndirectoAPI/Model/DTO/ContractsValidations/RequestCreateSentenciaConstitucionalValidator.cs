using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateSentenciaConstitucionalValidator : AbstractValidator<RequestCreateSentenciaConstitucional>
    {
        public RequestCreateSentenciaConstitucionalValidator()
        {


            RuleFor(c => c.id)
                .NotNull()
                .NotEmpty()
                .WithMessage("El Juicio de Amparo Indirecto es requerido");

            RuleFor(c => c.idAutoridadResponsable)
                .NotNull()
                .NotEmpty()
                .WithMessage("Autoridad responsable es requerido");
            RuleFor(c => c.fechaNotificacionSentencia)
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de la fecha de notificación sentencia no es válido.")
                .When(c => c.fechaNotificacionSentencia != "undefined");
            RuleFor(c => c.fechaPresentacionOficioComunicacion)
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de la fecha de presentación de oficio comunicación no es válido.")
                .When(c => c.fechaPresentacionOficioComunicacion != "undefined");
            RuleFor(c => c.fechaComunicacionAcuerdoFirmeza)
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de la fecha de comunicación de acuerdo de firmeza no es válido.")
                .When(c => c.fechaComunicacionAcuerdoFirmeza != "undefined");

            RuleFor(c => c.idSentidoSentencia)
                  .NotNull()
                  .NotEmpty()
                  .WithMessage("Sentido sentencia es requerido");
            RuleFor(c => c.idTipoSentidoSentencia)
                  .NotNull()
                  .NotEmpty()
                  .WithMessage("Sentido sentencia es requerido");
        }

        //private bool ValidateComunicacion(string input)
        //{
        //    string patron = @"^[A-Za-z0-9_\-,/]+(\s[A-Za-z0-9_\-,/]+)*$";
        //    return Regex.IsMatch(input, patron);
        //}

        //private bool ValidateFirmeza(string input)
        //{
        //    string patron = @"^[A-Za-z0-9_\-,/]{1,40}$";
        //    return Regex.IsMatch(input, patron);
        //}

        //private bool ValidateCaracteres(string input)
        //{
        //    string patron = @"^[^#""'\[\]{}áéíóúü]*$";
        //    return Regex.IsMatch(input, patron);
        //}
    }
}