using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Despesa;
using ControleFinanceiro.Domain.Entities.Enums;
using ControleFinanceiro.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace ControleFinanceiro.IntegrationTests;

public class DespesasControllerTests : IntegrationTest
{
    public DespesasControllerTests(WebApplicationFactoryFixture fixture) : base(fixture)
    {
    }


    [Fact]
    public async Task GetAllDespesaAsync_ReturnsOk()
    {
        //Arrange
        var client = Factory.RebuildDb().CreateClient();

        //Act 
        var response = await client.GetAsync("api/Despesas");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<ResponseDto<IEnumerable<DespesaDto>>>();
        json.Should().NotBeNull();
        json.Success.Should().BeTrue();
        json.Data.Should().OnlyHaveUniqueItems();
        json.Data.Should().HaveCount(8);
        json.Erros.Should().HaveCount(0);
    }

    [Fact]
    public async Task GetAllDespesaAsync_AllDespesasWithDescricaoFilter_ReturnsOk()
    {
        var client = Factory.RebuildDb().CreateClient();

        var response = await client.GetAsync("api/Despesas?descricao=Saude");
        var json = await response.DeserializeContent<ResponseDto<IEnumerable<DespesaDto>>>();
        json.Should().NotBeNull();
        json.Success.Should().BeTrue();
        json.Data.Should().OnlyHaveUniqueItems();
        json.Data.Should().HaveCount(1);
        json.Erros.Should().HaveCount(0);
    }


    [Fact]
    public async Task GetAllDespesaAsync_NonExistiongDespesasWithDescricaoFilter_ReturnsOk()
    {
        var client = Factory.RebuildDb().CreateClient();

        var response = await client.GetAsync("api/Despesas?descricao=teste");

        var json = await response.DeserializeContent<ResponseDto<IEnumerable<DespesaDto>>>();
        json.Should().NotBeNull();
        json.Success.Should().BeTrue();
        json.Data.Should().BeEmpty();
        json.Data.Should().HaveCount(0);
        json.Erros.Should().HaveCount(0);
    }

    [Fact]
    public async Task GetDespesaByIdAsync_ExistingHero_ReturnsOk()
    {
        var client = Factory.RebuildDb().CreateClient();

        var response = await client.GetAsync("api/Despesas/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<DespesaDto>();
        json.Should().NotBeNull();
        json.Id.Should().Be(1);
        json.Descricao.Should().NotBeNullOrWhiteSpace();
        json.Categoria.Should().NotBeNull();
    }

    [Fact]
    public async Task GetDespesaByIdAsync_ExistingHero_ReturnsNotFound()
    {
        var client = Factory.RebuildDb().CreateClient();

        var response = await client.GetAsync("api/Despesas/100");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // [Fact]
    // public async Task CreateDespesaAsync_ValidDespesa_ReturnsOk()
    // {
    //     var client = Factory.RebuildDb().CreateClient();

    //     var despesa = new DespesaDto
    //     {
    //         Descricao = "Despesa de teste",
    //         Categoria = CategoriaType.Outras,
    //         Data = DateTime.Now
    //     };

    //     var response = await client.PostAsync("api/Despesas", despesa.GetStringContent());

    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    //     var json = await  response.DeserializeContent<ResponseDto<DespesaDto>>();
    //     json.Should().NotBeNull();
    //     json.
    //     json.Id.Should().BeGreaterThan(0);
    //     json.Descricao.Should().NotBeNullOrWhiteSpace();
    //     json.Categoria.Should().NotBeNull();            
    // }

}
