using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.Login).IsUnique();
            builder.Property(x => x.Login).HasMaxLength(20).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Senha).HasMaxLength(255).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Token).HasMaxLength(1000).HasColumnType("varchar");
            builder.Property(x => x.Role).IsRequired().HasColumnType("int");

            builder.HasOne(x => x.Colaborador)
                .WithOne(x => x.Usuario)
                .HasForeignKey<Usuario>(x => x.ColaboradorID);

            // Seed
            var usuarioAdmin = new Usuario(Guid.Parse("bb9ac2c8-c7d4-4c21-b6cf-84419b12a810"), new DateTime(2021, 01, 01), 
                "12345678912", "$2a$12$ti.QT85lj9IANHP8VfAue.X4yzVM458OqmCtR0d1zBp9yv4mK2CGC", null, 
                RoleEnum.Administrador, Guid.Parse("c20cf935-802e-4dba-be10-14131ea6279a"), true);
            builder.HasData(usuarioAdmin);
        }
    }
}
