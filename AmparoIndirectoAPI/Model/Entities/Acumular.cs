using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.Entities
{
    public class Acumular
    {
        public int id { set; get; }

        public int administracion { set; get; }

        public string numero_asunto { get; set; } = null!;
        //public string juicio_amparo { get; set; } = null!; se modifico
        
        public string numero_expediente { get; set; } = null!;

        public int juzgado { get; set; }

        public string rfc_quejoso { get; set; } = null!;


        public string nombre_quejoso { get; set; } = null!;
    }
}
