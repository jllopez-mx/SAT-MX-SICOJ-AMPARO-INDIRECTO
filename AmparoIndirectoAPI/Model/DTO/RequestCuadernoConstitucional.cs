using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCuadernoConstitucional
    {
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public string autoridad_responsable { get; set; } = null!;
        //public int id_autoridad_responsable { get; set; }
        public bool solucion_opinion_tecnica { get; set; }
        public string fecha_recepcion_demanda { get; set; } = null!;
        public string fecha_vencimiento_justificado { get; set; } = null!;
        public string numero_oficio_informe_justificado { get; set; } = null!;
        public string fecha_presentacion_informe_justificado { get; set; } = null!;
        public string observaciones_justificado { get; set; } = null!;
        public string fecha_notificacion_sentencia { get; set; } = null!;
        public int id_sentido_sentencia { get; set; }
        public string? sentido_sentencia { get; set; } = null!;
        public int id_tipo_sentido_sentencia { get; set; }
        public string? tipo_sentido_sentencia { get; set; } = null!;
        public int id_dictamen_no_revision { get; set; }
        public string? dictamen_no_revision { get; set; } = null!;
        public int id_sentido_general_asunto { get; set; }
        public string? sentido_general_asunto { get; set; } = null!;
        public int id_tipo_sentido_general { get; set; }
        public string? tipo_sentido_general { get; set; } = null!;
        public string oficio_comunicacion_autoridad { get; set; } = null!;
        public string fecha_oficio_comunicacion_sentencia { get; set; } = null!;
        public string fecha_recepcion_auto_sentencia_ejecutoria { get; set; } = null!;
        public string comunicado_acuerdo_firmeza { get; set; } = null!;
        public string fecha_conclusion_expediente { get; set; } = null!;
        public bool recurso_queja_principal { get; set; }
        public int id_recurrente { get; set; }
        public string fecha_recepcion_acuerdo_queja { get; set; } = null!;
        public string fecha_vencimiento_recurso_queja { get; set; } = null!;
        public string oficio_recurso_queja { get; set; } = null!;
        public string fecha_presentacion_queja { get; set; } = null!;
        public int id_motivo_recurso_queja { get; set; }
        public string? motivo_recurso_queja { get; set; } = null!;
        public string fecha_admision_queja { get; set; } = null!;
        public int id_organo_radicacion_queja { get; set; }
        public string? organo_radicacion_queja { get; set; } = null!;
        public string toca_queja { get; set; } = null!;
        public string fecha_notificacion_ejecutoria { get; set; } = null!;
        public int id_sentido_resolucion_queja { get; set; }
        public string? sentido_resolucion_queja { get; set; } = null!;
        public string oficio_comunicacion_area_correspondiente { get; set; } = null!;
        public string fecha_oficio_comunicacion { get; set; } = null!;
        public bool recurso_revision_principal { get; set; }
        public string fecha_recepcion_sentencia { get; set; } = null!;
        public string fecha_vencimiento_recurso_revision { get; set; } = null!;
        public string oficio_recurso { get; set; } = null!;
        public string fecha_presentacion_revision { get; set; } = null!;
        public string fecha_admision { get; set; } = null!;
        public int id_organo_radicacion_revision { get; set; }
        public string? organo_radicacion_revision { get; set; } = null!;
        public string toca_revision { get; set; } = null!;
        public bool revision_adhesiva { get; set; }
        public string oficio_revision_adhesiva { get; set; } = null!;
        public string fecha_vecimiento_adhesion { get; set; } = null!;
        public string fecha_notificacion_ejecutoria_revision { get; set; } = null!;
        public int id_sentido_resolucion_revision { get; set; }
        public string? sentido_resolucion_revision { get; set; } = null!;
        public int id_tipo_sentido_revision { get; set; }
        public string? tipo_sentido_revision { get; set; } = null!;
        public int id_sentido_general { get; set; }
        public string? sentido_general { get; set; } = null!;
        public int id_tipo_sentido_general_revision { get; set; }
        public string? tipo_sentido_general_revision { get; set; } = null!;
        public string oficio_comunicacion_autoridad_revision { get; set; } = null!;
        public string fecha_oficio_comunicacion_revision { get; set; } = null!;
        public bool activo { get; set; }

    }
}
