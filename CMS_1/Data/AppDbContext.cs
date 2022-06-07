using Microsoft.EntityFrameworkCore;

namespace CMS_1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Charset> Charsets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GiftCategory> GiftCategories { get; set; }
        public DbSet<ProgramSize> ProgramSizes { get; set; }
        public DbSet<RepeatSchedule> RepeatSchedules { get; set; }
        public DbSet<RuleOfGift> RuleOfGifts { get; set; }
        public DbSet<TimeFrame> TimeFrames { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ValueSchedule> ValueSchedules { get; set; }
        public DbSet<Winner> Winners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasData(
                    new { Id = 1, Email = "123@gmail.com", Password = "123Aaa" },
                    new { Id = 2, Email = "abc@gmail.com", Password = "123Qwe" }
                    );
            });
        }
    }
}
