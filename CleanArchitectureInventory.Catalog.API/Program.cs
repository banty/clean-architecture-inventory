using CleanArchitectureInventory.Catalog.API;
using CleanArchitectureInventory.Catalog.Application;
using CleanArchitectureInventory.Catalog.Infrastructure;
using CleanArchitectureInventory.Catalog.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddAPIService(builder.Configuration);
// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseMigrationsEndPoint();
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initialiser.InitializeAsyn();
        await initialiser.SeedAsync();
    }
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();

