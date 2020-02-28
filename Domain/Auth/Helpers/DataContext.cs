using Microsoft.EntityFrameworkCore;
using Tester.Db.Model.Client;

namespace Auth.Helpers
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}