using KursovaWorkDAL.Entity;
using KursovaWorkDAL.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace KursovaWorkDAL.Repositories.OrderRepository
{
    /// <summary>
    /// Імплементація інтерфейсу для обробки запитів зв'язаних з замовленнями
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// Контекст для роботи з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="OrderRepository"/>.
        /// </summary>
        /// <param name="context">Контекст для роботи з бд</param>
        public OrderRepository(CarSaleContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
        public Order GetById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }
        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        public IEnumerable<Order> FindAll(int id)
        {
            return _context.Orders
                .Include(o => o.Car)
                    .ThenInclude(c => c.Detail)
                .Include(o => o.ConfiguratorOptions)
                .Where(o => o.UserId == id);
        }
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders
               .Include(o => o.Car)
                   .ThenInclude(c => c.Detail)
               .Include(o => o.ConfiguratorOptions);
        }
    }
}
