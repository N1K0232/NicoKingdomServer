namespace NicoKingdomManager;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = CreateBuilder(args);
        Startup startup = new();
        startup.ConfigureServices(builder.Services, builder.Configuration, builder.Host);

        WebApplication app = builder.Build();
        startup.Configure(app);
        await app.RunAsync();
    }
    private static WebApplicationBuilder CreateBuilder(string[] args) => WebApplication.CreateBuilder(args);
}