using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Sol_AutoServiceRegistration;
using Sol_AutoServiceRegistration.CustomHealthChecks;
using Sol_AutoServiceRegistration.Implementation;
using Sol_AutoServiceRegistration.Interfaces;
using System.Reflection;

// creating serilog instance
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("starting the server");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    #region Auto Service Registration

    //------------ Approach-1 ------------//
    // Using Convention-Based Registration, Establish clear naming conventions (Not-recommended, error prune)
    //builder.Services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());

    //------------ Approach-2 ------------//
    // Using attribute based registration
    //builder.Services.AddAttributeBasedServicesFromAssembly(Assembly.GetExecutingAssembly());


    #endregion

    #region Health Checks Configuration

    //builder.Services.ConfigureHealthChecks(builder.Configuration);

    #endregion

    #region Serilog Configuration

    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.WriteTo.Console();  // specify where to log the message, i.e. console, file, cloudwatch
        loggerConfiguration.ReadFrom.Configuration(context.Configuration); // specify from where to read the serilog configuration, i.e. appsettings.json
    });

    builder.Services.AddTransient<IDemo, Demo>();

    #endregion


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    #region Health Checks Middleware
    //app.UseHealthChecks("/api/health");

    //app.MapHealthChecks("/api/health", new HealthCheckOptions()
    //{
    //    Predicate = _ => true,
    //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    //});
    //app.UseHealthChecksUI(delegate (Options options)
    //{
    //    options.UIPath = "/healthcheck-ui";
    //    // options.AddCustomStylesheet("./HealthCheck/Custom.css");

    //});

    #endregion

    #region Serilog Configuration

    app.MapGet("/serilog/demo", (IDemo demo) =>
    {
        demo.Display();
    });

    app.UseSerilogRequestLogging();

    #endregion

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}

finally
{
    Log.CloseAndFlush();
}
