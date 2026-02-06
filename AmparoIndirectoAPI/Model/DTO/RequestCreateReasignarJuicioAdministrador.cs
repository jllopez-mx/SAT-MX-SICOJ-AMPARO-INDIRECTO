namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateReasignarJuicioAdministrador
    {
        public List<int> ids { get; set; } = new List<int>();
        public string motivo_reasignacion { get; set; } = null!;
        public string id_abogado { get; set; } = null!;
    }
}
