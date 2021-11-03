using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Infra.Data.Context
{
    public class FerramentariaContext : DbContext
    {
        public DbSet<Ferramenta> Ferramentas { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Afericao> Afericoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Reparo> Reparos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VerificacaoEmprestimo> VerificacoesEmprestimos { get; set; }

        public FerramentariaContext(DbContextOptions<FerramentariaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(type => !string.IsNullOrEmpty(type.Namespace))
                                  .Where(type => type.GetInterfaces().Any(i => i.IsGenericType
                                  && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}