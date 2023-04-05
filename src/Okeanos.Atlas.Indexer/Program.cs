using Okeanos.Atlas.Indexer.Extensions;

namespace Okeanos.Atlas.Indexer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureAppConfiguration(config =>
            {
                config.AddBlockcore("Atlas Indexer", args);
            });

            Startup.AddIndexerServices(builder.Services, builder.Configuration);
        
            var app = builder.Build();

            Startup.Configure(app, app.Environment);
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}