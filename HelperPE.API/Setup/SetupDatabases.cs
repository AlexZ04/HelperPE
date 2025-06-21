using HelperPE.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.API.Setup
{
    public class SetupDatabases
    {
        public static void AddDatabases(WebApplicationBuilder builder)
        {
            AddDb(builder);
            AddRedis(builder);
        }

        public static void AddDb(WebApplicationBuilder builder)
        {
            var applicationsConnection = builder.Configuration.GetConnectionString("DbConnection");

            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(applicationsConnection));
        }

        public static void AddRedis(WebApplicationBuilder builder)
        {
            builder.Services.AddStackExchangeRedisCache(options => {
                options.Configuration = "localhost:6379";
                options.InstanceName = "HelperPE";
            });
        }

        public static void RunMigrations(WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();

            var applicationContext = serviceScope.ServiceProvider.GetService<DataContext>();

            applicationContext?.Database.Migrate();
        }
    }
}
