namespace AmparoIndirectoAPI.Model.Entities
{
    public class CumplimientoFalloProtectorConstitucional
    {
        public int id_numero_asunto { get; set; }
        //public int id_juicio amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        //public bool cumplimiento_fallo { get; set; }
        public DateTime  fecha_notificacion_requerimiento { get; set; }
        public int plazo_fallo { get; set; }
        public DateTime? fecha_vencimiento_requerimiento { get; set; }
        public DateTime ? fecha_presentacion_fallo { get; set; }
        public string ? numero_oficio_atencion { get; set; } = null!;
        public DateTime ? fecha_notificacion_acuerdo_fallo { get; set; }
        public DateTime ? fecha_oficio_comunicacion_fallo { get; set; }
        public string ? numero_oficio_comunicacion_autoridad { get; set; } = null!;
        public string ? usuario { get; set; } = null!;
    }
}
