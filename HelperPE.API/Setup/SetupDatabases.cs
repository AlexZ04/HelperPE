using HelperPE.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.API.Setup
{
    public class SetupDatabases
    {
        public static void AddDatabases(WebApplicationBuilder builder)
        {
            AddDb(builder);
        }

        public static void AddDb(WebApplicationBuilder builder)
        {
            var applicationsConnection = builder.Configuration.GetConnectionString("DbConnection");

            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(applicationsConnection));
        }

        public static void RunMigrations(WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();

            var applicationContext = serviceScope.ServiceProvider.GetService<DataContext>();

            applicationContext?.Database.Migrate();
        }
    }
}
