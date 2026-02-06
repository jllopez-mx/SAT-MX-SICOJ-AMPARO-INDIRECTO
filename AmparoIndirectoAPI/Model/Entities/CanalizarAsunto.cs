namespace AmparoIndirectoAPI.Model.Entities
{
    public class CanalizarAsunto
    {
        public DateTime fecha_canalizacion { get; set; }
        public string numero_oficio_canalizacion { get; set; } = null!;
        public int unidad_administrativa_canaliza { get; set; }
        public int unidad_administrativa_recibe { get; set; }
        public string motivo_canalizacion { get; set; } = null!;
        public string usuario { get; set; } = null!;
    }
}
