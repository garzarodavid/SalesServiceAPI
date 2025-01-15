using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesServiceAPI.Domain.Entities;

public class Filial
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public ICollection<Venda> Vendas { get; set; }
}
