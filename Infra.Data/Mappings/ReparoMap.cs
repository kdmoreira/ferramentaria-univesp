﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class ReparoMap : IEntityTypeConfiguration<Reparo>
    {
        public void Configure(EntityTypeBuilder<Reparo> builder)
        {
            builder.ToTable("Reparos");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.DataInicio).HasColumnType("timestamp without time zone");
            builder.Property(x => x.DataFim).HasColumnType("timestamp without time zone");

            builder.HasOne(x => x.Ferramenta)
                .WithMany(x => x.Reparos)
                .HasForeignKey(x => x.FerramentaID);
        }
    }
}
