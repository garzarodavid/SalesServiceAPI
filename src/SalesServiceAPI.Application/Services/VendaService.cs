using AutoMapper;
using SalesServiceAPI.Application.DTOs;
using SalesServiceAPI.Application.Interfaces.Services;
using SalesServiceAPI.Domain.Entities;
using SalesServiceAPI.Domain.Repositories;
using Serilog;

namespace SalesServiceAPI.Application.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFilialRepository _filialRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VendaService(
            IVendaRepository vendaRepository,
            IProdutoRepository produtoRepository,
            IClienteRepository clienteRepository,
            IFilialRepository filialRepository,
            IMapper mapper,
            ILogger logger)
        {
            _vendaRepository = vendaRepository;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
            _filialRepository = filialRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<VendaDTO> CreateVendaAsync(VendaDTO vendaDto)
        {
            // Mapear DTO para entidade
            var venda = _mapper.Map<Venda>(vendaDto);

            // Validar e aplicar regras de negócios
            foreach (var item in venda.Itens)
            {
                if (item.Quantidade > 20)
                {
                    throw new Exception("Não é possível vender acima de 20 itens iguais.");
                }
                else if (item.Quantidade >= 10)
                {
                    item.Desconto = item.ValorUnitario * 0.20m;
                }
                else if (item.Quantidade >= 4)
                {
                    item.Desconto = item.ValorUnitario * 0.10m;
                }
                else
                {
                    item.Desconto = 0m;
                }
            }

            // Calcular o valor total da venda
            venda.ValorTotal = venda.Itens.Sum(i => i.ValorTotal);

            // Salvar venda no repositório
            await _vendaRepository.AddAsync(venda);

            // Mapear entidade para DTO
            var vendaCriada = _mapper.Map<VendaDTO>(venda);

            // Log da venda criada
            _logger.Information("Venda criada: {@Venda}", vendaCriada);

            return vendaCriada;
        }

        public async Task<VendaDTO> UpdateVendaAsync(int id, VendaDTO vendaDto)
        {
            var vendaExistente = await _vendaRepository.GetByIdAsync(id);
            if (vendaExistente == null)
            {
                throw new Exception("Venda não encontrada.");
            }

            var venda = _mapper.Map<Venda>(vendaDto);

            // Validar e aplicar regras de negócios
            foreach (var item in venda.Itens)
            {
                if (item.Quantidade > 20)
                {
                    throw new Exception("Não é possível vender acima de 20 itens iguais.");
                }
                else if (item.Quantidade >= 10)
                {
                    item.Desconto = item.ValorUnitario * 0.20m;
                }
                else if (item.Quantidade >= 4)
                {
                    item.Desconto = item.ValorUnitario * 0.10m;
                }
                else
                {
                    item.Desconto = 0m;
                }
            }

            // Calcular o valor total da venda
            venda.ValorTotal = venda.Itens.Sum(i => i.ValorTotal);

            // Atualizar venda no repositório
            await _vendaRepository.UpdateAsync(venda);

            var vendaAtualizada = _mapper.Map<VendaDTO>(venda);

            // Log da venda atualizada
            _logger.Information("Venda atualizada: {@Venda}", vendaAtualizada);

            return vendaAtualizada;
        }

        public async Task<bool> DeleteVendaAsync(int id)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
            {
                throw new Exception("Venda não encontrada.");
            }

            await _vendaRepository.DeleteAsync(venda);

            // Log da venda deletada
            _logger.Information("Venda deletada: {@VendaId}", id);

            return true;
        }

        public async Task<VendaDTO> GetVendaByIdAsync(int id)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
            {
                throw new Exception("Venda não encontrada.");
            }

            var vendaDto = _mapper.Map<VendaDTO>(venda);
            return vendaDto;
        }

        public async Task<IEnumerable<VendaDTO>> GetAllVendasAsync()
        {
            var vendas = await _vendaRepository.GetAllAsync();
            var vendaDtos = _mapper.Map<IEnumerable<VendaDTO>>(vendas);
            return vendaDtos;
        }
    }
}