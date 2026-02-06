using System;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateInformeJustificadoConstitucional
    {
        public int id { get; set; }
        public int idNumeroAsunto { get; set; }
        //public int idJuicioAmparo { get; set; } se modifico
        public int idAutoridadResponsable { get; set; }
        public bool? solicitudOpinionTecnica { get; set; }
        public string fechaRecepcionDemanda { get; set; } = null!;
        public string fechaVencimientoJustificado { get; set; } = null!;
        public string numeroOficioInformeJustificado { get; set; } = null!;
        public string? fechaOficioInformeJustificado { get; set; } = null!;
        public string fechaPresentacionInformeJustificado { get; set; } = null!;
        public string? observacionesJustificado { get; set; } = null!;
        public IFormFile? documento { get; set; }
        public int? idTipoArchivo { get; set; }
        public int? idSeccion { get; set; }


    }
}
