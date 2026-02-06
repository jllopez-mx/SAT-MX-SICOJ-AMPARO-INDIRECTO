using AmparoIndirectoAPI.Model.DAO.Repository;
using AmparoIndirectoAPI.Model.DAO.ServicesDAO;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;

namespace AmparoIndirectoAPI.ServiceRegistration
{
    public static class OficialPartesServiceRegistration
    {
        public static IServiceCollection AddOficialPartesServices(
            this IServiceCollection services
        )
        {
            
           
            services.AddScoped<IAmparoIndirectoRepository, AmparoIndirectoRepository>();
            services.AddScoped<IAmparoIndirectoOficialPartesService, AmparoIndirectoOficialPartesService>();
            services.AddScoped<IArchivosAmparoIndirectoRepository, ArchivosAmparoIndirectoRepository>();

            services.AddScoped<IAmparoIndirectoOficialPartesRepository, AmparoIndirectoOficialPartesRepository>();

            return services;
        }
    }
}
