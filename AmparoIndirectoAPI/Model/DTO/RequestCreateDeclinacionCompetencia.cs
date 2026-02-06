namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateDeclinacionCompetencia
    {
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public string numero_oficio { get; set; } = null!;
        public string fecha_recepcion_declinacion { get; set; } = null!; 
        public int id_juzgado_destino { get; set; }
        public string numero_asunto_destino { get; set;} = null!;
        //public string juicio_amparo_destino { get; set;} = null!; se modifico
        public IFormFile? documento { get; set; }
        public int id_tipo_documento { get; set; }
        public int id_seccion { get; set; }

    }
}
