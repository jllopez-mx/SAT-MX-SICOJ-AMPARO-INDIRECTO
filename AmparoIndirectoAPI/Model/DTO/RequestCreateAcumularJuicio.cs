namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateAcumularJuicio
    {
        public int id_juicio_padre { get; set; }
        public List<int> id_juicio_acumulado { get; set; } = new List<int>();
        public string numero_oficio { get; set; } = null!;
        public IFormFile? documento { get; set; }
        public int id_tipo_documento { get; set; }
        public int id_seccion { get; set; }

    }
}
