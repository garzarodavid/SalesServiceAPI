using SalesServiceAPI.Domain.Entities;

namespace SalesServiceAPI.Domain.Repositories;

public interface IFilialRepository : IGenericRepository<Filial>
{
    Task<Filial?> GetByNameAsync(string name);
}