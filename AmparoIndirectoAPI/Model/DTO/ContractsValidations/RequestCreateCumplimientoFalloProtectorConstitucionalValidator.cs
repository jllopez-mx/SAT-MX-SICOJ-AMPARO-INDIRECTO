using System.Text.RegularExpressions;
using FluentValidation;
using Sicoj.Utils;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateCumplimientoFalloProtectorConstitucionalValidator : AbstractValidator<RequestCreateCumplimientoFalloProtectorConstitucional>
    {
        public RequestCreateCumplimientoFalloProtectorConstitucionalValidator()
        {
            RuleFor(c => c.idNumeroAsunto)
                .NotNull()
                .NotEmpty()
                .WithMessage("Juicio de Amparo Indirecto es requerido");
            RuleFor(c => c.idAutoridadResponsable)
                .NotNull()
                .NotEmpty()
                .WithMessage("Autoridad responsable es requerido");
            RuleFor(c => c.fechaNotificacionRequerimiento)
                .NotNull()
                .NotEmpty()
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de Fecha notificación de requerimiento no es válido.")
                .When(c => c.fechaNotificacionRequerimiento != "undefined");
            RuleFor(c => c.plazoFallo)
                .NotNull()
                .NotEmpty()
                .WithMessage("El plazo es requerido");
            //RuleFor(c => c.fechaPresentacionFallo)
            //    .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
            //    .WithMessage("El formato de la fecha de presentación fallo no es válido.")
            //    .When(x => x.fechaPresentacionFallo != "undefined");
            RuleFor(c => c.fechaPresentacionFallo)
                    .Custom((value, context) =>
                    {
                        var normalizado = value == null || value == "undefined" ? "" : value;

                        if (!string.IsNullOrEmpty(normalizado) && !FluentValidationGuard.BeValidateDateFormat(normalizado))
                        {
                            context.AddFailure("El formato de la fecha de presentación fallo no es válido.");
                        }
                    });
            RuleFor(c => c.fechaOficioComunicacionFallo)
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de la fecha de oficio de comunicación no es válido.")
                .When(x => x.fechaOficioComunicacionFallo != "undefined");
            //NO ES OBLIGATORIO
            //RuleFor(c => c.fecha_presentacion_fallo)
            //    //.NotNull()
            //    //.NotEmpty()
            //    //.WithMessage("Número de Oficio de comunicación es requerida.")
            //    .Must(ValidateOficio)
            //    .WithMessage("Número de Oficio de comunicación no es válido");
        }
        //private bool ValidateOficio(string input)
        //{
        //    string patron = @"^[A-Z0-9_\-.,/]{1,40}$";
        //    return Regex.IsMatch(input, patron);
        //}
    }
}
