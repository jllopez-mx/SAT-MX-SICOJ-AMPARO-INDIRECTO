namespace AmparoIndirectoAPI.Model.DTO
{
    public class InformacionAdicional
    {
        public int? id { get; set; }
        public DateTime? fecha_resolucion_oficio { get; set; }
        public decimal? cuantia { get; set; }
        public string? oficio_resolucion_reclamado { get; set; } = null!;
        public int? id_autoridad_emisora_resolucion_oficio { get; set; }
        public string? concepto_violacion { get; set; } = null!;
        public string? usuario { get; set; } = null!;
    }
}
