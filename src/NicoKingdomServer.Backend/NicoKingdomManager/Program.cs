using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using NicoKingdomManager.BusinessLayer.MapperProfiles;
using NicoKingdomManager.BusinessLayer.Services;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.BusinessLayer.Validators;
using NicoKingdomManager.DataAccessLayer;
using Serilog;
using System.Text.Json.Serialization;
using TinyHelpers.Json.Serialization;

namespace NicoKingdomManager;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        ConfigureSerilog(builder.Host);
        ConfigureServices(builder.Services, builder.Configuration);

        WebApplication app = builder.Build();
        Configure(app);
        await app.RunAsync();
    }
    private static void ConfigureSerilog(IHostBuilder host)
    {
        host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        });
    }
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddProblemDetails();
        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
        });
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
        services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<SaveUserValidator>();
        });
        services.AddSwaggerGen()
        .AddFluentValidationRulesToSwagger(options =>
        {
            options.SetNotNullableIfMinLengthGreaterThenZero = true;
        });

        string connectionString = configuration.GetConnectionString("SqlConnection");
        services.AddSqlServer<DataContext>(connectionString);
        services.AddScoped<IDataContext>(sp => sp.GetRequiredService<DataContext>());

        services.AddScoped<IUsersService, UsersService>();
    }
    private static void Configure(IApplicationBuilder app)
    {
        app.UseProblemDetails();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}