namespace AmparoIndirectoAPI.Model.Entities
{
    public class InformeJustificadoConstitucional
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public bool? solicitud_opinion_tecnica { get; set; }
        public DateTime fecha_recepcion_demanda { get; set; }
        public DateTime fecha_vencimiento_justificado { get; set; }
        public string numero_oficio_informe_justificado { get; set; } = null!;
        public DateTime? fecha_oficio_informe_justificado { get; set; } = null!;
        public DateTime fecha_presentacion_informe_justificado { get; set; }
        public string? observaciones_justificado { get; set; } = null!;
        public string? usuario { get; set; } = null!;
    }
}
