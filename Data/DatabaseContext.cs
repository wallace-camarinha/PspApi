using Microsoft.EntityFrameworkCore;
using PspApi.Models;

namespace PspApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Merchant> Merchants { get; set; }

    }
}
