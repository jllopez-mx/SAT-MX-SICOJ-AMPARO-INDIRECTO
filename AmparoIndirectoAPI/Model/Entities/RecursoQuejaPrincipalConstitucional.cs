namespace AmparoIndirectoAPI.Model.Entities
{
    public class RecursoQuejaPrincipalConstitucional
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; } //SE AGREGA CAMPO PARA ACTUALIZAR
        //public int id_juicio amparo { get; set; } //SE AGREGA CAMPO PARA ACTUALIZAR se modifico
        public int? id_recurrente { get; set; }
        public bool? recurso_queja_principal { get; set; }
        public int? id_autoridad_responsable { get; set; }
        public DateTime fecha_recepcion_acuerdo_queja { get; set; }
        public DateTime? fecha_vencimiento_recurso_queja { get; set; } = null!;
        public string? oficio_recurso_queja { get; set; } = null!;
        public DateTime? fecha_presentacion_queja { get; set; } = null!;
        public int? id_motivo_recurso_queja { get; set; }
        public DateTime? fecha_admision_queja { get; set; } = null!;
        public int? id_organo_radicacion_queja { get; set; }
        public string? toca_queja { get; set; } = null!;
        public DateTime? fecha_notificacion_ejecutoria { get; set; } = null!;
        public int? id_sentido_resolucion_queja { get; set; }
        public string? oficio_comunicacion_area_correspondiente { get; set; } = null!;
        public DateTime? fecha_oficio_comunicacion { get; set; } = null!;
        public string? usuario { get; set; } = null!;

    }
}
