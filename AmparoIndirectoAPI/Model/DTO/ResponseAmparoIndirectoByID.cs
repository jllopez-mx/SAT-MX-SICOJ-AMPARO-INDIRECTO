using Sicoj.Utils.Redis;
using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseAutoridadesResponsables
    {
        public int id { get; set; }

        //public int id_juicio_amparo { get; set; } se modifico
        public int id_numero_asunto { get; set; }
        public int id_autoridad_responsable { get; set; } = new();
        public string? autoridad_responsable { get; set; } = null!;
    }
    public class ResponseAmparoIndirectoByID : PortadorClass
    {
        //public int id { set; get; }

        //public string juicio_amparo { get; set; } = null!; se modifico
        public string numero_asunto { get; set; } = null!;

        public string numero_expediente { get; set; } = null!;

        public string fecha_recepcion_demanda { get; set; } = null!;

        public int id_juzgado { get; set; } = new();
        public string? juzgado { get; set; } = null!;

        public bool contribuyente { get; set; }

        public string rfc_quejoso { get; set; } = null!;

        public string nombre_quejoso { get; set; } = null!;

        public int id_materia { get; set; } = new();
        public string? materia { get; set; } = null!;

        public int id_submateria { get; set; } = new();
        public string? submateria { get; set; } = null!;

        public int id_tipo_acto { get; set; } = new();
        public string? tipo_acto { get; set; } = null!;

        public string despacho { get; set; } = null!;

        public List<ResponseAutoridadesResponsables> list_autoridad_responsable { get; set; } = new();

        public int id_administracion { get; set; } = new();
        public string? administracion { get; set; } = null;

        public int id_subadministracion { get; set; } = new();
        public string subadministracion { get; set; } = null!;

        public int id_estado_tarea { get; set; } = new();
        public string? estado_tarea { get; set; } = null!;

        public int id_estado_procesal { get; set; } = new();
        public string? estado_procesal { get; set; } = null!;
        public int id_estado_procesal_incidental { get; set; } = new();
        public string? estado_procesal_incidental { get; set; } = null!;
        public bool recurso_queja_principal { get; set; }
        public bool recurso_revision_principal { get; set; }
        public bool recurso_queja_incidental { get; set; }
        public bool recurso_revision_incidental { get; set; }
        public bool activo { get; set; }
        public string numero_empleado { get; set; } = null!;
        public string fecha_turnado { get; set; } = null!;
        public string fecha_creacion { get; set; } = null!;
        public string fecha_modificacion { get; set; } = null!;
    }
}
