using FluentValidation;
using Sicoj.Utils;

namespace AmparoIndirectoAPI.Model.DTO.ContractsValidations
{
    public class RequestCreateArchivoAmparoIndirectoValidator : AbstractValidator<RequestCreateArchivoAmparoIndirecto>
    {
        public RequestCreateArchivoAmparoIndirectoValidator()
        {
            RuleFor(c => c.id).NotNull().NotEmpty().WithMessage("Juicio de Amparo es requerido");

            //RequestCreateArchivoAmparoIndirecto

            RuleFor(c => c.documento)
            .NotNull().WithMessage("Documento es requerido.")
            .Must(c => FluentValidationGuard.ConvertBytesToMegaBytes(c.Length) <= 10).WithMessage("El tamaño del documento no puede ser mayor a 10MB.");

            RuleFor(c => c.id_tipo_documento)
                .GreaterThan(0).WithMessage("Tipo asunto no es válido.");

            RuleFor(c => c.id_seccion)
                .GreaterThan(0).WithMessage("Sección no es válida.");

            //RuleFor(c => c.documento)
            //.NotNull().WithMessage("Archivo es obligatorio.")
            //.Must(BeValidFileSize).WithMessage("El tamaño del archivo excede el límite de 10 MB.");
        }
        //private bool BeValidFileSize(IFormFile file)
        //{
        //    // Verificar si el archivo es nulo
        //    if (file == null)
        //        return true; // Se permite que el archivo sea nulo, ya que la validación de nulidad está en otra regla

        //    long maxSizeBytes = 10 * 1024 * 1024; // 10 MB en bytes
        //    return file.Length <= maxSizeBytes;
        //}
    }
}
