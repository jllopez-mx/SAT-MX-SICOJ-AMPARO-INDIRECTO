namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestSolicitudTransparenciaFilters
    {
        public List<string> idNumeroAsunto { get; set; } = new()!;
        //public List<string> idJuicioAmparo { get; set; } = new()!; se modifico

    }

    public class RequestAmparoIndirectoFilters
    {
        public List<string> ByFechaRecepcionInicial {  get; set; } = new()!;
        public List<string> ByFechaRecepcionFinal {  get; set; } = new()!;
        public List<string> ByFechaVencimientoDemanda {  get; set; } = new()!;
        public List<string> ByNumeroExpediente { get; set; } = new()!;
        public List<string> ByNumeroAsunto { get; set; } = new()!;
        //public List<string> ByJuicioAmparo { get; set; } = new()!; se modifico
        public List<string> ByIdJuzgado { get; set; } = new()!;
        public List<string> ByNombreQuejoso { get; set; } = new()!;
        public List<string> ByIdMateria { get; set; } = new()!;
        public List<string> ByIdSubmateria { get; set; } = new()!;
        public List<string> ByIdTipoActo { get; set; } = new()!;
        public List<string> ByDespacho { get; set; } = new()!;
        public List<string> ByIdAdministracion { get; set; } = new()!;
        public List<string> ByIdSubAdministracion { get; set; } = new()!;
        public List<string> ByRfcQuejoso { get; set; } = new()!;
        public List<string> ByTransparencia { get; set; } = new()!;
        public List<string> ByIdEstadoTarea { get; set; } = new()!;
        public List<string> ByIdEstadoProcesal { get; set; } = new()!;
    }
}
