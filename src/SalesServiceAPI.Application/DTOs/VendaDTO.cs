namespace SalesServiceAPI.Application.DTOs;

/// <summary>
/// DTO para representar uma Venda.
/// </summary>
public class VendaDTO
{
    /// <summary>
    /// Id da Venda.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// Data da Venda.
    /// </summary>
    /// <example>2023-05-01T14:30:00</example>
    public DateTime Data { get; set; }

    /// <summary>
    /// Id do Cliente.
    /// </summary>
    /// <example>1</example>
    public int ClienteId { get; set; }

    /// <summary>
    /// Nome do Cliente.
    /// </summary>
    /// <example>João Silva</example>
    public string ClienteNome { get; set; }

    /// <summary>
    /// Valor Total da Venda.
    /// </summary>
    /// <example>100.00</example>
    public decimal ValorTotal { get; set; }

    /// <summary>
    /// Itens da Venda.
    /// </summary>
    public ICollection<ItemVendaDTO> Itens { get; set; }

    /// <summary>
    /// Venda Cancelada.
    /// </summary>
    /// <example>false</example>
    public bool Cancelado { get; set; }

    /// <summary>
    /// Id da Filial.
    /// </summary>
    /// <example>5</example>
    public int FilialId { get; set; }

    /// <summary>
    /// Nome da Filial.
    /// </summary>
    /// <example>Filial Central</example>
    public string FilialNome { get; set; }
}