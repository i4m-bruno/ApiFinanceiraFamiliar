using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ControleFinanceiro.IntegrationTests;

public abstract class IntegrationTest : IClassFixture<WebApplicationFactoryFixture>
{
    protected readonly WebApplicationFactory<Program> Factory;

    protected IntegrationTest(WebApplicationFactoryFixture fixture)
    {
        Factory = fixture.Factory;
    }
}
