using AutoMapper;
using SalesServiceAPI.Application.DTOs;
using SalesServiceAPI.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamento de Cliente
        CreateMap<Cliente, ClienteDTO>().ReverseMap();

        // Mapeamento de ItemVenda
        CreateMap<ItemVenda, ItemVendaDTO>()
            .ForMember(dest => dest.ProdutoNome, opt => opt.MapFrom(src => src.Produto.Nome))
            .ForMember(dest => dest.Desconto, opt => opt.MapFrom(src => src.Desconto)) 
            .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal)) 
            .ReverseMap();

        // Mapeamento de Produto
        CreateMap<Produto, ProdutoDTO>().ReverseMap();

        // Mapeamento de Venda
        CreateMap<Venda, VendaDTO>()
            .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Cliente.Nome))
            .ForMember(dest => dest.ClienteEmail, opt => opt.MapFrom(src => src.Cliente.Email)) 
            .ForMember(dest => dest.FilialNome, opt => opt.MapFrom(src => src.Filial.Nome))
            .ForMember(dest => dest.FilialEndereco, opt => opt.MapFrom(src => src.Filial.Endereco))
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens)) 
            .ReverseMap();

        // Mapeamento de Filial
        CreateMap<Filial, FilialDTO>().ReverseMap();
    }
}