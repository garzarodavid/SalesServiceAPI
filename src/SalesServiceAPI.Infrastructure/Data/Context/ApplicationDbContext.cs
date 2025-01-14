using Microsoft.EntityFrameworkCore;
using SalesServiceAPI.Domain.Entities;
using SalesServiceAPI.Infrastructure.Data.Configurations;

namespace SalesServiceAPI.Infrastructure.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<ItemVenda> ItensVenda { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Filial> Filiais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new VendaConfiguration());
        modelBuilder.ApplyConfiguration(new ItemVendaConfiguration());
        modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        modelBuilder.ApplyConfiguration(new FilialConfiguration());
    }
}
