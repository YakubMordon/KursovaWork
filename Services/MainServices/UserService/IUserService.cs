using KursovaWork.Entity.Entities;
using KursovaWork.Models;

namespace KursovaWork.Services.MainServices.UserService
{
    /// <summary>
    /// Інтерфейс для бізнес-логіки зв'язаної з користувачами
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Метод получення користувача за його ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор користувача</param>
        /// <returns>Користувач</returns>
        User GetUserById(int id);

        /// <summary>
        /// Метод получення користувача за його електронною поштою
        /// </summary>
        /// <param name="email">Електронна пошта</param>
        /// <returns>Користувач</returns>
        User GetUserByEmail(string email);

        /// <summary>
        /// Метод получення користувача, який є залогіненим на даний момент
        /// </summary>
        /// <returns>Користувач</returns>
        User GetLoggedInUser();

        /// <summary>
        /// Метод додання нового користувача у базу даних
        /// </summary>
        /// <param name="user">Користувач</param>
        void AddUser(User user);

        /// <summary>
        /// Метод оновлення інформації про користувача в базі даних
        /// </summary>
        /// <param name="user">Користувач</param>
        void UpdateUser(User user);

        /// <summary>
        /// Метод оновлення паролей користувача в базі даних
        /// </summary>
        /// <param name="model">Модель, в якій знаходяться нові паролі користувача</param>
        /// <param name="user">Користувач</param>
        void UpdatePasswordOfUser(ChangePasswordViewModel model, User user);

        /// <summary>
        /// Метод видалення користувача з бази даних
        /// </summary>
        /// <param name="user">Користувач</param>
        void DeleteUser(User user);

        /// <summary>
        /// Метод заполучення всіх користувачів
        /// </summary>
        /// <returns>Список користувачів</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Метод валідації користувача
        /// </summary>
        /// <param name="model">Модель, яка містить в собі логін та пароль</param>
        /// <returns>Повертає користувача, якщо пароль і логін такі ж, в інакшому випадку null</returns>
        User ValidateUser(LogInViewModel model);
    }
}
