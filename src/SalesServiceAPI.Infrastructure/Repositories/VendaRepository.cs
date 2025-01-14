using SalesServiceAPI.Domain.Entities;
using SalesServiceAPI.Domain.Repositories;
using SalesServiceAPI.Infrastructure.Data.Context;

namespace SalesServiceAPI.Infrastructure.Repositories;

public class VendaRepository : GenericRepository<Venda>, IVendaRepository
{
    public VendaRepository(ApplicationDbContext context) : base(context)
    {
    }
}