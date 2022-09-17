using Domain.Interfaces.Repositories;
using Infra.Data.Context;
using Infra.Data.Implementations;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            // Context
            serviceCollection.AddDbContext<FerramentariaContext>(
                options => options.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRESQL"),
                x => x.MigrationsHistoryTable("__efmigrationshistory", "public"))
                .ReplaceService<IHistoryRepository, LoweredCaseMigrationHistoryRepository>());

            serviceCollection.AddScoped<DbContext, FerramentariaContext>();

            // Repositories
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}