namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateSolicitudTransparencia
    {
        public int id { get; set; }
        public string numero_solicitud { get; set; } = null!;
        public string fecha_solicitud { get; set; } = null!;
        public IFormFile? documento { get; set; }
        public int id_tipo_documento { get; set; }
        public int id_seccion { get; set; }
    }
}
