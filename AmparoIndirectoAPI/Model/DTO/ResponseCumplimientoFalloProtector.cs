using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseCumplimientoFalloProtector
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; } = new ();
        public string? autoridad_responsable { get; set; } = null!;
        //public bool cumplimiento_fallo { get; set; }
        public string? fecha_notificacion_requerimiento { get; set; } = null!;
        public int id_plazo_fallo { get; set; } = new();
        public string plazo_fallo { get; set; } = null!;
        public string? fecha_vencimiento_requerimiento { get; set; } = null!;
        public string? fecha_presentacion_fallo { get; set; } = null!;
        public string? numero_oficio_atencion { get; set; } = null!;
        public string? fecha_notificacion_acuerdo_fallo { get; set; } = null!;
        public string? fecha_oficio_comunicacion_fallo { get; set; } = null!;
        public string? numero_oficio_comunicacion_autoridad { get; set; } = null!;
        public bool ? activo {  get; set; }
    }
}
