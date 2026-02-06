using System.ComponentModel.DataAnnotations;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateAmparoIndirecto
    {
        public string numero_asunto { get; set; } = null!; //00000/2025 <---- numero_asunto
        //public string juicio_amparo { get; set; } = null!; //00000/2025 <---- numero_asunto
        public string numero_expediente { get; set; } = null!;
        public string fecha_recepcion_demanda { get; set; } = null !;
        public int id_juzgado { get; set; }
        public bool contribuyente { get; set; }
        public string rfc_quejoso { get; set; } = null!;
        public string nombre_quejoso { get; set; } = null!;
        public int id_materia { get; set; }
        public int id_submateria { get; set; }
        public int id_tipo_acto { get; set; }
        public string despacho { get; set; } = null!;
        public int id_autoridad_responsable { get; set; }
        public int id_administracion_central { get; set; }

       
    }
}
