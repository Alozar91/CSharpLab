using Lab3._2.Services;

namespace Lab3._2
{
    public class Program
    {
        internal static IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSqlServer<ApplicationContext>(
                config.GetConnectionString("DefaultConnection"));

            builder.Services.AddGrpc();

            var app = builder.Build();

            app.MapGrpcService<PersonApiService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}