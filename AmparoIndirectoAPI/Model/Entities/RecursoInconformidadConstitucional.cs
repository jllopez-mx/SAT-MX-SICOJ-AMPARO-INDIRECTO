namespace AmparoIndirectoAPI.Model.Entities
{
    public class RecursoInconformidadConstitucional
    {
        public int id { get; set; }
        public DateTime? notificacion_admision_recurso_inconformidad { get; set; } = null!;
        public string? numero_recurso_inconformidad { get; set; } = null!;
        public int? id_organo_radicacion_inconformidad { get; set; }
        public DateTime? fecha_notificacion_resolucion_inconformidad { get; set; } = null!;
        public int? id_sentido_resolucion_inconformidad { get; set; }
        public string? usuario {  get; set; }
    }
}
