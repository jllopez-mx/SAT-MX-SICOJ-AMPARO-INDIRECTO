namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateAutoridadResponsableAbogado
    {
        public int id { get; set; }
        public List<int> ids_autoridades_responsables { get; set; } = new List<int>();

    }
}
