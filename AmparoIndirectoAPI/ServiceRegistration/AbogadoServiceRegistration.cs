using AmparoIndirectoAPI.Model.DAO.Repository;
using AmparoIndirectoAPI.Model.DAO.ServicesDAO;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;
using Sicoj.Utils.Extentions;

namespace AmparoIndirectoAPI.ServiceRegistration
{
    public static class AbogadoServiceRegistration
    {
        public static IServiceCollection AddAbogadoServices(this IServiceCollection services)
        {
            
           
            services.AddScoped<IAmparoIndirectoRepository, AmparoIndirectoRepository>();
            services.AddScoped<IAmparoIndirectoAbogadoService, AmparoIndirectoAbogadoService>();
            //services.AddScoped<IArchivosAmparoIndirectoRepository, ArchivosAmparoIndirectoRepository>();
            services.AddScoped<ApiService>();
            services.AddScoped<IAmparoIndirectoAbogadoRepository, AmparoIndirectoAbogadoRepository>();

           
            

            return services;
        }
    }
}
