namespace AmparoIndirectoAPI.Model.Entities
{
    public class CuadernoIncidental
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public bool suspension_provisional { get; set; }
        public int id_otorgamiento_garantia { get; set; }
        public string oficio_comunicacion_suspension_provisional { get; set; } = null!;
        public string fecha_comunicacion { get; set; } = null!;
        public string fecha_recepcion_apertura_incidentes { get; set; } = null!;
        public string fecha_vencimiento { get; set; } = null!;
        public string numero_oficio_informe_previo { get; set; } = null!;
        public string fecha_presentacion_informe_previo { get; set; } = null!;
        public string fecha_notificacion_sentencia { get; set; } = null!;
        public int id_sentido_suspension_definitiva { get; set; }
        public int id_otorgamiento_garantia_sentencia_incidental { get; set; }
        public string oficio_comunicacion_area_correspondiente { get; set; } = null !;
        public string fecha_comunicacion_suspension { get; set; } = null !;
        public int id_dictamen_no_revision_incidental { get; set; }
        public string fecha_recepcion_apertura { get; set; } = null!;
        public string fecha_vencimiento_recurso_queja { get; set; } = null!;
        public string oficio_queja { get; set; } = null!;
        public string fecha_adminision { get; set; } = null!;
        public int id_organo_radicacion { get; set; }
        public string numero_queja { get; set; } = null!;
        public string fecha_notificacion_ejecutoria { get; set; } = null!;
        public int id_sentido_resolucion { get; set; }
        public int id_oficio_comunicacion_queja { get; set; }
        public string fecha_oficio_comunicacion_queja { get; set; } = null!;
        public string fecha_recepcion_sentencia { get; set; } = null!;
        public string fecha_vencimiento_recurso_revision { get; set; } = null!;
        public string oficio_recurso_revision { get; set; } = null!;
        public string fecha_admision_revision { get; set; } = null!;
        public int id_organo_radicacion_revision { get; set; }
        public string toca { get; set; } = null!;
        public bool revision_adhesiva { get; set; }
        public string oficio_revision_adhesiva { get; set; } = null!;
        public string fecha_vencimiento_adhesion { get; set; } = null!;
        public string fecha_notificacion_ejecutoria_revision { get; set; } = null!;
        public int id_sentido_resolucion_revision { get; set; }
        public string oficio_comunicacion_autoridad_revision { get; set; } = null!;
        public bool interposicion_incidente { get; set; }
        public string fecha_notificacion_acuerdo { get; set; } = null!;
        public string oficio_desahogo { get; set; } = null!;
        public string fecha_oficio_desahogo { get; set; } = null!;
        public int id_sentido_notificacion_resolucion { get; set; }
        public string fecha_notificacion_resolucion { get; set; } =  null!;
        public bool activo { get; set; }
    }
}
