namespace AmparoIndirectoAPI.Model.Entities
{
    public class CuadernoConstitucional
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public bool solucion_opinion_tecnica { get; set; }
        public string fecha_recepcion_demanda { get; set; } = null!;
        public string fecha_vencimiento_justificado { get; set; } = null!;
        public string numero_oficio_informe_justificado { get; set; } = null!;
        public string fecha_presentacion_informe_justificado { get; set; } = null!;
        public string observaciones_justificado { get; set; } = null!;
        public string fecha_notificacion_sentencia { get; set; } = null!;
        public int id_sentido_sentencia { get; set; }
        public int id_tipo_sentido_sentencia { get; set; }
        public int id_dictamen_no_revision { get; set; }
        public int id_sentido_general_asunto { get; set; }
        public int id_tipo_sentido_general { get; set; }
        public string oficio_comunicacion_autoridad { get; set; } = null!;
        public string fecha_oficio_comunicacion_sentencia { get; set; } = null!;
        public string fecha_recepcion_auto_sentencia_ejecutoria { get; set; } = null!;
        public string comunicado_acuerdo_firmeza { get; set; } = null!;
        public string fecha_conclusion_expediente { get; set; } = null!;
        public bool cumplimiento { get; set; }
        public string fecha_notificacion_requerimiento { get; set; } = null!;
        public int plazo_fallo { get; set; }
        public string fecha_vencimiento_requerimiento { get; set; } = null!;
        public string fecha_presentacion_fallo { get; set; } = null!;
        public string numero_oficio_atencion { get; set; } = null!;
        public string fecha_notificacion_acuerdo_fallo { get; set; } = null!;
        public string fecha_oficio_comunicacion_fallo { get; set; } = null!;
        public string numero_oficio_comunicacion_autoridad { get; set; } = null!;
        public string fecha_recepcion_acuerdo_queja { get; set; } = null!;
        public string fecha_vencimiento_recurso_queja { get; set; } = null!;
        public string oficio_recurso_queja { get; set; } = null!;
        public string fecha_presentacion_queja { get; set; } = null!;
        public int id_motivo_recurso_queja { get; set; }
        public string fecha_adminision_queja { get; set; } = null !;
        public int id_organo_radicacion_queja { get; set; }
        public string toca_queja { get; set; } = null!;
        public string fecha_notificacion_ejecutoria { get; set; } = null!;
        public int id_sentido_resolucion_queja { get; set; }
        public string oficio_comunicacion_area_correspondiente { get; set; } = null!;
        public string fecha_oficio_comunicacion { get; set; } = null!;
        public string fecha_vencimiento_recurso_revision { get; set; } = null!;
        public string oficio_recurso { get; set; } = null!;
        public string fecha_presentacion_revision { get; set; } = null!;
        public string fecha_adminision { get; set; } = null!;
        public int id_organo_radicacion_revision { get; set; }
        public string toca_revision { get; set; } = null !;
        public bool revision_adhesiva { get; set; }
        public string oficio_revision_adhesiva { get; set; } = null!;
        public string fecha_vecimiento_adhesion { get; set; } = null!;
        public string fecha_notificacion_ejecutoria_revision { get; set; } = null!;
        public int id_sentido_resolucion_revision { get; set; }
        public int id_tipo_sentido_revision { get; set; }
        public int id_sentido_general { get; set; }
        public int id_tipo_sentido_general_revision { get; set; }
        public string oficio_comunicacion_autoridad_revision { get; set; } = null!;
        public string fecha_oficio_comunicacion_revision { get; set; } = null!;
        public string notificacion_admision_recurso_inconformidad { get; set; } = null!;
        public string numero_recurso_inconformidad { get; set; } = null!;
        public int id_organo_radicacion_inconformidad { get; set; }
        public string fecha_notificacion_resolucion_inconformidad { get; set; } = null!;
        public int id_sentido_resolucion_inconformidad { get; set; }
        public bool recurso_reclamacion { get; set; }
        public string notificacion_adminsion_recurso_reclamacion { get; set; } = null!;
        public string fecha_vencimiento_recurso_reclamacion { get; set; } = null!;
        public string fecha_presentacion { get; set; } = null!;
        public string oficio_reclamacion { get; set; } = null!;
        public string numero_recurso { get; set; } = null!;
        public int id_organo_radicacion { get; set; }
        public string fecha_notificacion_resolucion { get; set; } = null!;
        public int id_sentido_resolucion_reclamacion { get; set; }

    }
}
