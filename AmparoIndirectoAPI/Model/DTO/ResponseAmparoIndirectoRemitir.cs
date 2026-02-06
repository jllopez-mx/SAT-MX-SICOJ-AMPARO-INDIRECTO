namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseAmparoIndirectoRemitir
    {
        public int id { get; set; }
        public string numero_oficio_canalizacion { get; set; } = null!;
        public string fecha_canalizacion { get; set; } = null!;
        public string unidad_administrativa_canaliza_asunto { get; set; } = null!;
        public string unidad_administrativa_recibe_asunto { get; set; } = null!;
        public string motivo_canaliza { get; set; } = null!;
    }
}
