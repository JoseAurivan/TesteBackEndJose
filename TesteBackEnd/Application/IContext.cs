using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application
{
    public interface IContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        
        Task SaveChangesAsync();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entry) where TEntity: class;
        EntityEntry Entry(object entry);
    }
}