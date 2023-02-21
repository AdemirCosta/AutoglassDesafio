using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AutoglassContext : DbContext
    {
        public AutoglassContext(DbContextOptions<AutoglassContext> options) : base(options)
        {
            
        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}
