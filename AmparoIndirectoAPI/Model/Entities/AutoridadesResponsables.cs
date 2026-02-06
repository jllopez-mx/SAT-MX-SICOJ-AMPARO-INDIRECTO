namespace AmparoIndirectoAPI.Model.Entities
{
    public class AutoridadesResponsables
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; } se modifico
        public int id_autoridad_responsable { get; set; }
        public int id_consecutivo{ get; set; }
        public int id_recurrente{ get; set; }
        public bool activo{ get; set; }

    }
}
