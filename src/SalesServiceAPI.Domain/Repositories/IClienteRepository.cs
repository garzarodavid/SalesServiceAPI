using SalesServiceAPI.Domain.Entities;

namespace SalesServiceAPI.Domain.Repositories;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<Cliente?> GetByEmailAsync(string email);
}

