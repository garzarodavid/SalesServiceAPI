using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesServiceAPI.Domain.Entities;

public class ItemVenda
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public Venda Venda { get; set; }
    public int Quantidade { get; set; }
    public int VendaId { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Desconto { get; set; }
    public decimal ValorTotal => Quantidade * (ValorUnitario - Desconto);
}
