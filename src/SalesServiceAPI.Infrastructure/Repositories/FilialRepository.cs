using SalesServiceAPI.Domain.Entities;
using SalesServiceAPI.Domain.Repositories;
using SalesServiceAPI.Infrastructure.Data.Context;

namespace SalesServiceAPI.Infrastructure.Repositories;



public class FilialRepository : GenericRepository<Filial>, IFilialRepository
{
    public FilialRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Implemente métodos específicos de Filial, se necessário
}

