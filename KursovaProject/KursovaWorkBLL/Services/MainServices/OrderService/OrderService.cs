using KursovaWorkDAL.Entity.Entities;
using KursovaWorkDAL.Entity.Entities.Car;
using KursovaWorkDAL.Repositories.OrderRepository;
using KursovaWorkBLL.Services.AdditionalServices;
using Microsoft.Extensions.Logging;

namespace KursovaWorkBLL.Services.MainServices.OrderService
{
    /// <summary>
    /// Імплементація інтерфейсу IOrderService для бізнес-логіки зв'язаної з замовленнями
    /// </summary>
    public class OrderService : IOrderService
    {
        /// <summary>
        /// Репозиторій замовлень, завдяки якому працюємо з бд
        /// </summary>
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<OrderService> _logger;

        /// <summary>
        /// Об'єкт класу IDRetriever для получення ідентифікатора користувача
        /// </summary>
        private readonly IDRetriever _idRetriever;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="OrderService"/>.
        /// </summary>
        /// <param name="orderRepository">Репозиторій замовлень.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        /// <param name="idRetriever">Сервіс для отримання ідентифікатора користувача.</param>
        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger, IDRetriever idRetriever)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _idRetriever = idRetriever;
        }
        public void AddOrder(Order order)
        {
            _orderRepository.Add(order);
            _logger.LogInformation("Замовлення було успішно додано");
        }
        public int AddOrderLoggedIn(CarInfo _curCar, ConfiguratorOptions? configurator)
        {
            _logger.LogInformation("Заполучення ідентифікатора користувача");
            int loggedInUserId = _idRetriever.GetLoggedInUserId();

            Order order = new Order()
            {
                CarId = _curCar.Id,
                UserId = loggedInUserId,
                Price = _curCar.Price,
                OrderDate = DateTime.Now
            };

            _logger.LogInformation("Створення заказу автомобіля");

            if (configurator != null)
            {
                order.ConfiguratorOptions = configurator;
                _logger.LogInformation("Автомобіль було обрано у конфігураторі");
            }

            _orderRepository.Add(order);
            _logger.LogInformation("Заказ є успішно доданий");

            return order.Id;
        }
        public void DeleteOrder(Order order)
        {
            _orderRepository.Delete(order);
            _logger.LogInformation("Замовлення було успішно видалено");
        }
        public IEnumerable<Order> FindAll(int id)
        {
            _logger.LogInformation("Замовлення, які є прив'язані до користувача були успішно получені");
            return _orderRepository.FindAll(id);
        }
        public IEnumerable<Order> FindAllLoggedIn()
        {
            _logger.LogInformation("Заполучення ідентифікатор залогіненого користувача");
            int loggedInId = _idRetriever.GetLoggedInUserId();

            _logger.LogInformation("Замовлення, які є прив'язані до користувач, який є залогіненим були успішно получені");
            return _orderRepository.FindAll(loggedInId);
        }
        public IEnumerable<Order> GetAll()
        {
            _logger.LogInformation("Всі замовлення були успішно получені");
            return _orderRepository.GetAll();
        }
        public Order GetOrderById(int id)
        {
            _logger.LogInformation("Замовлення за ідентифікатором було успішно получене");
            return _orderRepository.GetById(id);
        }
        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
            _logger.LogInformation("Замовлення було успішно видалено");
        }
    }
}
