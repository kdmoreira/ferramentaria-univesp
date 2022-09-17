using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;

namespace Infra.Data.Repository
{
    public class LoweredCaseMigrationHistoryRepository : NpgsqlHistoryRepository
    {
        public LoweredCaseMigrationHistoryRepository(HistoryRepositoryDependencies dependencies)
        : base(dependencies)
        {
        }

        protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
        {
            base.ConfigureTable(history);
            history.Property(h => h.MigrationId).HasColumnName("migrationid");
            history.Property(h => h.ProductVersion).HasColumnName("productversion");
        }
    }
}
