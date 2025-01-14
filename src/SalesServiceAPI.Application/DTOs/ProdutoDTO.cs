namespace SalesServiceAPI.Application.DTOs;

/// <summary>
/// DTO para representar um Produto.
/// </summary>
public class ProdutoDTO
{
    /// <summary>
    /// Id do Produto.
    /// </summary>
    /// <example>10</example>
    public int Id { get; set; }

    /// <summary>
    /// Nome do Produto.
    /// </summary>
    /// <example>Produto A</example>
    public string Nome { get; set; }

    /// <summary>
    /// Preço do Produto.
    /// </summary>
    /// <example>50.00</example>
    public decimal Preco { get; set; }
}
