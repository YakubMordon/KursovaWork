using System.Security.Cryptography;
using System.Text;

namespace KursovaWork.Services.AdditionalServices
{
    /// <summary>
    /// Клас, який забезпечує функції шифрування та дешифрування даних.
    /// </summary>
    public class Encrypter
    {
        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        /// 
        private static readonly ILogger _logger = LoggerFactory.Create(builder => builder.AddConsole())
            .CreateLogger(typeof(Encrypter));

        /// <summary>
        /// Ключ для шифрування та дешифрування року та місяця
        /// </summary>
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("ojvafou1najfvsiu84IvnA42vhiOsv3M");

        /// <summary>
        /// Метод для шифрування номеру банківської карти.
        /// </summary>
        /// <param name="value">Номер банківської карти, який потрібно зашифрувати.</param>
        /// <returns>Зашифрований номер банківської карти.</returns>
        public static string Encrypt(string value)
        {
            _logger.LogInformation("Генеруємо випадковий ключ шифрування");
            byte[] key = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            _logger.LogInformation("Генеруємо випадковий вектор ініціалізації");
            byte[] iv = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }

            _logger.LogInformation("Конвертуємо рядок в масив байтів");
            byte[] plaintext = Encoding.UTF8.GetBytes(value);

            _logger.LogInformation("Шифруємо текстові дані використовуючи AES шифрування з випадковим ключем та вектором ініціалізації");
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

                    _logger.LogInformation("Об'єднуємо зашифровані байти з ключем та вектором ініціалізації у один масив байтів");
                    byte[] encrypted = ms.ToArray();
                    byte[] result = new byte[key.Length + iv.Length + encrypted.Length];
                    Buffer.BlockCopy(key, 0, result, 0, key.Length);
                    Buffer.BlockCopy(iv, 0, result, key.Length, iv.Length);
                    Buffer.BlockCopy(encrypted, 0, result, key.Length + iv.Length, encrypted.Length);

                    _logger.LogInformation("Конвертуємо результат у base64 строку та повертаємо її");
                    return Convert.ToBase64String(result);
                }
            }
        }

        /// <summary>
        /// Метод для дешифрування номеру банківської карти.
        /// </summary>
        /// <param name="encryptedValue">Зашифрований номер банківської карти.</param>
        /// <returns>Розшифрований номер банківської карти.</returns>
        public static string Decrypt(string encryptedValue)
        {
            _logger.LogInformation("Розбиваємо отримане значення на ключ, вектор ініціалізації та зашифрований текст");
            byte[] result = Convert.FromBase64String(encryptedValue);
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            byte[] encrypted = new byte[result.Length - key.Length - iv.Length];
            Buffer.BlockCopy(result, 0, key, 0, key.Length);
            Buffer.BlockCopy(result, key.Length, iv, 0, iv.Length);
            Buffer.BlockCopy(result, key.Length + iv.Length, encrypted, 0, encrypted.Length);
            _logger.LogInformation("Розшифровуємо зашифрований текст з ключем і вектором ініціалізації");
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new MemoryStream(encrypted))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs, Encoding.UTF8))
                {
                    _logger.LogInformation("Повертаємо розшифрований текст");
                    return reader.ReadToEnd();
                }
            }

        }

        /// <summary>
        /// Метод для хешування пароля.
        /// </summary>
        /// <param name="password">Пароль, який потрібно захешувати.</param>
        /// <returns>Хешоване значення пароля.</returns>
        public static string HashPassword(string password)
        {
            _logger.LogInformation("Хешуємо пароль");
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                _logger.LogInformation("Повертаємо захешований пароль");
                return Convert.ToBase64String(hashedBytes);
            }
        }

        /// <summary>
        /// Метод для шифрування місяця.
        /// </summary>
        /// <param name="month">Місяць, який потрібно зашифрувати.</param>
        /// <returns>Зашифрований місяць.</returns>
        public static string EncryptMonth(string month)
        {
            _logger.LogInformation("Шифруємо місяць");
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
                _logger.LogInformation("Повертаємо зашифрований місяць");
                return Convert.ToBase64String(encrypted);
            }
        }

        /// <summary>
        /// Метод для дешифрування місяця.
        /// </summary>
        /// <param name="encryptedMonth">Зашифрований місяць.</param>
        /// <returns>Розшифрований місяць.</returns>
        public static string DecryptMonth(string encryptedMonth)
        {
            _logger.LogInformation("Дешифруємо місяць");
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
                _logger.LogInformation("Повертаємо дешифрований місяць");
                return Encoding.UTF8.GetString(monthBytes).Substring(0, 2);
            }
        }

        /// <summary>
        /// Метод для шифрування року.
        /// </summary>
        /// <param name="year">Рік, який потрібно зашифрувати.</param>
        /// <returns>Зашифрований рік.</returns>
        public static string EncryptYear(string year)
        {
            _logger.LogInformation("Шифруємо рік");
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

                _logger.LogInformation("Повертаємо зашифрований рік");
                return Convert.ToBase64String(encrypted);
            }
        }

        /// <summary>
        /// Метод для дешифрування року.
        /// </summary>
        /// <param name="encryptedYear">Зашифрований рік.</param>
        /// <returns>Розшифрований рік.</returns>
        public static string DecryptYear(string encryptedYear)
        {
            _logger.LogInformation("Дешифруємо рік");
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

                _logger.LogInformation("Повертаємо дешифрований рік");
                return Encoding.UTF8.GetString(yearBytes).Substring(0, 2);
            }
        }

        /// <summary>
        /// Метод для шифрування CVV.
        /// </summary>
        /// <param name="cvv">CVV, який потрібно зашифрувати.</param>
        /// <returns>Зашифрований CVV.</returns>
        public static string EncryptCVV(string cvv)
        {
            _logger.LogInformation("Шифруємо CVV");
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

                _logger.LogInformation("Повертаємо зашифрований CVV");
                return Convert.ToBase64String(encrypted);
            }
        }

        /// <summary>
        /// Метод для дешифрування CVV.
        /// </summary>
        /// <param name="encryptedCVV">Зашифрований CVV.</param>
        /// <returns>Розшифрований CVV.</returns>
        public static string DecryptCVV(string encryptedCVV)
        {
            _logger.LogInformation("Дешифруємо CVV");
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

                _logger.LogInformation("Повертаємо дешифрований CVV");
                return Encoding.UTF8.GetString(cvvBytes);
            }
        }
    }
}
