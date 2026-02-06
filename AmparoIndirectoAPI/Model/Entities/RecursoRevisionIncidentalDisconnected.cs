namespace AmparoIndirectoAPI.Model.Entities
{
    public class RecursoRevisionIncidentalDisconnected
    {
        public int? id { get; set; }
        public int? id_numero_asunto { get; set; }
        //public int? id_juicio_amparo { get; set; } //se modifico
        public int id_autoridad_responsable { get; set; }
        public bool? recurso_revision_incidental { get; set; }
        public int? id_recurrente { get; set; }
        public string? fecha_recepcion_sentencia { get; set; } = null!;
        public string? fecha_vencimiento_recurso_revision { get; set; } = null!;
        public string? oficio_recurso { get; set; } = null!;
        public string? fecha_presentacion_recurso_revision { get; set; } = null !;
        public string? fecha_admision { get; set; } = null!;
        public int? id_organo_radicacion { get; set; }
        public string? toca_recurso_revision_incidental { get; set; } = null!;
        public bool? revision_adhesiva { get; set; }
        public string? oficio_revision_adhesiva { get; set; } = null!;
        public string? fecha_vencimiento_adhesion { get; set; } = null!;
        public string? fecha_presentacion_adhesiva { get; set; } = null!;
        public string? fecha_notificacion_resolucion { get; set; } = null!;
        public int? id_sentido_resolucion { get; set; }
        public int? id_tipo_sentido { get; set; }
        public int? id_sentido_general_asunto { get; set; }
        public int? id_tipo_sentido_general { get; set; }
        public string? oficio_comunicacion_autoridad { get; set; } = null!;
        public string? fecha_oficio_comunicacion { get; set; } = null!;
        public string? usuario { get; set; } = null!;
        public bool? activo { get; set; }
    }
}
