using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json; // Certifique-se de usar o namespace correto
using Xunit;

namespace SalesServiceAPI.Tests.IntegrationTestst.Controllers;

public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllVendas_Should_Return_Ok()
    {
        // Act
        var response = await _client.GetAsync("/api/venda");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetVendaById_Should_Return_Ok_When_Valid_Id()
    {
        // Arrange
        var vendaId = 1; // Assumindo que um venda com Id=1 exista para este teste

        // Act
        var response = await _client.GetAsync($"/api/venda/{vendaId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetVendaById_Should_Return_NotFound_When_Invalid_Id()
    {
        // Arrange
        var vendaId = 999; // Id inválido

        // Act
        var response = await _client.GetAsync($"/api/venda/{vendaId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateVenda_Should_Return_Created()
    {
        // Arrange
        var newVenda = new
        {
            Data = "2025-01-06T00:00:00",
            ClienteId = 1,
            FilialId = 1,
            Itens = new[]
            {
                new { ProdutoId = 1, Quantidade = 5, ValorUnitario = 20.0m, Desconto = 0m }
            },
            Cancelado = false
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/venda", newVenda);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task UpdateVenda_Should_Return_Ok_When_Valid_Id()
    {
        // Arrange
        var vendaId = 1; // Assumindo que um venda com Id=1 exista para este teste
        var updatedVenda = new
        {
            Id = vendaId,
            Data = "2025-01-06T00:00:00",
            ClienteId = 1,
            FilialId = 1,
            Itens = new[]
            {
                new { ProdutoId = 1, Quantidade = 10, ValorUnitario = 20.0m, Desconto = 2m }
            },
            Cancelado = false
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/venda/{vendaId}", updatedVenda);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateVenda_Should_Return_NotFound_When_Invalid_Id()
    {
        // Arrange
        var vendaId = 999; // Id inválido
        var updatedVenda = new
        {
            Id = vendaId,
            Data = "2025-01-06T00:00:00",
            ClienteId = 1,
            FilialId = 1,
            Itens = new[]
            {
                new { ProdutoId = 1, Quantidade = 10, ValorUnitario = 20.0m, Desconto = 2m }
            },
            Cancelado = false
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/venda/{vendaId}", updatedVenda);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteVenda_Should_Return_NoContent_When_Valid_Id()
    {
        // Arrange
        var vendaId = 1; // Assumindo que um venda com Id=1 exista para este teste

        // Act
        var response = await _client.DeleteAsync($"/api/venda/{vendaId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteVenda_Should_Return_NotFound_When_Invalid_Id()
    {
        // Arrange
        var vendaId = 999; // Id inválido

        // Act
        var response = await _client.DeleteAsync($"/api/venda/{vendaId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}