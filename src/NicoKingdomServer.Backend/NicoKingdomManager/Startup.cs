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

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        });

        services.AddProblemDetails();
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<IServerLogsService, ServerLogsService>();
    }
    public void Configure(IApplicationBuilder app)
    {
        app.UseProblemDetails();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}