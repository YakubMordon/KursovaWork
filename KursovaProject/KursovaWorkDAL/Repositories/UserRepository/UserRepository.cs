using KursovaWorkDAL.Entity;
using KursovaWorkDAL.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace KursovaWorkDAL.Repositories.UserRepository
{
    /// <summary>
    /// Імплементація інтерфейсу для обробки запитів зв'язаних з користувачем
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Контекст для роботи з базою даних
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="context">Контекст для роботи з базою даних</param>
        public UserRepository(CarSaleContext context)
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }
        public IEnumerable<User> GetAll()
        {
           return _context.Users
                .Include(u => u.CreditCard);
        }
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
