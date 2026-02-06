using Sicoj.Utils.ViewModels;


namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseAcumularByFilters
    {
        public int id { set; get; }

        public int id_administracion { set; get; }
        public string? administracion { set; get; } = null!;

        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifcio

        public string numero_expediente { get; set; } = null!;

        public int id_juzgado { get; set; } = new();
        public string? juzgado { get; set; } = null!;

        public string rfc_quejoso { get; set; } = null!;


        public string nombre_quejoso { get; set; } = null!;

    }
}
