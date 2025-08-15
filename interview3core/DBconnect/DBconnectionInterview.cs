using interview3core.Models;
using Microsoft.EntityFrameworkCore;

namespace interview3core.DBconnect
{
    public class DBconnectionInterview:DbContext
    {
        public DBconnectionInterview(DbContextOptions options) : base(options) { }
        public DbSet<Students> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<interview3core.Models.Register> Register { get; set; } = default!;
        public DbSet<interview3core.Models.LoginViewModel> LoginViewModel { get; set; } = default!;
    }
}
