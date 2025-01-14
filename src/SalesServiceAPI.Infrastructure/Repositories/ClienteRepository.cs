using SalesServiceAPI.Domain.Entities;
using SalesServiceAPI.Domain.Repositories;
using SalesServiceAPI.Infrastructure.Data.Context;

namespace SalesServiceAPI.Infrastructure.Repositories;



public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(ApplicationDbContext context) : base(context)
    {
    }
}