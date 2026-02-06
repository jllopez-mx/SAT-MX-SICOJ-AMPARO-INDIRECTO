namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateNotaLitigio
    {
        public int idNumeroAsunto { get; set; }
        //public int idJuicioAmparo { get; set; } se modifico
        public string fechaRegistroNota { get; set; } = null!;
        public IFormFile? documento { get; set; }
        public int idTipoDocumento { get; set; }
        public int idSeccion { get; set; }
        public string usuario { get; set; } = null!;

    }
}
