namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseNotaLitigioById
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public string fecha_registro_nota { get; set; } = null!;
        public int seccion { get; set; }
    }
}
