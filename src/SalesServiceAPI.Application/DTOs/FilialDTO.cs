namespace SalesServiceAPI.Application.DTOs;

/// <summary>
/// DTO para representar uma Filial.
/// </summary>
public class FilialDTO
{
    /// <summary>
    /// Id da Filial.
    /// </summary>
    /// <example>5</example>
    public int Id { get; set; }

    /// <summary>
    /// Nome da Filial.
    /// </summary>
    /// <example>Filial Central</example>
    public string Nome { get; set; }

    /// <summary>
    /// Endereço da Filial.
    /// </summary>
    /// <example>Rua Principal, 123</example>
    public string Endereco { get; set; }
}
