using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateIncidenteExcesoIncidentalValidator : AbstractValidator<RequestCreateIncidenteExcesoIncidental>
    {
        public RequestCreateIncidenteExcesoIncidentalValidator()
        {


            When(c => c.id_autoridad_responsable > 0, () =>
            {
                //Número de oficio de informe justificado
                RuleFor(c => c.interposicion_incidente)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Interposición del Incidente es requerida.");
                //.Must(ValidateOficio)
                //.WithMessage("Número de Oficio de Informe Justificado no es válido");

                RuleFor(c => c.id_autoridad_responsable)
                .NotNull()
                .NotEmpty()
                .WithMessage("La autoridad responsable es requerida.");

                RuleFor(c => c.fecha_notificacion_acuerdo)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Fecha notificación de acuerdo es requerida.")
                    .Must(FluentValidationGuard.BeValidateDateFormat)
                    .WithMessage("Fecha notificación de acuerdo requiere formato dd/mm/yyyy.");


                RuleFor(c => c.oficio_desahogo)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Oficio desahogo es requerido.")
                    .Must(ValidateOficio)
                    .WithMessage("Oficio desahogo no es válido");

                RuleFor(c => c.fecha_oficio_desahogo)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Fecha oficio de deshaogo es requerida.")
                    .Must(FluentValidationGuard.BeValidateDateFormat)
                    .WithMessage("Fecha oficio de desahogo requiere formato dd/mm/yyyy.");
            });

            When(c => c.id_sentido > 0, () =>
            {
                RuleFor(c => c.id_sentido)
                .NotNull()
                .NotEmpty()
                .WithMessage("El sentido de la resolución es requerido.");

                RuleFor(c => c.fecha_notificacion_resolucion)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha notificacion de resolución es requerida.")
                .Must(FluentValidationGuard.BeValidateDateFormat)
                .WithMessage("Fecha notificacion de resolución requiere formato dd/mm/yyyy.");

                RuleFor(c => c.oficio_comunicacion_autoridad)
                .NotNull()
                .NotEmpty()
                .WithMessage("Oficio de comunicación a la autoridad es requerido.")
                .Must(ValidateOficio)
                .WithMessage("Oficio de comunicación a la autoridad no es válido");

                RuleFor(c => c.fecha_oficio_comunicacion)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha de oficio de comunicación es requerida.")
                .Must(FluentValidationGuard.BeValidateDateFormat)
                .WithMessage("Fecha de oficio de comunicación requiere formato dd/mm/yyyy.");
            });

            //RuleFor(c => c.fecha_presentacion_informe_justificado.ToString())
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Fecha Presentación del Informe Justificado es requerida.")
            //    .Must(FluentValidationGuard.BeValidateDateFormat)
            //    .WithMessage("Fecha Presentación del Informe Justificado requiere formato dd-mm-yyyy.");

        }

        private bool ValidateOficio(string input)
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
