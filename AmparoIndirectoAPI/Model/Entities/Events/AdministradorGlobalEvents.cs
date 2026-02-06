namespace AmparoIndirectoAPI.Model.Entities.Events
{
    public class AdministradorGlobalEvents
    {
        public static AdministradorGlobalDescartar CreateDescartar(AmparoIndirecto entityAmparo, string? usuario)
        {
            AdministradorGlobalDescartar entity = new()
            {
                id_numero_asunto = entityAmparo.id,
                id_estado_procesal = entityAmparo.id_estado_procesal,
                fecha_creacion = DateTime.Now,
                fecha_modificacion = DateTime.Now,

            };
            return entity;
        }


    }
}
