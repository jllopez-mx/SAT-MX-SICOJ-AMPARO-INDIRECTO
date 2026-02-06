namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateAsuntoControlDocumental
    {
        public string FechaRecepcionDemanda { get; set; } = null!;
        public string NumeroAsunto { get; set; } = null!;
        //public string JuicioAmparo { get; set; } = null!; se modifico
        public int Juzgado { get; set; }
        public string NombreQuejoso { get; set; } = null!;
        public string RfcQuejoso { get; set; } = null!;
        public string RfcRecurrente{ get; set; } = null!;
        public int IdEstadoTarea { get; set; }
        public int IdUnidadAdministrativa { get; set; }
    }
}
