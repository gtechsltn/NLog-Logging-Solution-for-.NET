
using NLog;
using NLog.Web;
using NLogWithDapperAPI.Data;
using NLogWithDapperAPI.Services;

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
try
{
    logger.Debug("Starting up the application...");

    var builder = WebApplication.CreateBuilder(args);


    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddScoped<DapperContext>();
    builder.Services.AddScoped<UserRepository>();
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    // Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);  // Set minimum log level
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // Catch setup errors
    logger.Error(ex, "An error occurred during application startup");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();  // Ensure to flush and stop internal timers/threads before application-exit
}