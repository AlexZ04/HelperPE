using HelperPE.Persistence.Repositories;
using HelperPE.Persistence.Repositories.Implementations;

namespace HelperPE.API.Setup
{
    public class SetupRepositories
    {
        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IEventRepository, EventRepositoryImpl>();
            services.AddScoped<IPairRepository, PairRepositoryImpl>();
            services.AddScoped<IFacultyRepository, FacultyRepositoryImpl>();
        }
    }
}
