using System.Text.Json.Serialization;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.MappingProfiles;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Context;
using ControleFinanceiro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // options.UseInMemoryDatabase("DataBaseControleFinaceiro");
});

builder.Services.AddScoped<IDespesaRepository, DespesaRepository>();
builder.Services.AddScoped<IDespesaService, DespesaService>();
builder.Services.AddScoped<IReceitaRepository, ReceitaRepository>();
builder.Services.AddScoped<IReceitaService, ReceitaService>();
builder.Services.AddScoped<IResumoService, ResumoService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }