namespace AmparoIndirectoAPI.Model.Entities
{
    public class RecursoQuejaIncidental
    {
        public int? id { get; set; }
        public int? id_numero_asunto { get; set; }
        //public int? id_juicio_amparo { get; set; } se modifico
        public int? id_autoridad_responsable { get; set; }
        public bool? recurso_queja { get; set; }
        public int? id_recurrente { get; set; }
        public DateTime fecha_recepcion_apertura { get; set; }
        public DateTime? fecha_vencimiento_recurso_queja { get; set; }
        public string? oficio_queja { get; set; } = null!;
        public DateTime? fecha_presentacion_recurso_queja { get; set; }
        public DateTime? fecha_admision { get; set; }
        public int? organo_radicacion { get; set; }
        public string? toca { get; set; } = null!;
        public DateTime? fecha_notificacion_ejecutoria { get; set; }
        public int? sentido_resolucion { get; set; }
        public string? oficio_comunicacion_area_correspondiente { get; set; } = null!;
        public DateTime? fecha_oficio_comunicacion { get; set; }
        public string? usuario { get; set; } = null!;
        public bool? activo { get; set; }
    }
}
