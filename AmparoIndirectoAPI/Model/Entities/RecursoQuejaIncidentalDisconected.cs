namespace AmparoIndirectoAPI.Model.Entities
{
    public class RecursoQuejaIncidentalDisconected
    {
        public int? id { get; set; }
        public int? id_numero_asunto { get; set; }
        //public int? id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public bool? recurso_queja_incidental { get; set; }
        public int? id_recurrente { get; set; }
        public string? fecha_recepcion_apertura { get; set; }
        public string? fecha_vencimiento_recurso_queja { get; set; }
        public string? oficio_queja { get; set; } = null!;
        public string? fecha_presentacion_recurso_queja { get; set; }
        public string? fecha_admision { get; set; }
        public int? id_organo_radicacion { get; set; }
        public string? toca_queja_incidental { get; set; } = null!;
        public string? fecha_notificacion_ejecutoria { get; set; }
        public int? id_sentido_resolucion_ejecutoria { get; set; }
        public string? oficio_comunicacion_area_correspondiente { get; set; } = null!;
        public string? fecha_oficio_comunicacion { get; set; }
    }
}
