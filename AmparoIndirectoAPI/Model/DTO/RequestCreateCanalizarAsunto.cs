namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateCanalizarAsunto
    {
        public string fecha_canalizacion { get; set; } = null!;
        public string numero_oficio_canalizacion { get; set; } = null!;
        public int unidad_administrativa_canaliza { get; set; }
        public int unidad_administrativa_recibe { get; set; }
        public string motivo_canalizacion { get; set; } = null!;
        public IFormFile? documento { get; set; }
        public int id_tipo_documento { get; set; }
        public int id_seccion { get; set; }
    }
}
