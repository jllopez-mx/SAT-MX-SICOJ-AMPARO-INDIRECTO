using AmparoIndirectoAPI.Model.ViewModels;
using Sicoj.Utils.Redis;
using Sicoj.Utils.ViewModels;


namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseAmparoIndirectoAdministradorBandeja : PortadorClass
    {

        public string fecha_recepcion { get; set; } = null!;

        public string fecha_vencimiento { get; set; } = null!;

        public string numero_expediente { get; set; } = null!;

        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico

        public int id_juzgado { get; set; } = new();
        public string? juzgado { get; set; }

        public string nombre_quejoso { get; set; } = null!;

        public int id_materia { get; set; } = new();
        public string? materia { get; set; }

        public int id_submateria { get; set; } = new();
        public string? submateria { get; set; }
        

        public int id_tipo_acto { get; set; } = new();
        public string? tipo_acto { get; set; }

        public string despacho { get; set; } = null!;

        public string rfc_quejoso { get; set; } = null!;

        public int id_estado_tarea { get; set; } = new();
        public string? estado_tarea { get; set; }
        public int id_autoridad_responsable { get; set; } = new();
        public string? autoridad_responsable { get; set; }

        public int id_estado_procesal { get; set; } = new();
        public string? estado_procesal { get; set; }
        public int id_estado_procesal_incidental { get; set; } = new();
        public string? estado_procesal_incidental { get; set; }

        public bool activo { get; set; }

        public int id_alerta { get; set; } 

        public string alerta { get; set;} = null!;



    }
}
