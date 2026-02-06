namespace AmparoIndirectoAPI.Model.Entities
{
    public class SolicitudTransparencia
    {
        public int id { get; set; }
        public string numero_solicitud { get; set; } = null!;
        public DateTime fecha_solicitud { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public string usuario {  get; set; } = null !;
    }
}
