using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesServiceAPI.Domain.Entities;

namespace SalesServiceAPI.Infrastructure.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
public void Configure(EntityTypeBuilder<Produto> builder)
{
    builder.HasKey(p => p.Id);
    builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
    builder.Property(p => p.Preco).HasColumnType("decimal(18,2)");
}
}
