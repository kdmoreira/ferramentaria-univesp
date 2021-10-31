using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping
{
    public class FerramentaMap : IEntityTypeConfiguration<Ferramenta>
    {
        public void Configure(EntityTypeBuilder<Ferramenta> builder)
        {
            builder.ToTable("Ferramentas");
            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.Codigo).IsUnique();
            builder.Property(x => x.Codigo).HasMaxLength(100).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Descricao).HasMaxLength(255).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.NumeroPatrimonial).HasMaxLength(50).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Fabricante).HasMaxLength(50).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.QuantidadeDisponivel).IsRequired().HasColumnType("int");
            builder.Property(x => x.QuantidadeTotal).IsRequired().HasColumnType("int");
            builder.Property(x => x.ValorCompra).IsRequired().HasColumnType("float");
            builder.Property(x => x.Localizacao).HasMaxLength(100).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Status).IsRequired().HasColumnType("int");

            builder.HasOne(x => x.Categoria)
                .WithMany(x => x.Ferramentas)
                .HasForeignKey(x => x.CategoriaID);
        }
    }
}
