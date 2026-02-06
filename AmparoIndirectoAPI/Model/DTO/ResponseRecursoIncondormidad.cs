using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseRecursoIncondormidad
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public string notificacion_admision_recurso_inconformidad { get; set; } = null!;
        public string numero_recurso_inconformidad { get; set; } = null!;
        public int id_organo_radicacion_inconformidad { get; set; } = new ();
        public string? organo_radicacion_inconformidad { get; set; } = null!;
        public string fecha_notificacion_resolucion_inconformidad { get; set; } = null!;
        public int id_sentido_resolucion_inconformidad { get; set; } = new();
        public string? sentido_resolucion_inconformidad { get; set; } = null!;
        public string usuario { get; set; } = null!;
        public string fecha_modificacion { get; set; } = null!;
        public bool activo { get; set; }
    }
}
