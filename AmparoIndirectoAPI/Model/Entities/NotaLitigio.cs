namespace AmparoIndirectoAPI.Model.Entities
{
    public class NotaLitigio
    {
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public DateTime fecha_registro_nota { get; set; }
        public int id_seccion { get; set; }
        public int id_tipo_documento { get; set; }
        public string usuario { get; set; } = null!;
    }
}
