namespace SalesServiceAPI.Application.DTOs;

/// <summary>
/// DTO para representar um Item de Venda.
/// </summary>
public class ItemVendaDTO
{
    /// <summary>
    /// Id do Item de Venda.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// Id do Produto.
    /// </summary>
    /// <example>10</example>
    public int ProdutoId { get; set; }

    /// <summary>
    /// Nome do Produto.
    /// </summary>
    /// <example>Produto A</example>
    public string ProdutoNome { get; set; }

    /// <summary>
    /// Quantidade do Produto Vendida.
    /// </summary>
    /// <example>2</example>
    public int Quantidade { get; set; }

    /// <summary>
    /// Valor Unitário do Produto.
    /// </summary>
    /// <example>50.00</example>
    public decimal ValorUnitario { get; set; }

    /// <summary>
    /// Desconto Aplicado no Produto.
    /// </summary>
    /// <example>5.00</example>
    public decimal Desconto { get; set; }

    /// <summary>
    /// Valor Total do Item de Venda (Quantidade * (ValorUnitário - Desconto)).
    /// </summary>
    /// <example>90.00</example>
    public decimal ValorTotal { get; set; }
}