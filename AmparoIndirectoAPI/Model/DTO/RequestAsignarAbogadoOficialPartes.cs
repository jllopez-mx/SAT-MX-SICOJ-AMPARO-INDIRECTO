namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestAsignarAbogadoOficialPartes
    {
        public int id { get; set; }
        public int id_administracion_central { get; set; }
        public int id_subadministracion { get; set; }
        public string id_abogado { get; set; } = null!;
    }
}
