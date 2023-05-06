using KursovaWork.Entity.Car;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;

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
            
            // Додання валідації кредитної карти
            modelBuilder.Entity<Card>()
                .Property(o => o.CardNumber)
                .HasConversion(
                    card => Encrypt(card),
                    encryptedCard => Decrypt(encryptedCard)
                );

            base.OnModelCreating(modelBuilder);
        }

        
        // Допоміжні методи для шифрування / дешифрування номера кредитної картки
        private static string Encrypt(string value)
        {
            // Генеруємо випадковий ключ шифрування
            byte[] key = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            // Генеруємо випадковий вектор ініціалізації
            byte[] iv = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }

            // Конвертуємо рядок в масив байтів
            byte[] plaintext = Encoding.UTF8.GetBytes(value);

            // Шифруємо текстові дані використовуючи AES шифрування з випадковим ключем та вектором ініціалізації
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(plaintext, 0, plaintext.Length);
                    }

                    // Об'єднуємо зашифровані байти з ключем та вектором ініціалізації у один масив байтів
                    byte[] encrypted = ms.ToArray();
                    byte[] result = new byte[key.Length + iv.Length + encrypted.Length];
                    Buffer.BlockCopy(key, 0, result, 0, key.Length);
                    Buffer.BlockCopy(iv, 0, result, key.Length, iv.Length);
                    Buffer.BlockCopy(encrypted, 0, result, key.Length + iv.Length, encrypted.Length);

                    // Конвертуємо результат у base64 строку та повертаємо її
                    return Convert.ToBase64String(result);
                }
            }
        }

        private static string Decrypt(string encryptedValue)
        {
            // Розбиваємо отримане значення на ключ, вектор ініціалізації та зашифрований текст
            byte[] result = Convert.FromBase64String(encryptedValue);
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            byte[] encrypted = new byte[result.Length - key.Length - iv.Length];
            Buffer.BlockCopy(result, 0, key, 0, key.Length);
            Buffer.BlockCopy(result, key.Length, iv, 0, iv.Length);
            Buffer.BlockCopy(result, key.Length + iv.Length, encrypted, 0, encrypted.Length);
            // Розшифровуємо зашифрований текст з ключем і вектором ініціалізації
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new MemoryStream(encrypted))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs, Encoding.UTF8))
                {
                    // Повертаємо розшифрований текст
                    return reader.ReadToEnd();
                }
            }

        }
    }
}

