using Scalable.Api;
using Scalable.Stock;
using Scalable.Core;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.RegisterCoreDbContexts(builder.Configuration);
builder.Services.RegisterStockModuleDbContexts(builder.Configuration);

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(ApiModule.GetAssemblies());
});

builder.Services.SetupIntegrationEvents();

builder.Services.RegisterAllModulesServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<StockContext>();
    db.Database.Migrate();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }