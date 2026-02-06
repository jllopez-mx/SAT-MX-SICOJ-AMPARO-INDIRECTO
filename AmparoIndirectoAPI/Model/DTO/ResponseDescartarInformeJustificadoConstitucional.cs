namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseDescartarInformeJustificadoConstitucional
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public string? autoridad_responsable { get; set; } = null!;
        public bool? solicitud_opinion_tecnica { get; set; } = null!;
        public string? fecha_recepcion_demanda { get; set; } = null!;
        public string? fecha_vencimiento_justificado { get; set; } = null!;
        public string? numero_oficio_informe_justificado { get; set; } = null!;
        public string? fecha_presentacion_informe_justificado { get; set; } = null!;
    }
}
