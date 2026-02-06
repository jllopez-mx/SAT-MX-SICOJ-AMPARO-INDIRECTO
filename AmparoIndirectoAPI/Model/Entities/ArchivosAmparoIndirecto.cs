namespace AmparoIndirectoAPI.Model.Entities
{
    public class ArchivosAmparoIndirecto
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_seccion { get; set; }
        public int id_tipo_documento { get; set; }
        public string file_name { get; set; } = null!;
        public string file_path { get; set; } = null!;
        public string content_type { get; set; } = null!;
        public string file_size { get; set; } = null!;
        public int id_unidad_administrativa { get; set; }
        public int id_rol { get; set; }
        public bool permanente { get; set; }
        public bool reemplazable { get; set; }
        public DateTime fecha_creacion { get; set; }
        public string usuario { get; set; } = null!;
        public string usuario_modificacion { get; set; } = null!;
        public DateTime fecha_modificacion { get; set; }
        public bool activo { get; set; }

    }
}
