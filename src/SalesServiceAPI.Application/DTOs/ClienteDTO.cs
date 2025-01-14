namespace SalesServiceAPI.Application.DTOs;

/// <summary>
/// DTO para representar um Cliente.
/// </summary>
public class ClienteDTO
{
    /// <summary>
    /// Id do Cliente.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// Nome do Cliente.
    /// </summary>
    /// <example>João Silva</example>
    public string Nome { get; set; }

    /// <summary>
    /// Email do Cliente.
    /// </summary>
    /// <example>joao.silva@example.com</example>
    public string Email { get; set; }
}
