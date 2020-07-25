using Banco.Models;
using Microsoft.EntityFrameworkCore;

namespace Banco.Persistence
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
