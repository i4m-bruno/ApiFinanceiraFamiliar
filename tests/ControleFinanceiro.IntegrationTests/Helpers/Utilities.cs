using System;
using System.Linq;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFinanceiro.IntegrationTests.Helpers;

public static class Utilities
{
    public static WebApplicationFactory<Program> BuildApplicationFactory(this WebApplicationFactory<Program> factory)
    {
        var connectionString = $"Data Source={Guid.NewGuid()}.db";
        return factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite(connectionString);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                InitializeDbForTests(context);
            });
        });
    }

    public static void InitializeDbForTests(ApplicationDbContext db)
    {
        db.Receitas.RemoveRange(db.Receitas);
        db.Despesas.RemoveRange(db.Despesas);

        db.Receitas.AddRange(GetSeedingReceitas());
        db.Despesas.AddRange(GetSeedingDespesas());
        db.SaveChanges();
    }

    public static Receita[] GetSeedingReceitas()
    {
        return new Receita[]
        {
                new()
                {
                    Id = 1,
                    Descricao = "salário",
                    Data = DateTime.Now,
                    Valor = 3000
                },
                new()
                {
                    Id = 2,
                    Descricao = "freelancer",
                    Data = DateTime.Now,
                    Valor = 1000
                }
        };
    }

    public static Despesa[] GetSeedingDespesas()
    {
        return new Despesa[]
        {
                new()
                {
                    Id = 1,
                    Descricao = "Alimentacao",
                    Categoria = Domain.Entities.Enums.CategoriaType.Alimentacao,
                    Data = DateTime.Now,
                    Valor = 300
                },
                new()
                {
                    Id = 2,
                    Descricao = "Educacao",
                    Categoria = Domain.Entities.Enums.CategoriaType.Educacao,
                    Data = DateTime.Now,
                    Valor = 100
                },
                new()
                {
                    Id = 3,
                    Descricao = "Imprevistos",
                    Categoria = Domain.Entities.Enums.CategoriaType.Imprevistos,
                    Data = DateTime.Now,
                    Valor = 100
                },
                new()
                {
                    Id = 4,
                    Descricao = "Lazer",
                    Categoria = Domain.Entities.Enums.CategoriaType.Lazer,
                    Data = DateTime.Now,
                    Valor = 100
                },
                new()
                {
                    Id = 5,
                    Descricao = "Moradia",
                    Categoria = Domain.Entities.Enums.CategoriaType.Moradia,
                    Data = DateTime.Now,
                    Valor = 500
                },
                new()
                {
                    Id = 6,
                    Descricao = "Outras",
                    Categoria = Domain.Entities.Enums.CategoriaType.Outras,
                    Data = DateTime.Now,
                    Valor = 100
                },
                new()
                {
                    Id = 7,
                    Descricao = "Saude",
                    Categoria = Domain.Entities.Enums.CategoriaType.Saude,
                    Data = DateTime.Now,
                    Valor = 100
                },
                new()
                {
                    Id = 8,
                    Descricao = "Transporte",
                    Categoria = Domain.Entities.Enums.CategoriaType.Transporte,
                    Data = DateTime.Now,
                    Valor = 100
                }

        };
    }

    public static WebApplicationFactory<Program> RebuildDb(this WebApplicationFactory<Program> factory)
    {
        return factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices
                    .GetRequiredService<ApplicationDbContext>();
                ReinitializeDbForTests(db);
            });
        });
    }
    public static void ReinitializeDbForTests(ApplicationDbContext db)
    {
        db.Receitas.RemoveRange(db.Receitas.ToList());
        db.Despesas.RemoveRange(db.Despesas.ToList());
        InitializeDbForTests(db);
    }

}
