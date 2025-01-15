using Microsoft.AspNetCore.Mvc;
using SalesServiceAPI.Application.DTOs;
using SalesServiceAPI.Application.Interfaces.Services;
using SalesServiceAPI.Domain.Entities;

namespace SalesServiceAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendaController : ControllerBase
{
    private readonly IVendaService _vendaService;

    public VendaController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    /// <summary>
    /// Retorna todas as vendas.
    /// </summary>
    /// <returns>Lista de vendas.</returns>
    /// <response code="200">Retorna a lista de vendas.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VendaDTO>), 200)]
    public async Task<IActionResult> GetAllVendas()
    {
        var vendas = await _vendaService.GetAllVendasAsync();
        if (vendas?.Count() == 0 || vendas == null)
        {
            return NotFound();
        }
        return Ok(vendas);
    }

    /// <summary>
    /// Retorna uma venda específica pelo ID.
    /// </summary>
    /// <param name="id">Id da venda.</param>
    /// <returns>Venda correspondente ao ID.</returns>
    /// <response code="200">Retorna a venda.</response>
    /// <response code="404">Se a venda não for encontrada.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VendaDTO), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetVendaById(int id)
    {
        var venda = await _vendaService.GetVendaByIdAsync(id);
        if (venda == null)
        {
            return NotFound();
        }
        return Ok(venda);
    }

    /// <summary>
    /// Cria uma nova venda.
    /// </summary>
    /// <param name="vendaDto">Dados da venda a ser criada.</param>
    /// <returns>Venda criada.</returns>
    /// <response code="201">Retorna a venda recém-criada.</response>
    /// <response code="400">Se os dados da venda forem inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(VendaDTO), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateVenda(VendaDTO vendaDto)
    {
        if (vendaDto == null)
        {
            return BadRequest();
        }

        var vendaCriada = await _vendaService.CreateVendaAsync(vendaDto);
        return CreatedAtAction(nameof(GetVendaById), new { id = vendaCriada.Id }, vendaCriada);
    }

    /// <summary>
    /// Atualiza uma venda existente pelo ID.
    /// </summary>
    /// <param name="id">Id da venda a ser atualizada.</param>
    /// <param name="vendaDto">Dados atualizados da venda.</param>
    /// <returns>Venda atualizada.</returns>
    /// <response code="200">Retorna a venda atualizada.</response>
    /// <response code="400">Se os dados da venda forem inválidos.</response>
    /// <response code="404">Se a venda não for encontrada.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(VendaDTO), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateVenda(int id, VendaDTO vendaDto)
    {
        if (vendaDto == null || id != vendaDto.Id)
        {
            return BadRequest();
        }

        try
        {
            var vendaAtualizada = await _vendaService.UpdateVendaAsync(id, vendaDto);
            return Ok(vendaAtualizada);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deleta uma venda pelo ID.
    /// </summary>
    /// <param name="id">Id da venda a ser deletada.</param>
    /// <returns>Confirmação da exclusão.</returns>
    /// <response code="204">Confirmação de que a venda foi deletada.</response>
    /// <response code="404">Se a venda não for encontrada.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteVenda(int id)
    {
        try
        {
            var deleted = await _vendaService.DeleteVendaAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
