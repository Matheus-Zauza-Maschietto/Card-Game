using System.Text;
using app.Context;
using app.Helper;
using app.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.ConfigureDependencyInjections();

builder.ConfigureHttpClients();

builder.ConfigureIdentity();

builder.ConfigureAuthorization();

builder.ConfigureSwaggerUI();

var app = builder.Build();

app.UseCors(cr =>
{
    cr.AllowAnyHeader();
    cr.AllowAnyMethod();
    cr.AllowAnyOrigin();
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
