using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;

namespace KursovaWork.Services
{
    public class Encrypter
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("ojvafou1najfvsiu84IvnA42vhiOsv3M"); // Replace with your own encryption key
        // Допоміжні методи для шифрування / дешифрування номера кредитної картки
        public static string Encrypt(string value)
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

        public static string Decrypt(string encryptedValue)
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

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static string EncryptMonth(string month)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.GenerateIV();

                byte[] encrypted;

                using (var encryptor = aes.CreateEncryptor())
                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(aes.IV, 0, aes.IV.Length);

                    byte[] monthBytes = Encoding.UTF8.GetBytes(month);

                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(monthBytes, 0, monthBytes.Length);
                    }

                    encrypted = memoryStream.ToArray();
                }

                return Convert.ToBase64String(encrypted);
            }
        }

        public static string DecryptMonth(string encryptedMonth)
        {
            byte[] encrypted = Convert.FromBase64String(encryptedMonth);

            using (var aes = Aes.Create())
            {
                aes.Key = Key;

                byte[] iv = new byte[aes.IV.Length];
                Buffer.BlockCopy(encrypted, 0, iv, 0, iv.Length);

                byte[] monthBytes;

                using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                using (var memoryStream = new MemoryStream(encrypted, iv.Length, encrypted.Length - iv.Length))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    monthBytes = new byte[memoryStream.Length];
                    cryptoStream.Read(monthBytes, 0, monthBytes.Length);
                }

                return Encoding.UTF8.GetString(monthBytes).Substring(0, 2);
            }
        }

        public static string EncryptYear(string year)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.GenerateIV();

                byte[] encrypted;

                using (var encryptor = aes.CreateEncryptor())
                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(aes.IV, 0, aes.IV.Length);

                    byte[] yearBytes = Encoding.UTF8.GetBytes(year);

                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(yearBytes, 0, yearBytes.Length);
                    }

                    encrypted = memoryStream.ToArray();
                }

                return Convert.ToBase64String(encrypted);
            }
        }

        public static string DecryptYear(string encryptedYear)
        {
            byte[] encrypted = Convert.FromBase64String(encryptedYear);

            using (var aes = Aes.Create())
            {
                aes.Key = Key;

                byte[] iv = new byte[aes.IV.Length];
                Buffer.BlockCopy(encrypted, 0, iv, 0, iv.Length);

                byte[] yearBytes;

                using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                using (var memoryStream = new MemoryStream(encrypted, iv.Length, encrypted.Length - iv.Length))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    yearBytes = new byte[memoryStream.Length];
                    cryptoStream.Read(yearBytes, 0, yearBytes.Length);
                }

                return Encoding.UTF8.GetString(yearBytes).Substring(0, 2);
            }
        }

        public static string EncryptCVV(string cvv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.GenerateIV();

                byte[] encrypted;

                using (var encryptor = aes.CreateEncryptor())
                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(aes.IV, 0, aes.IV.Length);

                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(cvv);
                    }

                    encrypted = memoryStream.ToArray();
                }

                return Convert.ToBase64String(encrypted);
            }
        }

        public static string DecryptCVV(string encryptedCVV)
        {
            byte[] encrypted = Convert.FromBase64String(encryptedCVV);

            using (var aes = Aes.Create())
            {
                aes.Key = Key;

                byte[] iv = new byte[aes.IV.Length];
                Buffer.BlockCopy(encrypted, 0, iv, 0, iv.Length);

                byte[] cvvBytes;

                using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                using (var memoryStream = new MemoryStream(encrypted, iv.Length, encrypted.Length - iv.Length))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    string cvv = streamReader.ReadToEnd();
                    cvvBytes = Encoding.UTF8.GetBytes(cvv);
                }

                return Encoding.UTF8.GetString(cvvBytes);
            }
        }
    }
}
