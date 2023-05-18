using KursovaWorkDAL.Entity.Entities.Car;
using KursovaWorkDAL.Entity.Entities;
using KursovaWorkDAL.Entity.Service;
using Microsoft.EntityFrameworkCore;

namespace KursovaWorkDAL.Entity
{
    /// <summary>
    /// Контекст бази даних для продажу автомобілів.
    /// </summary>
    public class CarSaleContext : DbContext
    {
        /// <summary>
        /// Ініціалізує новий екземпляр класу CarSaleContext з заданими опціями.
        /// </summary>
        /// <param name="options">Опції бази даних.</param>
        public CarSaleContext(DbContextOptions<CarSaleContext> options)
        : base(options)
        {
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу CarSaleContext.
        /// </summary>
        public CarSaleContext() : base() { }

        /// <summary>
        /// Представляє таблицю автомобілів в базі даних.
        /// </summary>
        public DbSet<CarInfo> Cars { get; set; }

        /// <summary>
        /// Представляє таблицю зображень автомобілів в базі даних.
        /// </summary>
        public DbSet<CarImage> CarImages { get; set; }

        /// <summary>
        /// Представляє таблицю деталей автомобілів в базі даних.
        /// </summary>
        public DbSet<CarDetail> CarDetails { get; set; }

        /// <summary>
        /// Представляє таблицю користувачів в базі даних.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Представляє таблицю замовлень автомобілів в базі даних.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Представляє таблицю кредитних карт користувачів в базі даних.
        /// </summary>
        public DbSet<Card> Cards { get; set; }

        /// <summary>
        /// Представляє таблицю опцій конфігуратора автомобілів в базі даних.
        /// </summary>
        public DbSet<ConfiguratorOptions> ConfiguratorOptions { get; set; }

        /// <summary>
        /// Визначає модель бази даних та встановлює зв'язки між таблицями.
        /// </summary>
        /// <param name="modelBuilder">Об'єкт, який використовується для побудови моделі.</param>
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

            // Додання шифрування пароля
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasConversion(
                    password => Encrypter.HashPassword(password),
                    hashedPassword => hashedPassword
                );

            modelBuilder.Entity<User>()
                .Property(u => u.ConfirmPassword)
                .HasConversion(
                    password => Encrypter.HashPassword(password),
                    hashedPassword => hashedPassword
                );

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Заповнює базу даних початковими даними.
        /// </summary>
        public void FillDB()
        {
            DbInitializer.Initialize(this);
        }
    }
}

