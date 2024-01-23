using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Reflection.Metadata;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
        base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Chat>().HasAlternateKey(f => new { f.UserId1, f.UserId2 });
        }
    }
}
