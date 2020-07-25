using Banco.Models;
using Microsoft.EntityFrameworkCore;

namespace Banco.Persistence
{
    public class ContaDbContext : DbContext
    {
        public ContaDbContext(DbContextOptions<ContaDbContext> options) : base(options)
        {

        }

        public DbSet<Conta> Contas { get; set; }


    }
}
