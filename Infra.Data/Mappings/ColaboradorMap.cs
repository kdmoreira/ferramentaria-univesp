using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infra.Data.Mapping
{
    public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.ToTable("Colaboradores");
            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.Matricula).IsUnique();
            builder.Property(x => x.CPF).HasMaxLength(11).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Matricula).HasMaxLength(20).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Nome).HasMaxLength(255).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Sobrenome).HasMaxLength(255).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Telefone).HasMaxLength(20).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Cargo).HasMaxLength(100).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Empresa).HasMaxLength(100).IsRequired().HasColumnType("varchar");
            builder.Property(x => x.Perfil).IsRequired().HasColumnType("int");

            builder.HasOne(x => x.Supervisor)
                .WithMany(x => x.Supervisionados)
                .HasForeignKey(x => x.SupervisorID);

            // Seed
            var colaboradorAdmin = new Colaborador(Guid.Parse("c20cf935-802e-4dba-be10-14131ea6279a"), new DateTime(2021, 01, 01), "12345678912", "000", "Admin", "Admin",
                "N/A", "(00)0000-0000", "Admin", "N/A", PerfilEnum.Colaborador, false);
            builder.HasData(colaboradorAdmin);
        }
    }
}