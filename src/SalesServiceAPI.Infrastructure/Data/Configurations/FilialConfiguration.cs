using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesServiceAPI.Domain.Entities;

namespace SalesServiceAPI.Infrastructure.Data.Configurations;

public class FilialConfiguration : IEntityTypeConfiguration<Filial>
{
    public void Configure(EntityTypeBuilder<Filial> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Nome).IsRequired().HasMaxLength(100);
        builder.Property(f => f.Endereco).IsRequired().HasMaxLength(200);
        builder.HasMany(f => f.Vendas)
               .WithOne(v => v.Filial)
               .HasForeignKey(v => v.FilialId);
    }
}
