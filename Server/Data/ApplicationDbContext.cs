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
        public DbSet<Trade> Trades  { get; set; }
        public DbSet<TradeComment> TradeComments { get; set; }
        public DbSet<TradeOffer> TradeOffers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
        base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Chat>().HasAlternateKey(f => new { f.UserId1, f.UserId2 });
            modelBuilder.Entity<TradeComment>().HasKey(tc => new { tc.TradeId,tc.CreateTime});
            modelBuilder.Entity<TradeOffer>().HasKey(t => new { t.TradeId, t.CreateTime });
        }
    }
}
