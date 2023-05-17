namespace KursovaWork.Services
{
    public static class EmailBodyTemplate
    {
        public static string bodyTemp(string FirstName, string LastName, int verificationCode, string purpose)
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
    }
}
