namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateInformacionAdicional
    {
        public int id {  get; set; }
        public string fecha_resolucion_oficio { get; set; } = null!;
        public decimal cuantia {  get; set; }
        public string oficio_resolucion_reclamado { get; set; } = null!;
        public int id_autoridad_emisora_resolucion_oficio {  get; set; }
        public List<int> concepto_violacion { get; set; } = new List<int>();

        //public string concepto_violacion { get; set; } = null!;
    }
}
