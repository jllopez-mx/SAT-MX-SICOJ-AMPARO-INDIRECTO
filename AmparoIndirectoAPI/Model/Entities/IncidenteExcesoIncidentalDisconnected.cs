namespace AmparoIndirectoAPI.Model.Entities
{
    public class IncidenteExcesoIncidentalDisconnected
    {
        public int? id { get; set; }
        public int? id_numero_asunto { get; set; }
        //public int? id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public bool? interposicion_incidente { get; set; }
        public string? fecha_notificacion_acuerdo { get; set; } = null!;
        public string? oficio_desahogo { get; set; } = null!;
        public string? fecha_oficio_desahogo { get; set; } = null!;
        public int? id_sentido { get; set; }
        public string? fecha_notificacion_resolucion { get; set; } = null!;
        public string? oficio_comunicacion_autoridad { get; set; } = null!;
        public string? fecha_oficio_comunicacion { get; set; } = null!;
        public string? usuario { get; set; } = null!;
        public bool activo { get; set; }
    }
}
