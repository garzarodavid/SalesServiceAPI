using Microsoft.EntityFrameworkCore;
using SalesServiceAPI.Domain.Entities;
using SalesServiceAPI.Domain.Repositories;
using SalesServiceAPI.Infrastructure.Data.Context;

namespace SalesServiceAPI.Infrastructure.Repositories;



public class FilialRepository : GenericRepository<Filial>, IFilialRepository
{
    public FilialRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<Filial?> GetByNameAsync(string name)
    {
        return await _context.Set<Filial>().FirstOrDefaultAsync(f => f.Nome == name);
    }
}

