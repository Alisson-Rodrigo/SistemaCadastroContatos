using Microsoft.EntityFrameworkCore;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UserModel> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Map.ContatoMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
