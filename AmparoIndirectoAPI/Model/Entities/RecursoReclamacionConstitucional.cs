namespace AmparoIndirectoAPI.Model.Entities
{
    public class RecursoReclamacionConstitucional
    {
        public int id { get; set; }
        //public int id_autoridad_responsable { get; set; }
        public bool? recurso_reclamacion { get; set; }
        public DateTime fecha_notificacion_acuerdo { get; set; }
        public DateTime? notificacion_admision_recurso_reclamacion { get; set; } = null!;
        public DateTime? fecha_vencimiento_recurso_reclamacion { get; set; } = null!;
        public DateTime? fecha_presentacion { get; set; } = null!;
        public string? oficio_reclamacion { get; set; } = null!;
        public string? numero_recurso { get; set; } = null!;
        public int? id_organo_radicacion { get; set; }
        public DateTime? fecha_notificacion_resolucion { get; set; } = null!;
        public int? id_sentido_resolucion_reclamacion { get; set; }
        public string? usuario { get; set; }
    }
}
