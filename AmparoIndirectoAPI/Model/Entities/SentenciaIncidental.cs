namespace AmparoIndirectoAPI.Model.Entities
{
    public class SentenciaIncidental
    {
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public DateTime? fecha_notificacion_sentencia { get; set; } = null!;
        public int? id_sentido_suspencion_definitiva { get; set; }
        public int? id_otorgamiento_garatia { get; set; }
        public int? id_sentido_general_asunto { get; set; }
        public int? id_tipo_sentido_general_asunto { get; set; }
        public string? oficio_comunicacion_area_correspondiente { get; set; } = null!;
        public DateTime? fecha_comunicacion_suspencion { get; set; } = null!;
        public int? id_dictamen_no_revision_incidental { get; set; }
        public string? usuario { get; set; } = null!;


    }
}
