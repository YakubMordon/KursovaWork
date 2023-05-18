using KursovaWorkDAL.Entity.Entities;

namespace KursovaWorkBLL.Services.MainServices.CardService
{
    /// <summary>
    /// Інтерфейс для бізнес-логіки зв'язаної з картами
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Метод получення даних методу оплати за ідентифікатором користувача
        /// </summary>
        /// <param name="id">Ідентифікатор користувача</param>
        /// <returns>Об'єкт класу Card. Дані про метод оплати користувача</returns>
        Card GetById(int id);

        /// <summary>
        /// Метод получення даних методу оплати залогіненого користувача
        /// </summary>
        /// <returns>Об'єкт класу Card. Дані про метод оплати залогіненого користувача</returns>
        Card GetByLoggedInUser();

        /// <summary>
        /// Метод додавання нового методу оплати до залогіненого користувача
        /// </summary>
        /// <param name="card">Метод оплати</param>
        void AddCard(Card card);

        /// <summary>
        /// Метод оновлення методу оплати
        /// </summary>
        /// <param name="card">Метод оплати</param>
        void UpdateCard(Card card);

        /// <summary>
        /// Метод видалення методу оплати у залогіненого користувача
        /// </summary>
        void DeleteCard();

        /// <summary>
        /// Метод перевірки чи існує підключений метод оплати у залогіненого користувача
        /// </summary>
        /// <returns>True - якщо існує, False - якщо не існує</returns>
        bool CardExists();

        /// <summary>
        /// Метод заполучення всіх можливих методів оплати
        /// </summary>
        /// <returns>Список всіх можливих методів оплати</returns>
        IEnumerable<Card> GetAllCards();
    }
}
