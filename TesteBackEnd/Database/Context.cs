using System.Threading.Tasks;
using Application;
using Database.FluentAPIConfiguration;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context:DbContext, IContext
    {
        public Context()
        {

        }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public Task SaveChangesAsync() =>  SaveChangesAsync(default);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteApiConfiguration());
        }
    }
}