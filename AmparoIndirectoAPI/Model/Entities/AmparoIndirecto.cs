namespace AmparoIndirectoAPI.Model.Entities
{
    public class AmparoIndirecto
    {
        public int id { set; get; }
        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico
        public string numero_expediente { get; set; } = null!;
        public DateTime fecha_recepcion_demanda { get; set; }
        //public DateTime fecha_recepcion { get; set; }
        public int id_juzgado { get; set; } = new();
        //public string numero_oficio_segunda_pieza { get; set; } = null!;
        public bool contribuyente { get; set; }
        public string rfc_quejoso { get; set; } = null!;
        public string nombre_quejoso { get; set; } = null!;
        
        public int id_materia { get; set; } = new();
        
        public int id_submateria { get; set; } = new();
        
        public int id_tipo_acto { get; set; } = new();
        public string id_abogado { get; set; } = null!;
        public string despacho { get; set; } = null!;

        public int id_autoridad_responsable { get; set; }

        public int id_administracion { get; set; }
        public int id_subadministracion { get; set; }
        public int id_estado_tarea { get; set; }
        public int id_estado_procesal { get; set; }
        public int id_estado_procesal_incidental { get; set; }
        public bool recurso_queja_principal { get; set; }
        public bool recurso_revision_principal { get; set; }
        public bool recurso_queja_incidental { get; set; }
        public bool recurso_revision_incidental { get; set; }
        public bool activo { get; set; }
        public string numero_empleado { get; set; } = null!;
        public string motivo_reasignacion { get; set; } = null!;
        public DateTime fecha_turnado { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }
        //SE agrega para fecha vencimiento
        public DateTime fecha_vecimiento_demanda { get; set; }
    }
}
