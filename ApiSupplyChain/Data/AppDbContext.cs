using Microsoft.EntityFrameworkCore;

namespace ApiSupplyChain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }
        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Estoque> Estoque{ get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasData(
                    new Produto { Id = 1, Name = "Macacão RF", Manufacturer = "Empresa XPTO", Register_Number = 0001, Type = "EPI", Description = "Macacão de proteção individual" },
                    new Produto { Id = 2, Name = "Óculos de proteção", Manufacturer = "Empresa XPTO", Register_Number = 0002, Type = "EPI", Description = "Óculos de proteção individual" },
                    new Produto { Id = 3, Name = "Bota", Manufacturer = "Empresa XPTO", Register_Number = 0003, Type = "EPI", Description = "Bota de proteção individual" }
                    ); ;
        }
    }
}
