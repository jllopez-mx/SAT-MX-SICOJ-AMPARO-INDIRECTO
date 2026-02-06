using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateRecursoReclamacionAbogadoValidator : AbstractValidator<RequestCreateRecursoReclamacionConstitucional>
    {
        public RequestCreateRecursoReclamacionAbogadoValidator()
        {
            RuleFor(c => c.id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Es requerido seleccionar el juicio de Amparo Indirecto");
            RuleFor(c => c.fechaNotificacionAcuerdo)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha de notificación de acuerdo es requerida.")
                .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
                .WithMessage("El formato de la fecha de notificación de acuerdo no es válido.")
                .When(x => x.fechaNotificacionAcuerdo != "undefined");
            //RuleFor(c => c.fecha_vencimiento_recurso_reclamacion)
            //   .NotNull()
            //   .NotEmpty()
            //   .WithMessage("Fecha vencimiento recurso reclamacion es requerida.")
            //   .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
            //   .WithMessage("El formato de la fecha vencimiento recurso reclamacion no es válido.")
            //   .When(x => x.fecha_vencimiento_recurso_reclamacion != "undefined");

            RuleFor(c => c.fechaPresentacion)
               .NotNull()
               .NotEmpty()
               .WithMessage("Fecha de presentación es requerida.")
               .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
               .WithMessage("El formato de la de presentación no es válido.")
               .When(x => x.fechaPresentacion != "undefined");

            //RuleFor(c => c.oficio_reclamacion)
            //    //.NotEmpty()
            //    //.WithMessage("No ha indicado el parámetro de número de oficio de comunicación de autoridad.")
            //    .Matches(@"^[A-Z0-9_\-,/]+(\s[A-Z0-9_\-,/]+)*{1,40}$")
            //    .WithMessage("El número de oficio de reclamación contiene caracteres no permitidos. Solo se permiten letras, números, diagonal (/), guion medio (-) y paréntesis ().");

            RuleFor(c => c.notificacionAdmisionRecursoReclamacion)
               .NotNull()
               .NotEmpty()
               .WithMessage("Notificación de admision de recurso de reclamación es requerida.")
               .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
               .WithMessage("El formato de la de Notificación de admision de recurso de reclamación no es válido.")
               .When(x => x.notificacionAdmisionRecursoReclamacion != "undefined");

            //RuleFor(c => c.numero_recurso)
            //     //.NotEmpty()
            //     //.WithMessage("No ha indicado el parámetro de número de oficio de comunicación de autoridad.")
            //     .Matches(@"^[A-Z0-9_\-,/]+(\s[A-Z0-9_\-,/]+)*{1,40}$")
            //     .WithMessage("El número de recurso contiene caracteres no permitidos. Solo se permiten letras, números, diagonal (/), guion medio (-) y paréntesis ().");
            RuleFor(c => c.fechaNotificacionResolucion)
               //.NotNull()
               //.NotEmpty()
               //.WithMessage("Fecha de presentación es requerida.")
               .Must(date => date == null || date == "" || FluentValidationGuard.BeValidateDateFormat(date))
               .WithMessage("El formato de la fecha de notificación de resolución no es válido.")
               .When(x => x.fechaNotificacionResolucion != "undefined");


            //RuleFor(c => c.notificacion_admision_recurso_reclamacion.ToString())
            //        .NotNull()
            //        .NotEmpty()
            //        .WithMessage("Notificacion admision recurso reclamación es requerida.")
            //        .Must(FluentValidationGuard.BeValidateDateFormat)
            //        .WithMessage("Notificacion admision recurso reclamación requiere formato dd/mm/yyyy.");
            //RuleFor(c => c.fecha_presentacion.ToString())
            //        .NotNull()
            //        .NotEmpty()
            //        .WithMessage("Fecha presentación es requerida.")
            //        .Must(FluentValidationGuard.BeValidateDateFormat)
            //        .WithMessage("Fecha presentación requiere formato dd/mm/yyyy.");
            //RuleFor(c => c.fecha_vencimiento_recurso_reclamacion.ToString())
            //        .NotNull()
            //        .NotEmpty()
            //        .WithMessage("Fecha vencimiento del recurso reclamación es requerida.")
            //        .Must(FluentValidationGuard.BeValidateDateFormat)
            //        .WithMessage("Fecha vencimiento del recurso reclamación requiere formato dd/mm/yyyy.");
            //RuleFor(c => c.oficio_reclamacion.ToString())
            //        .Must(ValidateOficioReclamacion)
            //        .WithMessage("Oficio reclamación contiene caracteres NO permitidos.");
            //RuleFor(c => c.numero_recurso.ToString())
            //        .Must(ValidateOficioReclamacion)
            //        .WithMessage("Numero recurso contiene caracteres NO permitidos.");


        }

        private bool ValidateOficioReclamacion(string input)
        {
            //string patron = @"^[A-Z0-9_\-./]{1,40}$";
            string patron = @"^[A-Za-z0-9_\-,/]{1,40}$";
            return Regex.IsMatch(input, patron);
        }

    }
}
