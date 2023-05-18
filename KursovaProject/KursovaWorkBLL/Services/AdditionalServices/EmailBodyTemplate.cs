namespace KursovaWorkBLL.Services.AdditionalServices
{
    /// <summary>
    /// Клас, який надає шаблон для створення HTML-тіла електронного листа.
    /// </summary>
    public static class EmailBodyTemplate
    {
        /// <summary>
        /// Створює HTML-тіло електронного листа з використанням заданих даних.
        /// </summary>
        /// <param name="FirstName">Ім'я отримувача.</param>
        /// <param name="LastName">Прізвище отримувача.</param>
        /// <param name="verificationCode">Код підтвердження.</param>
        /// <param name="purpose">Призначення підтвердження.</param>
        /// <returns>HTML-тіло електронного листа.</returns>
        public static string BodyTemp(string FirstName, string LastName, int verificationCode, string purpose)
        {
            return $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            font-size: 14px;
                        }}
                    </style>
                </head>
                <body>
                    <h2>Шановний(а) {FirstName} {LastName},</h2>
                    <p>Ваш код підтвердження: <strong>{verificationCode}</strong></p>
                    <p>Будь ласка, використовуйте цей код для підтвердження вашої {purpose}.</p>
                    <p>Якщо у вас виникнуть будь-які питання або потреба у додатковій інформації, будь ласка, зв'яжіться з нашою службою підтримки.</p>
                    <p>Дякуємо за вашу довіру!</p>
                    <p>З повагою,</p>
                    <p>VAG Dealer</p>
                </body>
                </html>";
        }

        /// <summary>
        /// Створює HTML-тіло електронного листа з використанням заданих даних для замовлення.
        /// </summary>
        /// <param name="userName">Ім'я та прізвище користувача</param>
        /// <param name="Make">Марка автомобіля</param>
        /// <param name="Model">Модель автомобіля</param>
        /// <param name="Year">Рік виробництва автомобіля</param>
        /// <returns>HTML-тіло електронного листа.</returns>
        public static string OrderBodyTemp(string userName, string Make, string Model, int Year)
        {
            return $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            font-size: 14px;
                        }}
                    </style>
                </head>
                <body>
                    <h2>Шановний(а) {userName},</h2>
                    <p>Дякуємо за вашу покупку!</p>
                    <p>Ви придбали новий автомобіль {Make} {Model}, {Year} року виробництва.</p>
                    <p>Деталі вашого замовлення:</p>
                    <ul>
                        <li>Марка: {Make}</li>
                        <li>Модель: {Model}</li>
                        <li>Рік виробництва: {Year} рік</li>
                    </ul>
                    <p>Додаткова інформація про замовлення знаходиться у нас на сайті в вашому особистому кабінеті</p>
                    <p>Якщо у вас виникнуть будь-які питання або потреба у додатковій інформації, будь ласка, зв'яжіться з нашою службою підтримки.</p>
                    <p>Дякуємо за вашу довіру!</p>
                    <p>З повагою,</p>
                    <p>VAG Dealer</p>
                </body>
                </html>";
        }
    }
}
