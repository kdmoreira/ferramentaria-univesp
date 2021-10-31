using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class AfericaoMap : IEntityTypeConfiguration<Afericao>
    {
        public void Configure(EntityTypeBuilder<Afericao> builder)
        {
            builder.ToTable("Afericoes");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.IntervaloDias).IsRequired().HasColumnType("int");
            builder.Property(x => x.DataUltima).HasColumnType("datetime2");

            builder.HasOne(x => x.Ferramenta)
                .WithOne(x => x.Afericao)
                .HasForeignKey<Ferramenta>(x => x.AfericaoID);
        }
    }
}
