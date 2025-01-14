using SalesServiceAPI.Application.DTOs;

namespace SalesServiceAPI.Application.Interfaces.Services;

public interface IVendaService
{
    Task<VendaDTO> CreateVendaAsync(VendaDTO vendaDto);
    Task<VendaDTO> UpdateVendaAsync(int id, VendaDTO vendaDto);
    Task<bool> DeleteVendaAsync(int id);
    Task<VendaDTO> GetVendaByIdAsync(int id);
    Task<IEnumerable<VendaDTO>> GetAllVendasAsync();
}
