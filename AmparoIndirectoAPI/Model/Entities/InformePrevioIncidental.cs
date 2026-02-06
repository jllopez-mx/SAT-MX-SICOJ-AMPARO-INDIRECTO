namespace AmparoIndirectoAPI.Model.Entities
{
    public class InformePrevioIncidental
    {
        public int id { get; set; }
        public int? id_autoridad_responsable { get; set; }
        public DateTime fecha_apertura_incidente { get; set; }
        public DateTime? fecha_vencimiento { get; set; }
        public string? numero_oficio_informe_previo { get; set; } = null!;
        public DateTime? fecha_presentacion_informe_previo { get; set; }
        public string? usuario { get; set; } = null!;
        public bool? activo { get; set; } = null!;

    }
}
