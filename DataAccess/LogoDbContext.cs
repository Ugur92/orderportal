
using DataAccess.LogoModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public partial class LogoDbContext : DbContext
    {
        public DbSet<LG_050_01_CLFLINE> LG_050_01_CLFLINE { get; set; }
        public DbSet<LG_050_CLCARD> LG_050_CLCARD { get; set; }


        public LogoDbContext(DbContextOptions<LogoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}