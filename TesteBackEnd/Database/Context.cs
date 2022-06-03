﻿using System.Threading.Tasks;
using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context:DbContext, IContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public Task SaveChangesAsync() =>  SaveChangesAsync(default);
        
        
    }
}