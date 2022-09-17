﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping
{
    public class EmprestimoMap : IEntityTypeConfiguration<Emprestimo>
    {
        public void Configure(EntityTypeBuilder<Emprestimo> builder)
        {
            builder.ToTable("Emprestimos");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.DataEmprestimo).IsRequired().HasColumnType("timestamp without time zone");
            builder.Property(x => x.DataDevolucao).IsRequired().HasColumnType("timestamp without time zone");
            builder.Property(x => x.Observacao).HasMaxLength(255).HasColumnType("varchar");
            builder.Property(x => x.Quantidade).IsRequired().HasColumnType("int");
            builder.Property(x => x.Status).IsRequired().HasColumnType("int");

            builder.HasOne(x => x.Colaborador)
                .WithMany(x => x.Emprestimos)
                .HasForeignKey(x => x.ColaboradorID);

            builder.HasOne(x => x.Ferramenta)
                .WithMany(x => x.Emprestimos)
                .HasForeignKey(x => x.FerramentaID);
        }
    }
}