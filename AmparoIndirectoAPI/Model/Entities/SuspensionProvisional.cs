namespace AmparoIndirectoAPI.Model.Entities
{
    public class SuspensionProvisional
    {
        public int? id { get; set; }
        public bool? suspension_provisional { get; set; }
        public int? otorgamiento_garantia { get; set; }
        public string? oficio_comunicacion { get; set; } = null!;

        public DateTime? fecha_comunicacion { get; set; } = null!;
        //public string fecha_creacion { get; set; } = null!;
        //public string fecha_modificacion { get; set; } = null!;
        public string? usuario_modificacion { get; set; } = null!;
        //public string id_estado_procesal { get; set; } = null!;
        public bool activo { get; set; }
    }
}
