using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseSuspensionProvisionalIncidental
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public bool? suspension_provisional { get; set; }
        public int otorgamiento_garantia { get; set; }
        public string oficio_comunicacion_suspension { get; set; } = null!;
        public string? fecha_comunicacion { get; set; } = null!;
        public bool? activo { get; set; }

    }
}
