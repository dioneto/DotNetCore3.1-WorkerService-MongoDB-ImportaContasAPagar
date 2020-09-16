using ImportadorContasAPagar.FileProcessor;
using ImportadorContasAPagar.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImportadorContasAPagar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<FileImport>();
                    services.AddSingleton<IConnection, MongoConnection>();
                    services.AddSingleton<ContasAPagarRepository>();
                });
    }
}
