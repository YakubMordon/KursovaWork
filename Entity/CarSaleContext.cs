using KursovaWork.Entity.Entities.Car;
using KursovaWork.Entity.Entities;
using KursovaWork.Services;
using Microsoft.EntityFrameworkCore;

namespace KursovaWork.Entity
{
    public class CarSaleContext : DbContext
    {
        public CarSaleContext(DbContextOptions<CarSaleContext> options)
        : base(options)
        {
        }

        public CarSaleContext() : base() { }

        public DbSet<CarInfo> Cars { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Card> Cards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarInfo>()
                .HasMany(c => c.Images)
                .WithOne(ci => ci.Car)
                .HasForeignKey(ci => ci.CarId);

            modelBuilder.Entity<CarInfo>()
                .HasOne(c => c.Detail)
                .WithOne(cd => cd.Car)
                .HasForeignKey<CarDetail>(cd => cd.CarId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Car)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CarId);

            // Додання шифрування кредитної карти
            modelBuilder.Entity<Card>()
            .Property(o => o.CardNumber)
            .HasConversion(
                card => Encrypter.Encrypt(card),
                encryptedCard => Encrypter.Decrypt(encryptedCard)
            );

            // Додання шифрування місяця
            modelBuilder.Entity<Card>()
            .Property(o => o.ExpirationMonth)
            .HasConversion(
                month => Encrypter.EncryptMonth(month),
                encryptedMonth => Encrypter.DecryptMonth(encryptedMonth)
            );

            // Додання шифрування року
            modelBuilder.Entity<Card>()
            .Property(o => o.ExpirationYear)
            .HasConversion(
                year => Encrypter.EncryptYear(year),
                encryptedYear => Encrypter.DecryptYear(encryptedYear)
            );

            // Додання шифрування CVV коду
            modelBuilder.Entity<Card>()
            .Property(o => o.CVV)
            .HasConversion(
                CVV => Encrypter.EncryptCVV(CVV),
                encryptedCVV => Encrypter.DecryptCVV(encryptedCVV)
            );

            base.OnModelCreating(modelBuilder);
        }

        public void FillDB()
        {
            DbInitializer.Initialize(this);
        }
    }
}

