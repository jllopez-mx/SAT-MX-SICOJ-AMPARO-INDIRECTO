using AmparoIndirectoAPI.Model.DAO.Repository;
using AmparoIndirectoAPI.Model.DAO.ServicesDAO;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;

namespace AmparoIndirectoAPI.ServiceRegistration
{
    public static class AdministradorServiceRegistration
    {
        public static IServiceCollection AddAdministradorServices(this IServiceCollection services)
        {
            
           
            services.AddScoped<IAmparoIndirectoRepository, AmparoIndirectoRepository>();
            services.AddScoped<IAmparoIndirectoAdministradorService, AmparoIndirectoAdministradorService>();
            //services.AddScoped<IArchivosAmparoIndirectoRepository, ArchivosAmparoIndirectoRepository>();

            services.AddScoped<IAmparoIndirectoAdministradorRepository, AmparoIndirectoAdministradorRepository>();

           
            

            return services;
        }
    }
}
