namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateCumplimientoFalloProtectorConstitucional
    {
        public int idNumeroAsunto { get; set; }
        //public int idJuicioAmparo { get; set; } se modifico
        public int idAutoridadResponsable { get; set; }
        //public bool cumplimiento_fallo { get; set; }
        public string ? fechaNotificacionRequerimiento { get; set; }
        public int plazoFallo { get; set; }
        //public string? fecha_vencimiento_requerimiento { get; set; } = null!;
        public string ? fechaPresentacionFallo { get; set; } = null!;
        public string ? numeroOficioAtencion { get; set; } = null!;
        public string ? fechaNotificacionAcuerdoFallo { get; set; } = null!;
        public string ? fechaOficioComunicacionFallo { get; set; } = null!;
        public string ? numeroOficioComunicacionAutoridad { get; set; } = null!;
        public IFormFile? documento { get; set; }
        public int? idTipoArchivo { get; set; }
        public int? idSeccion { get; set; }
    }
}
