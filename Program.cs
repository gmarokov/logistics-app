using app.web.Data.Contracts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace app.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDefaultDbContextInitializer>();
                var env = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();
                
                // Apply any pending migrations
                dbInitializer.Migrate();
                if (env.IsDevelopment())
                {
                    // Seed the database in development mode
                    dbInitializer.Seed().GetAwaiter().GetResult();
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
