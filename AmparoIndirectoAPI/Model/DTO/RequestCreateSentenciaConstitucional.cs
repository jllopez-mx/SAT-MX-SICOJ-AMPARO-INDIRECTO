namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateSentenciaConstitucional
    {
        public int id { get; set; }
        public int idAutoridadResponsable { get; set; }
        public string? fechaNotificacionSentencia { get; set; } = null!;
        public int? idSentidoSentencia { get; set; }
        public int? idTipoSentidoSentencia { get; set; }
        public int? idDictamenNoRevision { get; set; }
        public int? idSentidoGeneralAsunto { get; set; }
        public int? idTipoSentidoGeneral { get; set; } = null!;
        public string? oficioComunicacionAutoridad { get; set; } = null!;
        public string? fechaOficioComunicacionSentencia { get; set; } = null!;
        public string? fechaPresentacionOficioComunicacion { get; set; } = null!;
        public string? fechaRecepcionAutoSentenciaEjecutoria { get; set; } = null!;
        public string? comunicadoAcuerdoFirmeza { get; set; } = null!;
        public string? fechaComunicacionAcuerdoFirmeza { get; set; } = null!;
        public string? fechaConclusionExpediente { get; set; } = null!;
        public IFormFile? documento { get; set; }
        public int? idTipoArchivo { get; set; }
        public int? idSeccion { get; set; }
    }
}
