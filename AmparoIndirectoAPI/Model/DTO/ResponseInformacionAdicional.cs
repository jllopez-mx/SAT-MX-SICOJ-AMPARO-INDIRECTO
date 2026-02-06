using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseInformacionAdicional
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public string fecha_resolucion_oficio { get; set; } = null!;
        public decimal cuantia { get; set; }
        public string oficio_resolucion_reclamado { get; set; } = null!;
        public int id_autoridad_emisora_resolucion_oficio { get; set; } = new();
        public string? autoridad_emisora_resolucion_oficio { get; set; } = null!;
        //public List<int> concepto_violacion { get; set; } = new List<int>();
        public string concepto_violacion { get; set; } = null!;
        public int id_tipo_acto { get; set; }
        public string usuario { get; set; } = null!;
        public string fecha_modificacion { get; set; } = null!;
        public bool activo { get; set; }
    }
}
