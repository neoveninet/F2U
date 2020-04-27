using f2u.API.Models;
using Microsoft.EntityFrameworkCore;

namespace f2u.API.Data
{
    public class DataContext : DbContext
    {
        DbContextOptions _contextOptions;
        public DbSet<Value> Values { get; set; } 
        public DataContext(DbContextOptions contextOptions): base(contextOptions)
        {}
    }
}