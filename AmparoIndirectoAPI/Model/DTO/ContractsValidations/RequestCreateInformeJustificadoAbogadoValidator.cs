using FluentValidation;
using Microsoft.OpenApi.Any;
using Sicoj.Utils;
using System.Text.RegularExpressions;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateInformeJustificadoAbogadoValidator : AbstractValidator<RequestCreateInformeJustificadoConstitucional>
    {
        public RequestCreateInformeJustificadoAbogadoValidator()
        {
            RuleFor(c => c.idNumeroAsunto)
               .NotNull()
               .NotEmpty()
               .WithMessage("El juicio de amparo indirecto es requerido.");
            RuleFor(c => c.idAutoridadResponsable)
               .NotNull()
               .NotEmpty()
               .WithMessage("La autoridad responsable es requerida.");
            //Número de oficio de informe justificado
            RuleFor(c => c.numeroOficioInformeJustificado)
                .NotNull()
                .NotEmpty()
                .WithMessage("Número de Oficio de Informe Justificado es requerida.")
                .Must(ValidateOficio)
                .WithMessage("Número de Oficio de Informe Justificado no es válido");

            RuleFor(c => c.fechaPresentacionInformeJustificado)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fecha Presentación del Informe Justificado es requerida.")
                .Must(FluentValidationGuard.BeValidateDateFormat)
                .WithMessage("Fecha Presentación del Informe Justificado requiere formato dd-mm-yyyy.");

            RuleFor(c => c.observacionesJustificado)
                .Must(ValidateObservaciones).When(c => !string.IsNullOrEmpty(c.observacionesJustificado)).WithMessage("Observaciones contiene carácteres no válidos");
        }

        private bool ValidateOficio(string input)
        {
            //string patron = @"^[A-Z0-9_\-./]{1,40}$";
            string patron = @"^[A-Za-z0-9_\-,/]{1,40}$";
            return Regex.IsMatch(input, patron);
        }
        //Esta expresion no permite más de un espacio entre palabras
        private bool ValidateObservaciones(string input)
        {
            string patron = @"^[A-Za-z0-9_\-,/]+(\s[A-Za-z0-9_\-,/]+)*$";
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
