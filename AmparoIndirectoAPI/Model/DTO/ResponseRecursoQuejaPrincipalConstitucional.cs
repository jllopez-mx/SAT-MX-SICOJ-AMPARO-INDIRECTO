using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseRecursoQuejaPrincipalConstitucional
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public string? autoridad_responsable { get; set; } = null!;
        public bool recurso_queja_principal {  get; set; }
        public int id_recurrente {  get; set; }
        public string fecha_recepcion_acuerdo_queja { get; set; } = null!;
        public string fecha_vencimiento_recurso_queja { get; set; } = null!;
        public string oficio_recurso_queja { get; set; } = null!;

        public string fecha_admision_queja { get; set; } = null!;
        public int id_motivo_recurso_queja { get; set; } = new();
        public string? motivo_recurso_queja { get; set; } = null!;
        public string fecha_notificacion_ejecutoria { get; set; } = null!;
        public string fecha_presentacion_queja { get; set; } = null!;
        public int id_sentido_resolucion_queja { get; set; } = new();
        public string? sentido_resolucion_queja { get; set; } = null!;
        public int id_organo_radicacion_queja { get; set; } = new();
        public string? organo_radicacion_queja { get; set; } = null!;
        public string toca_queja { get; set; } = null!;
        public string oficio_comunicacion_area_correspondiente { get; set; } = null!;
        public string fecha_oficio_comunicacion { get; set; } = null!;
        public bool activo { get; set; } 


    }
}
