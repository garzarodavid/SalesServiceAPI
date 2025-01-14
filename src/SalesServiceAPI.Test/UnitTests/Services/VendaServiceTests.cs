using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SalesServiceAPI.Application.DTOs;
using SalesServiceAPI.Application.Interfaces.Services;
using SalesServiceAPI.Application.Services;
using SalesServiceAPI.Domain.Entities;
using SalesServiceAPI.Domain.Repositories;
using Serilog;
using Xunit;

namespace SalesServiceAPI.Tests.UnitTests.Services;
public class VendaServiceTests
{
    private readonly IVendaService _vendaService;
    private readonly IVendaRepository _vendaRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IFilialRepository _filialRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public VendaServiceTest()
    {
        _vendaRepository = Substitute.For<IVendaRepository>();
        _produtoRepository = Substitute.For<IProdutoRepository>();
        _clienteRepository = Substitute.For<IClienteRepository>();
        _filialRepository = Substitute.For<IFilialRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger>();

        _vendaService = new VendaService(_vendaRepository, _produtoRepository, _clienteRepository, _filialRepository, _mapper, _logger);
    }

    [Fact]
    public async Task CreateVendaAsync_Should_Return_Created_Venda()
    {
        // Arrange
        var vendaDto = new VendaDTO
        {
            ClienteId = 1,
            FilialId = 1,
            Itens = new List<ItemVendaDTO>
            {
                new ItemVendaDTO { ProdutoId = 1, Quantidade = 5, ValorUnitario = 20.0m, Desconto = 0m }
            }
        };

        var venda = new Venda
        {
            Id = 1,
            ClienteId = 1,
            FilialId = 1,
            Itens = new List<ItemVenda>
            {
                new ItemVenda { ProdutoId = 1, Quantidade = 5, ValorUnitario = 20.0m, Desconto = 0m }
            }
        };

        _mapper.Map<Venda>(vendaDto).Returns(venda);
        _mapper.Map<VendaDTO>(venda).Returns(vendaDto);

        // Act
        var result = await _vendaService.CreateVendaAsync(vendaDto);

        // Assert
        result.Should().NotBeNull();
        result.Itens.Should().HaveCount(1);
        result.Itens.FirstOrDefault()?.Desconto.Should().Be(2.0m); // 10% de desconto aplicado
    }

    [Fact]
    public async Task UpdateVendaAsync_Should_Return_Updated_Venda()
    {
        // Arrange
        var vendaDto = new VendaDTO
        {
            Id = 1,
            ClienteId = 1,
            FilialId = 1,
            Itens = new List<ItemVendaDTO>
            {
                new ItemVendaDTO { ProdutoId = 1, Quantidade = 10, ValorUnitario = 20.0m, Desconto = 0m }
            }
        };

        var venda = new Venda
        {
            Id = 1,
            ClienteId = 1,
            FilialId = 1,
            Itens = new List<ItemVenda>
            {
                new ItemVenda { ProdutoId = 1, Quantidade = 10, ValorUnitario = 20.0m, Desconto = 0m }
            }
        };

        _vendaRepository.GetByIdAsync(1).Returns(venda);
        _mapper.Map<Venda>(vendaDto).Returns(venda);
        _mapper.Map<VendaDTO>(venda).Returns(vendaDto);

        // Act
        var result = await _vendaService.UpdateVendaAsync(1, vendaDto);

        // Assert
        result.Should().NotBeNull();
        result.Itens.Should().HaveCount(1);
        result.Itens.FirstOrDefault()?.Desconto.Should().Be(4.0m); // 20% de desconto aplicado
    }

    [Fact]
    public async Task UpdateVendaAsync_Should_Throw_Exception_When_Venda_Not_Found()
    {
        // Arrange
        var vendaDto = new VendaDTO { Id = 1 };
        _vendaRepository.GetByIdAsync(1).Returns((Venda)null);

        // Act
        Func<Task> action = async () => await _vendaService.UpdateVendaAsync(1, vendaDto);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Venda não encontrada.");
    }

    [Fact]
    public async Task GetVendaByIdAsync_Should_Return_Venda()
    {
        // Arrange
        var venda = new Venda
        {
            Id = 1,
            ClienteId = 1,
            FilialId = 1,
            Itens = new List<ItemVenda>
            {
                new ItemVenda { ProdutoId = 1, Quantidade = 5, ValorUnitario = 20.0m, Desconto = 2.0m }
            }
        };

        _vendaRepository.GetByIdAsync(1).Returns(venda);
        _mapper.Map<VendaDTO>(venda).Returns(new VendaDTO());

        // Act
        var result = await _vendaService.GetVendaByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetVendaByIdAsync_Should_Throw_Exception_When_Venda_Not_Found()
    {
        // Arrange
        _vendaRepository.GetByIdAsync(1).Returns((Venda)null);

        // Act
        Func<Task> action = async () => await _vendaService.GetVendaByIdAsync(1);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Venda não encontrada.");
    }

    [Fact]
    public async Task DeleteVendaAsync_Should_Return_True_When_Venda_Deleted()
    {
        // Arrange
        var venda = new Venda
        {
            Id = 1,
            ClienteId = 1,
            FilialId = 1,
            Itens = new List<ItemVenda>
            {
                new ItemVenda { ProdutoId = 1, Quantidade = 5, ValorUnitario = 20.0m, Desconto = 2.0m }
            }
        };

        _vendaRepository.GetByIdAsync(1).Returns(venda);

        // Act
        var result = await _vendaService.DeleteVendaAsync(1);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteVendaAsync_Should_Throw_Exception_When_Venda_Not_Found()
    {
        // Arrange
        _vendaRepository.GetByIdAsync(1).Returns((Venda)null);

        // Act
        Func<Task> action = async () => await _vendaService.DeleteVendaAsync(1);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Venda não encontrada.");
    }

    [Fact]
    public async Task GetAllVendasAsync_Should_Return_List_Of_Vendas()
    {
        // Arrange
        var vendas = new List<Venda>
        {
            new Venda { Id = 1, ClienteId = 1, FilialId = 1 },
            new Venda { Id = 2, ClienteId = 2, FilialId = 2 }
        };

        _vendaRepository.GetAllAsync().Returns(vendas);
        _mapper.Map<IEnumerable<VendaDTO>>(vendas).Returns(new List<VendaDTO>());

        // Act
        var result = await _vendaService.GetAllVendasAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }
}
