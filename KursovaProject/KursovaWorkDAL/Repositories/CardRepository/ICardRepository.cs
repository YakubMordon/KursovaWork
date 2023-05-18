using KursovaWorkDAL.Entity.Entities;

namespace KursovaWorkDAL.Repositories.CardRepository
{
    /// <summary>
    /// Інтерфейс для обробки запитів зв'язаних з картами
    /// </summary>
    public interface ICardRepository
    {
        /// <summary>
        /// Метод получення методу оплати за ідентифікатором користувача
        /// </summary>
        /// <param name="id">Ідентифікатор користувача</param>
        /// <returns>Об'єкт класу Card. Метод оплати користувача</returns>
        Card GetById(int id);

        /// <summary>
        /// Метод додавання методу оплати в бд
        /// </summary>
        /// <param name="card">Метод оплати</param>
        void Add(Card card);

        /// <summary>
        /// Метод оновлення методу оплати в бд
        /// </summary>
        /// <param name="card">Метод оплати</param>
        void Update(Card card);

        /// <summary>
        /// Метод видалення методу оплати з бд
        /// </summary>
        /// <param name="card">Метод оплати</param>
        void Delete(Card card);

        /// <summary>
        /// Метод перевірки чи існує метод оплати прив'язаний до користувача
        /// </summary>
        /// <param name="id">Ідентифікатор користувача</param>
        /// <returns>При знаходженні вертає True, в іншому випадку False</returns>
        bool IsExisting(int id);

        /// <summary>
        /// Метод получення всіх можливих методів оплати
        /// </summary>
        /// <returns>Список всіх можливих методів оплати</returns>
        IEnumerable<Card> GetAll();
    }
}
