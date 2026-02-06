namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseDocumentoAmparoIndirecto
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
        public bool remplazable { get; set; }
        public string usuario { get; set; } = null!;
        public bool activo { get; set; }
        public string? fecha_creacion { get; set; } = null!;
        public string usuario_modificacion { get; set; } = null!;
        public string? fecha_modificacion { get; set; } = null!;


    }
}
