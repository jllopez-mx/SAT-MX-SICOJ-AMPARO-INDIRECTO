namespace AmparoIndirectoAPI.Model.Entities
{
    public class AdministradorGlobalDescartar
    {
        public int id { get; set; }
        public int id_numero_asunto { get; set; }
        //public int id_juicio_amparo { get; set; }
        public int id_estado_procesal { get; set; }
        public DateTime fecha_creacion {  get; set; }
        public string? usuario { get; set; }
        public DateTime fecha_modificacion  { get; set; }
        public bool activo {  get; set; }
    }
}
