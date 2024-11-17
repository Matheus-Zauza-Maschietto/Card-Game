using app.Context;
using app.Helper;
using app.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseDefaultConnection")));

builder.ConfigureDependencyInjections();

builder.ConfigureHttpClients();

builder.ConfigureIdentity();

builder.ConfigureAuthorization();

builder.ConfigureSwaggerUI();

builder.ConfigureRedisCache();

builder.ConfigureKafka();

builder.ConfigureBackgroundJobs();

builder.ConfigureValidators();

var app = builder.Build();

app.UseCors(cr =>
{
    cr.AllowAnyHeader();
    cr.AllowAnyMethod();
    cr.AllowAnyOrigin();
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.ConfigureInitialMigration();
app.Run();
