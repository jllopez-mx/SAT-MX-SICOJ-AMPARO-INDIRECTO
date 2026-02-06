namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestDeclinarCompetencia
    {
        public int id { get; set; }
        public int id_administracion_central { get; set; }
        public string nombre_quejoso { get; set; } = null!;
        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico
        public int id_juzgado { get; set; }
        public string nombre_juzgado { get; set; } = null!;
        public string id_numero_asunto_destino { get; set; } = null!;
        //public string id_juicio_amparo_destino { get; set; } = null!; se modifico
        public int id_juzgado_destino { get; set; }
        public string nombre_juzgado_destino { get; set; } = null!;
        public string numero_expediente { get; set; } = null!;
    }
}
