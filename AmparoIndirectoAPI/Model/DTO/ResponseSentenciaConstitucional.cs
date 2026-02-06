using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseSentenciaConstitucional
    {

        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public string? autoridad_responsable { get; set; } = null!;
        public string fecha_notificacion_sentencia { get; set; } = null!;
        public int id_sentido_sentencia { get; set; }
        public string? sentido_sentencia { get; set; } = null!;
        public int id_tipo_sentido_sentencia { get; set; }
        public string? tipo_sentido_sentencia { get; set; } = null!;
        public int id_dictamen_no_revision { get; set; }
        public int id_sentido_general_asunto { get; set; }
        public string? sentido_general_asunto { get; set; } = null!;
        public int id_tipo_sentido_general { get; set; }
        public string? tipo_sentido_general { get; set; } = null!;
        public string oficio_comunicacion_autoridad { get; set; } = null!;
        public string fecha_oficio_comunicacion_sentencia { get; set; } = null!;
        public string fecha_presentacion_oficio_comunicacion { get; set; } = null!;
        public string fecha_recepcion_auto_sentencia_ejecutoria { get; set; } = null!;
        public string comunicado_acuerdo_firmeza { get; set; } = null!;
        public string fecha_comunicacion_acuerdo_firmeza { get; set; } = null!;
        public string fecha_conclusion_expediente { get; set; } = null!;

    }
}
