using AmparoIndirectoAPI.Model.DAO.Repository;
using AmparoIndirectoAPI.Model.DAO.ServicesDAO;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;

namespace AmparoIndirectoAPI.ServiceRegistration
{
    public static class AdministradorGlobalServiceRegistration
    {
        public static IServiceCollection AddAdministradorGlobalServices(this IServiceCollection services)
        {
            
           
            services.AddScoped<IAmparoIndirectoRepository, AmparoIndirectoRepository>();
            services.AddScoped<IAmparoIndirectoAdministradorGlobalService, AmparoIndirectoAdministradorGlobalService>();
            

            services.AddScoped<IAmparoIndirectoAdministradorGlobalRepository, AmparoIndirectoAdministradorGlobalRepository>();


            return services;
        }
    }
}
