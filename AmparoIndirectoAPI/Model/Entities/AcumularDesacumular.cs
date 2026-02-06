namespace AmparoIndirectoAPI.Model.Entities
{
    public class AcumularDesacumular
    {
        public int id_juicio_padre { get; set; }
        public List<int> id_juicio_acumulado { get; set; } = new List<int>();
        public string numero_oficio { get; set; } = null!;
        public int id_administracion { get; set; }
        public string usuario { get; set; } = null!;

    }
}
