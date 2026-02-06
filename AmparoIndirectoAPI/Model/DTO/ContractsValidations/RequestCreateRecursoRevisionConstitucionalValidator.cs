using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateRecursoRevisionConstitucionalValidator : AbstractValidator<RequestCreateRecursoRevisionConstitucional>
    {
        public RequestCreateRecursoRevisionConstitucionalValidator()
        {
            
            
            RuleFor(c => c.id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Juicio de Amparo Indirecto es requerido");

            When(c => c.recurso_revision_principal == true, () =>
            {
                RuleFor(c => c.id_recurrente)
                .NotNull()
                .NotEmpty()
                .WithMessage("Recurrente es requerido")
                .Must(ValidateRecurrente)
                .WithMessage("Recurrente debe de ser, Autoridad Responsable, Quejoso o Otras autoridades");

                RuleFor(c => c.id_autoridad_responsable)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("La autoridad es requerido");

                When(c => c.id_recurrente == 1, () =>
                {
                    RuleFor(c => c.fecha_recepcion_sentencia.ToString())
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Fecha recepción sentencia es requerida.")
                    .Must(FluentValidationGuard.BeValidateDateFormat)
                    .WithMessage("Fecha recepción acuerdo requiere formato dd/mm/yyyy.");

                    //RuleFor(c => c.fecha_vencimiento_recurso_revision.ToString())
                    //    .NotNull()
                    //    .NotEmpty()
                    //    .WithMessage("Fecha vencimiento del recurso es requerida.")
                    //    .Must(FluentValidationGuard.BeValidateDateFormat)
                    //    .WithMessage("Fecha vencimiento del recurso requiere formato dd/mm/yyyy.");
                });

                //Este es obligatorio en los tres casos Autoridades Responsables, Otras Autoridades y Quejoso
                //RuleFor(c => c.fecha_admision.ToString())
                //    .NotNull()
                //    .NotEmpty()
                //    .WithMessage("Fecha adminsión es requerida.")
                //    .Must(FluentValidationGuard.BeValidateDateFormat)
                //    .WithMessage("Fecha adminsión  formato dd/mm/yyyy.");
                //RuleFor(c => c.revision_adhesiva)
                //    .NotNull()
                //    .NotEmpty()
                //    .WithMessage("Revisión adhesiva es requerido");
                //Oficio de comunicación
                //RuleFor(c => c.oficio_recurso)
                //    .NotNull()
                //    .NotEmpty()
                //    .WithMessage("Oficio del Recurso es requerido.");
            });

        }

        private bool ValidateComunicacion(string input)
        {
            //string patron = @"^[A-Z0-9_\-./]{1,40}$";
            string patron = @"^[A-Za-z0-9_\-./]{1,40}$";
            return Regex.IsMatch(input, patron);
        }

        private bool ValidateRecurrente(int? recurrente)
        {
            // Verifica que el valor sea 0, 1 o 2
            return recurrente.HasValue && (recurrente.Value == 1 || recurrente.Value == 2 || recurrente.Value == 3);
        }
        
    }
}
