using System;
using ControleFinanceiro.Infrastructure.Context;
using ControleFinanceiro.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFinanceiro.IntegrationTests;

public class WebApplicationFactoryFixture : IDisposable
{
    public readonly WebApplicationFactory<Program> Factory;

    public WebApplicationFactoryFixture()
    {
        Factory = new WebApplicationFactory<Program>().BuildApplicationFactory();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            using var _scope = (Factory.Services.GetRequiredService<IServiceScopeFactory>()).CreateScope();
            using var _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _context?.Database.EnsureDeleted();
            Factory.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
