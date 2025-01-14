using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesServiceAPI.Domain.Entities;

namespace SalesServiceAPI.Infrastructure.Data.Configurations;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Data).IsRequired();
        builder.Property(v => v.ValorTotal).HasColumnType("decimal(18,2)");
        builder.HasOne(v => v.Cliente)
               .WithMany(c => c.Vendas)
               .HasForeignKey(v => v.ClienteId);
        builder.HasMany(v => v.Itens)
               .WithOne(i => i.Venda)
               .HasForeignKey(i => i.VendaId);
        builder.HasOne(v => v.Filial)
               .WithMany(f => f.Vendas)
               .HasForeignKey(v => v.FilialId);
    }
}
