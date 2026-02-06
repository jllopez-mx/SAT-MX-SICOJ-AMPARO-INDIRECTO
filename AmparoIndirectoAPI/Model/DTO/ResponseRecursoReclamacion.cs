using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseRecursoReclamacion
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public bool recurso_reclamacion { get; set; }
        public string fecha_notificacion_acuerdo { get; set; } = null!;
        public string notificacion_admision_recurso_reclamacion { get; set; } = null!;
        public string fecha_vencimiento_recurso_reclamacion { get; set; } = null!;
        public string fecha_presentacion { get; set; } = null!;
        public string oficio_reclamacion { get; set; } = null!;
        public string numero_recurso { get; set; } = null!;
        public int id_organo_radicacion { get; set; } = new();
        public string? organo_radicacion { get; set; } = null!;
        public string fecha_notificacion_resolucion { get; set; } = null!;
        public int id_sentido_resolucion_reclamacion { get; set; } = new();
        public string? sentido_resolucion_reclamacion { get; set; } = null!;
        public string usuario { get; set; } = null!;
        public string fecha_modificacion{ get; set; } = null!;
        public bool activo { get; set; }

    }
}
