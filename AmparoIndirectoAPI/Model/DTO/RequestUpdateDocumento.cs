namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestUpdateDocumento
    {
        public IFormFile documento { get; set; } = null!;
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_tipo_documento { get; set; }
        public int id_seccion { get; set; }
        public int id_unidad_administrativa { get; set; }
    }
}
