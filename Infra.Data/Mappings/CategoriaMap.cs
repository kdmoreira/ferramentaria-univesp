using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Mappings
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Descricao).HasMaxLength(30).IsRequired().HasColumnType("varchar");

            // Seed
            var ferramenta = new Categoria(Guid.Parse("5138f09b-7dc6-4e06-a983-c182e6d7d173"), new DateTime(2021, 01, 01), "Ferramenta", true);
            var dispositivo = new Categoria(Guid.Parse("61858a59-e022-4ace-8531-8db2d62b739e"), new DateTime(2021, 01, 01), "Dispositivo", true);
            var instrumento = new Categoria(Guid.Parse("70daebeb-22d5-4b70-8052-9211b92b9552"), new DateTime(2021, 01, 01), "Instrumento", true);
            var equipamento = new Categoria(Guid.Parse("1d16e4d4-59ac-438f-b484-fa097d3be8f3"), new DateTime(2021, 01, 01), "Equipamento", true);
            builder.HasData(ferramenta, dispositivo, instrumento, equipamento);
        }
    }
}
