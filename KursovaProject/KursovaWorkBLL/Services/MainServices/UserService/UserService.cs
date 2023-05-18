using KursovaWorkDAL.Entity.Entities;
using KursovaWorkDAL.Entity.Service;
using KursovaWorkDAL.Repositories.UserRepository;
using KursovaWorkBLL.Services.AdditionalServices;
using Microsoft.Extensions.Logging;

namespace KursovaWorkBLL.Services.MainServices.UserService
{
    /// <summary>
    /// Імплементація інтерфейсу IUserService для бізнес-логіки зв'язаної з користувачами
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Репозиторій користувачів, завдяки якому працюємо з бд
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Об'єкт класу IDRetriever для получення ідентифікатора користувача
        /// </summary>
        private readonly IDRetriever _idRetriever;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="UserService"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторій користувачів.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        /// <param name="idRetriever">Сервіс для отримання ідентифікатора користувача.</param>
        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IDRetriever idRetriever)
        {
            _userRepository = userRepository;
            _logger = logger;
            _idRetriever = idRetriever;
        }
        public void AddUser(User user)
        {
            _userRepository.Add(user);
            _logger.LogInformation("Користувача успішно додано");
        }
        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
            _logger.LogInformation("Користувача успішно видалено");
        }
        public IEnumerable<User> GetAllUsers()
        {
            _logger.LogInformation("Список користувачів успішно получено");
            return _userRepository.GetAll();
        }
        public User GetLoggedInUser()
        {
            _logger.LogInformation($"Успішно получено ідентифікатор користувача");
            int id = _idRetriever.GetLoggedInUserId();

            _logger.LogInformation("Залогіненого користувача за його ідентифікатором успішно знайдено");
            return _userRepository.GetById(id);
        }
        public User GetUserById(int id)
        {
            _logger.LogInformation("Користувача за його ідентифікатором успішно знайдено");
            return _userRepository.GetById(id);
        }
        public User GetUserByEmail(string email)
        {
            _logger.LogInformation("Користувача за його поштою успішно знайдено");
            return _userRepository.GetByEmail(email);
        }
        public void UpdatePasswordOfUser(string password, string confirmPassword, User user)
        {
            user.Password = password;
            user.ConfirmPassword = confirmPassword;

            _userRepository.Update(user);
            _logger.LogInformation("Пароль користувача успішно оновлено");
        }
        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
            _logger.LogInformation("Дані користувача успішно оновлені");
        }
        public User ValidateUser(string email, string password)
        {
            var users = _userRepository.GetAll();   
            string hashPassword = Encrypter.HashPassword(password);

            foreach (User user in users)
            {
                if (user.Email == email && user.Password == hashPassword)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
