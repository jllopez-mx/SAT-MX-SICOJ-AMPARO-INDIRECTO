using FluentValidation;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateRecursoQuejaConstitucionalValidator : AbstractValidator<RequestCreateRecursoQuejaConstitucional>
    {
        public RequestCreateRecursoQuejaConstitucionalValidator()
        {
            RuleFor(c => c.id_numero_asunto)
               .NotNull()
               .NotEmpty()
               .WithMessage("Juicio de Amparo Indirecto es requerido");

            When(c => c.recurso_queja_principal == true, () =>
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
                    RuleFor(c => c.fecha_recepcion_acuerdo_queja.ToString())
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Fecha recepción acuerdo es requerida.")
                    .Must(FluentValidationGuard.BeValidateDateFormat)
                    .WithMessage("Fecha recepción acuerdo requiere formato dd/mm/yyyy.");

                    //RuleFor(c => c.fecha_vencimiento_recurso_queja.ToString())
                    //    .NotNull()
                    //    .NotEmpty()
                    //    .WithMessage("Fecha vencimiento del recurso es requerida.")
                    //    .Must(FluentValidationGuard.BeValidateDateFormat)
                    //    .WithMessage("Fecha vencimiento del recurso requiere formato dd/mm/yyyy.");
                });

                //Este es oBligatorio en los tres casos Autoridades Responsables, Otras Autoridades y Quejoso
                //RuleFor(c => c.fecha_admision_queja.ToString())
                //    .NotNull()
                //    .NotEmpty()
                //    .WithMessage("Fecha adminsión queja es requerida.")
                //    .Must(FluentValidationGuard.BeValidateDateFormat)
                //    .WithMessage("Fecha adminsión queja  formato dd/mm/yyyy.");
                //RuleFor(c => c.id_organo_radicacion_queja)
                //    .NotNull()
                //    .NotEmpty()
                //    .WithMessage("Organo radicación queja es requerido");
                //RuleFor(c => c.toca_queja)
                //    .NotNull()
                //    .NotEmpty()
                //    .WithMessage("Toca es requerido.")
                //    .Must(ValidateExpediente)
                //    .WithMessage("Toca tiene carácteres no permitidos");
                //RuleFor(c => c.fecha_oficio_comunicacion.ToString())
                //    .NotNull()
                //    .NotEmpty()
                //    .WithMessage("Fecha oficio comunicación es requerida.")
                //    .Must(FluentValidationGuard.BeValidateDateFormat)
                //    .WithMessage("Fecha oficio comunicación requiere formato dd/mm/yyyy.");
            });

            //RuleFor(c => c.oficio_comunicacion_area_correspondiente)
            //    .NotNull()
            //    .NotEmpty()
            //    //.WithMessage("Número de Oficio de recurso de queja requerido.")
            //    //.Must(ValidateExpediente)
            //    .WithMessage("Número de Oficio de recurso de queja requerido");
            //RuleFor(c => c.oficio_comunicacion_area_correspondiente.ToString())
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Oficio de Comunicación de area es requerido.");
            ////.Must(FluentValidationGuard.BeValidateDateFormat)
            ////.WithMessage("Fecha apertura de incidente formato dd/mm/yyyy.");




            //RuleFor(c => c.fecha_presentacion_informe_previo.ToString())
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Fecha presentacion de informe previo es requerida.")
            //    .Must(FluentValidationGuard.BeValidateDateFormat)
            //    .WithMessage("Fecha presentación de informe previo requiere formato dd/mm/yyyy.");

        }
        private bool ValidateExpediente(string input)
        {
            string patron = @"^[A-Z0-9_\-./]{1,30}$";
            return Regex.IsMatch(input, patron);
        }
        private bool ValidateRecurrente(int? recurrente)
        {
            // Verifica que el valor sea 0, 1 o 2
            return recurrente.HasValue && (recurrente.Value == 1 || recurrente.Value == 2 || recurrente.Value == 3);
        }
    }
}