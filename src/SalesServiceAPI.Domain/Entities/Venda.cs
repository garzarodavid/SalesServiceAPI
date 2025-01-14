using SalesServiceAPI.Domain.Entities;

namespace SalesServiceAPI.Domain.Entities;

public class Venda
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public decimal ValorTotal { get; set; }
    public ICollection<ItemVenda> Itens { get; set; }
    public bool Cancelado { get; set; }
    public int FilialId { get; set; }
    public Filial Filial { get; set; }
}