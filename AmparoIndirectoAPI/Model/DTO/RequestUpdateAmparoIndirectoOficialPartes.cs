namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestUpdateAmparoIndirectoOficialPartes
    {
        public int id { get; set; }
        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico
        public string numero_expediente { get; set; } = null!;
        public string fecha_recepcion_demanda { get; set; } = null!;
        public int id_juzgado { get; set; }
        //public string numero_oficio_segunda_pieza { get; set; } = null!;
        public string rfc_quejoso { get; set; } = null!;
        public string nombre_quejoso { get; set; } = null!;
        public int id_materia { get; set; }
        public int id_submateria { get; set; }
        public int id_tipo_acto { get; set; }
        //public string despacho { get; set; } = null!;
        //public int id_autoridad_responsable { get; set; }
        public int id_administracion { get; set; }
        public int id_subadministracion { get; set; }

    }
}
