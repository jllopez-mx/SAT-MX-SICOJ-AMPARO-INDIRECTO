namespace AmparoIndirectoAPI.Model.Entities
{
    public class DeclinarCompetencia
    {
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public string numero_oficio { get; set; } = null!;
        public DateTime fecha_recepcion_declinacion { get; set; } = new();
        public int id_juzgado_origen { get; set; }
        public int id_juzgado_destino { get; set; }
        public string numero_asunto_origen { get; set; } = null!;
        //public string juicio_amparo_origen { get; set; } = null!; se modifico
        public string numero_asunto_destino { get; set; } = null!;
        //public string juicio_amparo_destino { get; set; } = null!; se modifico
        public string usuario { get; set; } = null!;
    }
}
