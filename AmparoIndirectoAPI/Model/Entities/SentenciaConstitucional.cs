namespace AmparoIndirectoAPI.Model.Entities
{
    public class SentenciaConstitucional
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public DateTime? fecha_notificacion_sentencia { get; set; } = null!;
        public int? id_sentido_sentencia { get; set; }
        public int? id_tipo_sentido_sentencia { get; set; }
        public int? id_dictamen_no_revision { get; set; }
        public int? id_sentido_general_asunto { get; set; } = null!;
        public int? id_tipo_sentido_general { get; set; }
        public string? oficio_comunicacion_autoridad { get; set; } = null!;
        public DateTime? fecha_oficio_comunicacion_sentencia { get; set; } = null!;
        public DateTime? fecha_presentacion_oficio_comunicacion { get; set; } = null!;
        public DateTime? fecha_recepcion_auto_sentencia_ejecutoria { get; set; } = null!;
        public string? comunicado_acuerdo_firmeza { get; set; } = null!;
        public DateTime? fecha_comunicacion_acuerdo_firmeza { get; set; } = null!;
        public DateTime? fecha_conclusion_expediente { get; set; } = null!;
        public string? usuario { get; set; } = null!;

    }
}
