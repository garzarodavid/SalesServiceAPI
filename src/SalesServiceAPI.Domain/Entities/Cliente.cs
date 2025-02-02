﻿namespace SalesServiceAPI.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public ICollection<Venda> Vendas { get; set; }
}