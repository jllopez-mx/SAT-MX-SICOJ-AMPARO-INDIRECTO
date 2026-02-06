namespace AmparoIndirectoAPI.Model.DTO
{
    public class ResponseFechaVencimiento
    {
    
    //    public List<ResultItem> Result { get; set; } = new();
    //    public bool Success { get; set; }
    //    public List<string> Messages { get; set; } = new();
    //}

    //public class ResultItem
    //{
        public int IdModulo { get; set; }
        public int IdSeccion { get; set; }
        public int IdAsunto { get; set; }
        //public DateTime FechaVencimiento { get; set; }
        public string FechaVencimiento { get; set; } = string.Empty;
        public int? IdSeccionRenglon { get; set; }   // null → nullable
        public int MesesCalendario { get; set; }
    }
}
