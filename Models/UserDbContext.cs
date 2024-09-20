using Microsoft.EntityFrameworkCore;

namespace Demo_3.Models
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options): base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
