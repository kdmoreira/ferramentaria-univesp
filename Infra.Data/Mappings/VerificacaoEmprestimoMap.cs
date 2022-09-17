using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infra.Data.Mappings
{
    public class VerificacaoEmprestimoMap : IEntityTypeConfiguration<VerificacaoEmprestimo>
    {
        public void Configure(EntityTypeBuilder<VerificacaoEmprestimo> builder)
        {
            builder.ToTable("VerificacoesEmprestimos");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.UltimaVerificacao).HasColumnType("timestamp without time zone");

            builder.HasData(
                new VerificacaoEmprestimo
                {
                    ID = Guid.Parse("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                    UltimaVerificacao = DateTime.UtcNow,
                    Ativo = true,
                    CriadoEm = DateTime.UtcNow,
                    CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000000")
                });
        }
    }
}
