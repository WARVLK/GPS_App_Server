using Microsoft.EntityFrameworkCore;
using ServerDB.Entities;


namespace ServerDB
{
    public class ServerDataBase : DbContext
    {
        public ServerDataBase(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(k => k.Id);
        }
    }
}
