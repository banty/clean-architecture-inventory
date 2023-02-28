using CleanArchitectureInventory.Receiving.API;
using CleanArchitectureInventory.Receiving.Applicaiton;
using CleanArchitectureInventory.Receiving.Infrastructure;
using CleanArchitectureInventory.Receiving.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddAPIService();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scop=app.Services.CreateScope())
    {
        var initaliser = scop.ServiceProvider.GetRequiredService<ApplicaitonDbContextInitializer>();
        await initaliser.InitializeAsync();
    }
}

app.UseHealthChecks("/health");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

