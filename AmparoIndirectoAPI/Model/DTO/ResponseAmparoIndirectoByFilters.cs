using AmparoIndirectoAPI.Model.ViewModels;
using Sicoj.Utils.ViewModels;
//using sicoj_utils_csharp.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseAmparoIndirectoByFilters
    {
        public int id { set; get; }

        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico

        public string numero_expediente { get; set; } = null!;

        public string fecha_recepcion { get; set; } = null!;

        public string fecha_vencimiento_demanda { get; set; } = null!;

        public int id_juzgado { get; set; } = new();
        public string? juzgado { get; set; } = null!;
        
        public string rfc_quejoso { get; set; } = null!;

        public string nombre_quejoso { get; set; } = null!;

        public int id_materia { get; set; } = new();
        public string? materia { get; set; } = null!;
        
        public int id_submateria { get; set; } = new();
        public string? submateria { get; set; } = null!;
        
        public int id_tipo_acto { get; set; } = new();
        public string? tipo_acto { get; set; } = null!;
        
        public string despacho { get; set; } = null!;
        public int numero_autoridad { get; set; }

        public int id_administracion { get; set; } = new();
        public string? administracion { get; set; } = null!;
        
        public int id_subadministracion { get; set; } = new();
        public string? subadministracion { get; set; } = null;

        //public CatalogView id_autoridad_responsable { get; set; } = new();

        public int id_estado_tarea { get; set; } = new();
        public string? estado_tarea { get; set; } = null!;  

        public int id_estado_procesal { get; set; } = new();
        public string? estado_procesal { get; set; } = null!;
        public int id_estado_procesal_incidental { get; set; } = new();
        public string? estado_procesal_incidental { get; set; } = null!;
        
        public bool transparencia { get; set; }

        public bool activo { get; set; }

    }
}
