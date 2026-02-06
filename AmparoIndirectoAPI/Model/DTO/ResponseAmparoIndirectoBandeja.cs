using AmparoIndirectoAPI.Model.ViewModels;
using Sicoj.Utils.Redis;
using Sicoj.Utils.ViewModels;


namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseAmparoIndirectoBandeja : PortadorClass
    {
        public string fecha_recepcion { get; set; } = null!;

        public string numero_expediente { get; set; } = null!;

        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico

        public int id_juzgado { get; set; } = new();
        public string? juzgado { get; set; } = null!;

        public string nombre_quejoso { get; set; } = null!;

        public int id_materia { get; set; } = new();
        public string? materia { get; set; } = null!;

        public int id_submateria { get; set; } = new();
        public string? submateria { get; set; } = null!;
        

        public int id_tipo_acto { get; set; } = new();
        public string? tipo_acto { get; set; } = null!;

        public string despacho { get; set; } = null!;
        public int numero_autoridades { get; set; }

        public string rfc_quejoso { get; set; } = null!;

        public int id_estado_tarea { get; set; } = new();
        public string? estado_tarea { get; set; } = null!;

        public int id_estado_procesal { get; set; } = new();
        public string? estado_procesal { get; set; } = null!;

        public bool activo { get; set; }

        
    }
}
