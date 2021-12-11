using LocalComminication.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocalComminication.Server.Contexts
{
    public class ClientDbContext : DbContext
    {

        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {

        }

        public DbSet<ClientEntity> Clients { get; set; } = null!;
    }
}
