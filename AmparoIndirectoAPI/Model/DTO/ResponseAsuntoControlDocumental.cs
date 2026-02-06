namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseAsuntoControlDocumental
    {
        public int id_asunto{ get; set; } = new();
        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico
        public int id_administracion { get; set; } = new();
        public string? administracion { get; set; } = null!;
        
        //public bool activo { get; set; }
        //public string? numero_empleado { get; set; } = null!;
        //public string? fecha_creacion { get; set; } = null!;
        //public string? fecha_modificacion { get; set; } = null!;
    }
}
