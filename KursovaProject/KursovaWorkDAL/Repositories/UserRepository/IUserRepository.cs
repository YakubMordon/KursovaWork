using KursovaWorkDAL.Entity.Entities;

namespace KursovaWorkDAL.Repositories.UserRepository
{
    /// <summary>
    /// Інтерфейс для обробки запитів зв'язаних з користувачем
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Метод получення користувача за його ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор користувача</param>
        /// <returns>Користувач</returns>
        User GetById(int id);

        /// <summary>
        /// Метод получення користувача за його електронною поштою
        /// </summary>
        /// <param name="email">Електронна пошта</param>
        /// <returns>Користувач</returns>
        User GetByEmail(string email);

        /// <summary>
        /// Метод додання користувача до бази даних
        /// </summary>
        /// <param name="user">Користувач</param>
        void Add(User user);

        /// <summary>
        /// Метод оновлення інформації про користувача в базі даних
        /// </summary>
        /// <param name="user">Користувач</param>
        void Update(User user);

        /// <summary>
        /// Метод видалення користувача з бази даних
        /// </summary>
        /// <param name="user">Користувач</param>
        void Delete(User user);

        /// <summary>
        /// Метод заполучення списку всіх користувачів
        /// </summary>
        /// <returns>Список всіх користувачів</returns>
        IEnumerable<User> GetAll();
    }
}
