namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseSolicitudTransparenciaLista
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public string numero_solicitud { get; set; } = null!;
        public string fecha_solicitud { get; set; } = null!;

    }
}
