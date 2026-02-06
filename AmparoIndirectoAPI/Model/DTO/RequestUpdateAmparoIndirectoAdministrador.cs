namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestUpdateAmparoIndirectoAdministrador
    {
        public int id { get; set; }
        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico
        public string numero_expediente { get; set; } = null!;
        public string fecha_recepcion_demanda { get; set; } = null!;
        //public string fecha_vencimiento { get; set; } = null!;
        public int id_juzgado { get; set; }
        public string rfc_quejoso { get; set; } = null!;
        public string nombre_quejoso { get; set; } = null!;
        public int id_materia { get; set; }
        public int id_submateria { get; set; }
        public int id_tipo_acto { get; set; }
        public string despacho { get; set; } = null!;
    }
}
