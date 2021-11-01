using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.Login).IsUnique();
            builder.Property(x => x.Login).HasMaxLength(11).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Senha).HasMaxLength(255).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Token).HasMaxLength(1000).HasColumnType("varchar");
            builder.Property(x => x.Role).IsRequired().HasColumnType("int");

            builder.HasOne(x => x.Colaborador)
                .WithOne(x => x.Usuario)
                .HasForeignKey<Usuario>(x => x.ColaboradorID);
        }
    }
}
