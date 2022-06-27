using Microsoft.EntityFrameworkCore;

namespace CMS_1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Campaignn> Campaigns { get; set; }
        public DbSet<Charset> Charsets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GiftCategory> GiftCategories { get; set; }
        public DbSet<ProgramSize> ProgramSizes { get; set; }
        public DbSet<RepeatSchedule> RepeatSchedules { get; set; }
        public DbSet<RuleOfGift> RuleOfGifts { get; set; }
        
        public DbSet<User> Users { get; set; }
      //  public DbSet<ValueSchedule> ValueSchedules { get; set; }
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
            modelBuilder.Entity<Charset>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasData(
                    new { Id = 1, Name = "Numbers"},
                    new { Id = 2, Name = "Character" },
                    new { Id = 3, Name = "All"}
                    );
            });
            modelBuilder.Entity<ProgramSize>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasData(
                    new { Id = 1, Name = "Bulk codes" },
                    new { Id = 2, Name = "Standalone code" }
                    );
            });
            modelBuilder.Entity<RepeatSchedule>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasData(
                    new { Id = 1, Name = "Monthly on day" },
                    new { Id = 2, Name = "Weekly on" },
                    new { Id = 3, Name = "Repeat daily" }
                    );
            });
            modelBuilder.Entity<Campaignn>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasData(
                    new { Id = 1, Name = "Campaign 1", AutoUpdate = true, JoinOnlyOne = true, Decription = "Defaut campaign", CountCode = 0, StartDay = DateTime.Parse("2020-08-02"), StartTime = TimeSpan.Parse("07:00:00"), EndDay = DateTime.Parse("2020-10-02"), EndTime = TimeSpan.Parse("23:59:59"), IdProgramSize = 1 }
                    );
            });
            modelBuilder.Entity<RuleOfGift>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
               // entity.HasIndex(e => e.Priority).IsUnique();
            });
            modelBuilder.Entity<Barcode>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique();
            });
            modelBuilder.Entity<GiftCategory>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasData(
                    new { Id = 1, Name = "Hạt nêm Knorr Chay Nấm Hương 400g", Decription = "Hạt nêm Knorr Chay Nấm Hương 400g", Count = 2, Active = true, CreateDate = DateTime.Parse("2020-08-02") },
                    new { Id = 2, Name = "Hạt nêm Knorr Từ Thịt Thăn, Xương Ống & Tủy 600gr", Decription = "Hạt nêm Knorr Từ Thịt Thăn, Xương Ống & Tủy 600gr", Count = 1, Active = true, CreateDate = DateTime.Parse("2020-03-10") },
                    new { Id = 3, Name = "Gia vị Hoàn Chỉnh Knorr Canh Chua 30g", Decription = "Gia vị Hoàn Chỉnh Knorr Canh Chua 30g", Count = 0, Active = true, CreateDate = DateTime.Parse("2020-03-10") }
                    );
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasData(
                    new { Id = 1, Name = "Nguyễn Hữu Huân", PhoneNumber = "0901456781", DoB = DateTime.Parse("1973-03-01"), Position = "Chủ", TypeOfBusiness = "Khách sạn", Address = "Quận 6, TPHCM", IsBlock = false },
                    new { Id = 2, Name = "Nguyễn Trọng Hữu", PhoneNumber = "0907852781", DoB = DateTime.Parse("1974-04-01"), Position = "Quản lý", TypeOfBusiness = "Nhà hàng", Address = "Quận 5, TPHCM", IsBlock = false },
                    new { Id = 3, Name = "Trần Hùng Phát", PhoneNumber = "0901485381", DoB = DateTime.Parse("1975-05-01"), Position = "Bếp", TypeOfBusiness = "Quán ăn", Address = "Quận 7, TPHCM", IsBlock = false },
                    new { Id = 4, Name = "Lê Ngọc Anh", PhoneNumber = "0901451981", DoB = DateTime.Parse("1976-06-01"), Position = "Chủ", TypeOfBusiness = "Bán sỉ", Address = "Bến Lức, Long An", IsBlock = false },
                    new { Id = 5, Name = "Lê Phan", PhoneNumber = "0901742681", DoB = DateTime.Parse("1977-07-01"), Position = "Quản lý", TypeOfBusiness = "Quán ăn", Address = "Biên Hòa, Đồng Nai", IsBlock = false },
                    new { Id = 6, Name = "Nguyễn Thị Ngọc Hương", PhoneNumber = "0904803457", DoB = DateTime.Parse("1978-08-01"), Position = "Chủ", TypeOfBusiness = "Quán ăn", Address = "Bến Lức, Long An", IsBlock = false },
                    new { Id = 7, Name = "Trần Văn Tình", PhoneNumber = "0947514514", DoB = DateTime.Parse("1979-09-01"), Position = "Chủ", TypeOfBusiness = "Resort", Address = "Cai Lậy, Tiền Giang", IsBlock = false }                          
                    );
            });
            modelBuilder.Entity<Gift>(entity =>
            {
                entity.HasData(
                    new { Id = 1, GiftCode = "GIF2DHMAAB3E9Y", GiftName= "Hạt nêm Knorr Chay Nấm Hương 400g", CreateDate= DateTime.Parse("2020-03-10"), UsageLimit = 1, Active = true, Used = 1 , IdGiftCategory = 1 , IdCampaign = 1 },
                    new { Id = 2, GiftCode = "GIF2DERGH1B3WE", GiftName = "Hạt nêm Knorr Chay Nấm Hương 400g", CreateDate = DateTime.Parse("2020-03-10"), UsageLimit = 1, Active = true, Used = 1, IdGiftCategory = 1, IdCampaign = 1 },
                    new { Id = 3, GiftCode = "GIF2DQBJLYNCVSD", GiftName = "Hạt nêm Knorr Từ Thịt Thăn, Xương Ống & Tủy 600gr", CreateDate = DateTime.Parse("2020-03-10"), UsageLimit = 1, Active = true, Used = 1, IdGiftCategory = 2, IdCampaign = 1 }
                    );
            });
            modelBuilder.Entity<Winner>(entity =>
            {
                entity.HasData(
                   new { Id = 1, WinDate = DateTime.Parse("2020-03-12"), SendGiftStatus = true, IdCustomer = 1, IdGift = 1 },
                   new { Id = 2, WinDate = DateTime.Parse("2020-03-12"), SendGiftStatus = true, IdCustomer = 2, IdGift = 2 },
                   new { Id = 3, WinDate = DateTime.Parse("2020-03-12"), SendGiftStatus = true, IdCustomer = 3, IdGift = 3 }
                   );
            });
        }
    }
}
