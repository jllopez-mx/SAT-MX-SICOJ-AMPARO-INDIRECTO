namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestRecursoQuejaIncidental
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; }//se modifico
        public int id_autoridad_responsable { get; set; }
        public bool recurso_queja { get; set; }
        public int recurrente { get; set; }
        public string? fecha_recepcion_apertura { get; set; } = null!;
        //public string? fecha_vencimiento_recurso_queja { get; set; } = null!;
        public string? oficio_queja { get; set; } = null!;
        public string? fecha_presentacion_recurso_queja { get; set; } = null!;
        public string? fecha_admision { get; set; } = null!;
        public int? organo_radicacion { get; set; }
        public string? toca { get; set; } = null!;
        public string? fecha_notificacion_ejecutoria { get; set; } = null!;
        public int? sentido_resolucion { get; set; }
        public string? oficio_comunicacion_area_correspondiente { get; set; } = null!;
        public string? fecha_oficio_comunicacion { get; set; } = null!;
    }
}
