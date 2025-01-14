using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesServiceAPI.Domain.Entities;

namespace SalesServiceAPI.Infrastructure.Data.Configurations;

public class ItemVendaConfiguration : IEntityTypeConfiguration<ItemVenda>
{
    public void Configure(EntityTypeBuilder<ItemVenda> builder)
    {
        builder.HasKey(iv => iv.Id);
        builder.Property(iv => iv.Quantidade).IsRequired();
        builder.Property(iv => iv.ValorUnitario).HasColumnType("decimal(18,2)");
        builder.Property(iv => iv.Desconto).HasColumnType("decimal(18,2)");
        builder.Ignore(iv => iv.ValorTotal); 
        builder.HasOne(iv => iv.Produto)
               .WithMany()
               .HasForeignKey(iv => iv.ProdutoId);
        builder.HasOne(iv => iv.Venda)
               .WithMany(v => v.Itens)
               .HasForeignKey(iv => iv.VendaId);
    }
}
