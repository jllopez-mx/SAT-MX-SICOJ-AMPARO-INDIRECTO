namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestDeleteDocumento
    {
        public int IdAmparo{ get; set; }
        public List<int> Ids { get; set; } = new();
    }
}
