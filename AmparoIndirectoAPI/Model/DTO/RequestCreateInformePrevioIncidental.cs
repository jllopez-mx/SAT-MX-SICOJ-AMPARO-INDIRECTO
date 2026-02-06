namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateInformePrevioIncidental
    {
        public int id { get; set; }
        public int idAutoridadResponsable { get; set; }
        public string fechaAperturaIncidente { get; set; } = null!;
        //public string fecha_vencimiento { get; set;} = null!;
        public string numeroOficioInformePrevio { get; set; } = null!;
        public string fechaPresentacionInformPrevio { get; set; } = null!;
        public IFormFile? documento { get; set; }
        public int? idTipoArchivo { get; set; }
        public int? idSeccion { get; set; }

    }
}
