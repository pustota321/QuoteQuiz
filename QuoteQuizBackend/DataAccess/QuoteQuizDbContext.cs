using Microsoft.EntityFrameworkCore;
using QuoteQuizBackend.Entities;

namespace QuoteQuizBackend.DataAccess
{
    public class QuoteQuizDbContext : DbContext
    {
        public QuoteQuizDbContext(DbContextOptions<QuoteQuizDbContext> options) : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Player> Players{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>().HasOne(e => e.Author).WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
