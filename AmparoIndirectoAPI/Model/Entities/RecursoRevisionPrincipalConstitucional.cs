namespace AmparoIndirectoAPI.Model.Entities
{
    public class RecursoRevisionPrincipalConstitucional
    {
        public int id { get; set; }
        public int? id_recurrente { get; set; }
        public bool? recurso_revision_principal { get; set; }
        public int id_autoridad_responsable { get; set; }
        public DateTime fecha_recepcion_sentencia { get; set; }
        public DateTime? fecha_vencimiento_recurso_revision { get; set; } = null!;
        public string? oficio_recurso { get; set; } = null!;
        public DateTime? fecha_presentacion_revision { get; set; } = null!;
        public DateTime fecha_admision { get; set; }
        public int? id_organo_radicacion_revision { get; set; }
        public string? toca_revision { get; set; } = null!;
        public bool? revision_adhesiva { get; set; }
        public string? oficio_revision_adhesiva { get; set; } = null!;
        public DateTime? fecha_vencimiento_adhesion { get; set; } = null!;
        public DateTime? fecha_notificacion_ejecutoria_revision { get; set; } = null!;
        public int? id_sentido_resolucion_revision { get; set; }
        public int? id_tipo_sentido_revision { get; set; }
        public int? id_sentido_general { get; set; }
        public int? id_tipo_sentido_general_revision { get; set; }
        public string? oficio_comunicacion_autoridad_revision { get; set; } = null!;
        public DateTime? fecha_oficio_comunicacion_revision { get; set; } = null!;
        public string? usuario { get; set; } = null!;

    }
}
