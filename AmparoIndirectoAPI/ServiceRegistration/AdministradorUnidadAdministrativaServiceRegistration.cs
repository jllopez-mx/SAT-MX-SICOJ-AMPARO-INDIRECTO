using AmparoIndirectoAPI.Model.DAO.Repository;
using AmparoIndirectoAPI.Model.DAO.ServicesDAO;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;

namespace AmparoIndirectoAPI.ServiceRegistration
{
    public static class AdministradorUnidadAdministrativaServiceRegistration
    {
        public static IServiceCollection AddAdministradorUnidadAdministrativaServices(this IServiceCollection services)
        {
            
           
            services.AddScoped<IAmparoIndirectoRepository, AmparoIndirectoRepository>();
            services.AddScoped<IAmparoIndirectoAdministradorUnidadAdministrativaService, AmparoIndirectoAdministradorUnidadAdministrativaService>();
            

            services.AddScoped<IAmparoIndirectoAdministradorUnidadAdministrativaRepository, AmparoIndirectoAdministradorUnidadAdministrativaRepository>();


            return services;
        }
    }
}
