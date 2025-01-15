using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SalesServiceAPI.Application.DTOs;
using SalesServiceAPI.Tests.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace SalesServiceAPI.Tests.IntegrationTestst.Controllers;

[TestCaseOrderer(TestOrderer.TypeName, TestOrderer.AssemblyName)]
public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private static int _createdVendaId;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact, TestOrder(1)]
    public async Task CreateVenda_Should_Return_Created()
    {
        // Arrange
        var newVenda = new
        {
            Data = "2025-01-06T00:00:00",
            ClienteId = 1,
            ClienteNome = "João Silva", 
            FilialId = 1,
            ClienteEmail = "joao.silva@teste.com",
            FilialNome = "Filial Central", 
            FilialEndereco = "Rua A, nº 123, Centro, São Paulo - SP, CEP 12345-123",
            Itens = new[]
            {
                new { ProdutoId = 1, ProdutoNome = "Produto A", Quantidade = 5, ValorUnitario = 20.0m, Desconto = 0m } 
            },
            Cancelado = false
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/venda", newVenda);


        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Recupera o ID da venda criada
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdVenda = JsonSerializer.Deserialize<VendaDTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        _createdVendaId = createdVenda.Id;

        // Confirma que o ID foi recuperado
        _createdVendaId.Should().BeGreaterThan(0);

    }

    [Fact, TestOrder(2)]
    public async Task GetAllVendas_Should_Return_Ok()
    {
        // Act
        var response = await _client.GetAsync("/api/Venda");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, TestOrder(3)]
    public async Task GetVendaById_Should_Return_Ok_When_Valid_Id()
    {
        // Arrange
        var vendaId = _createdVendaId;

        // Act
        var response = await _client.GetAsync($"/api/venda/{vendaId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, TestOrder(4)]
    public async Task GetVendaById_Should_Return_NotFound_When_Invalid_Id()
    {
        // Arrange
        var vendaId = 999; // Id inválido

        // Act
        var response = await _client.GetAsync($"/api/venda/{vendaId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact, TestOrder(5)]
    public async Task UpdateVenda_Should_Return_Ok_When_Valid_Id()
    {
        // Arrange

        var updatedVenda = new
        {
            Id = _createdVendaId,
            Data = "2025-01-06T00:00:00",
            ClienteId = 1,
            ClienteNome = "João Silva",
            ClienteEmail= "joao.silva@teste.com",
            FilialId = 1,
            FilialNome = "Filial Central",
            FilialEndereco = "Rua A, nº 123, Centro, São Paulo - SP, CEP 12345-123",
            Itens = new[]
            {
                new { ProdutoId = 1, ProdutoNome = "Produto A", Quantidade = 10, ValorUnitario = 20.0m, Desconto = 2m } 
            },
            Cancelado = false
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/venda/{_createdVendaId}", updatedVenda);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, TestOrder(6)]
    public async Task UpdateVenda_Should_Return_NotFound_When_Invalid_Id()
    {
        // Arrange
        var vendaId = 999; // Id inválido
        var updatedVenda = new
        {
            Id = vendaId,
            Data = "2025-01-06T00:00:00",
            ClienteId = 1,
            ClienteNome = "João Silva", 
            ClienteEmail = "joao.silva@teste.com",
            FilialId = 1,
            FilialNome = "Filial Central",
            FilialEndereco = "Rua A, nº 123, Centro, São Paulo - SP, CEP 12345-123",
            Itens = new[]
            {
                new { ProdutoId = 1, ProdutoNome = "Produto A", Quantidade = 10, ValorUnitario = 20.0m, Desconto = 2m }
            },
            Cancelado = false
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/venda/{vendaId}", updatedVenda);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact, TestOrder(7)]
    public async Task DeleteVenda_Should_Return_NoContent_When_Valid_Id()
    {
        // Arrange
        var vendaId = _createdVendaId; 

        // Act
        var response = await _client.DeleteAsync($"/api/venda/{vendaId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact, TestOrder(8)]
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
