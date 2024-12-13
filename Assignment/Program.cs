using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using CCMPreparation.Middleware;
using EmployeeManagement.Data;
using EmployeeManagement.Service;
using Serilog;

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/applog.txt", rollingInterval: RollingInterval.Hour, retainedFileCountLimit: 5)
    .WriteTo.Seq(" http://localhost:5341")
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    //Add support to logging with SERILOG
    builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));


    Log.Information("Application Setup Started....");

    // Add services to the container. 
    builder.Services.AddHttpClient();

    builder.Services.AddHttpClient<ExternalApiService>(client =>
    {
        client.BaseAddress = new Uri("https://fakestoreapi.com/products/");
        client.DefaultRequestHeaders.Add("Accept", "application/json");

    });

    builder.Services.AddHttpClient<IProductService, ProductService>(client =>
    {
        client.BaseAddress = new Uri("https://fakestoreapi.com/products/category/");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IDbOrderService, DbOrderService>();
    builder.Services.AddScoped<IDbPurchaseService, DbPurchaseService>();
    builder.Services.AddScoped<IDbInquiryService, DbInquiryService>();
    builder.Services.AddScoped<IDbSupportTicketService, DbSupportTicketService>();
    builder.Services.AddScoped<IDbCustomerService, DbCustomerService>();
    builder.Services.AddScoped<IDbSaleService, DbSaleService>(); 

    builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    builder.Services.AddHttpClient(); // Register HttpClient

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    Log.Logger.Information("AppBuilder Builds.");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(setUpAction => { setUpAction.DocExpansion(docExpansion: Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); });
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(options
  => options.EnrichDiagnosticContext = RequestEnricher.LogAdditionalInfo);

    app.UseMiddleware<LoggingMiddleware>();


    app.UseAuthorization();


    app.MapControllers();

    app.Run();

    Log.Information("Application Running...");

}
catch (Exception ex)
{
    Log.Error(ex, "The exception was thrown during application startup");
}
finally
{
    Log.CloseAndFlush();
}

internal class RequestEnricher
{
    internal static void LogAdditionalInfo(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
    }
}