using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateDocumentoList
    {
        public List<RequestCreateArchivoAmparoIndirecto> documentoList { get; set; } = null!;
        public int id { get; set; }
    }
    public class RequestCreateArchivoAmparoIndirecto
    {

        public int id { get; set; }
        public int id_tipo_documento { get; set; }
        public int id_seccion { get; set; }
        public IFormFile documento { get; set; } = null!;

    }
}
