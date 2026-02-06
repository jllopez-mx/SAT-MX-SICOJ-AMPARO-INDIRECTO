using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseInformePrevioIncidental
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        //public int id_autoridad_responsable { get; set; }
        public int id_autoridad_responsable { get; set; }
        public string? autoridad_responsable { get; set; } = null!;
        public string fecha_apertura_incidente { get; set; } = null!;
        public string fecha_vencimiento { get; set; } = null!;
        public string numero_oficio_informe_previo { get; set; } = null!;
        public string fecha_presentacion_informe_previo { get; set; } = null!;
        public bool? activo { get; set; } = null!;
    }
}
