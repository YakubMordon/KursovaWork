using KursovaWorkDAL.Entity.Entities;
using KursovaWorkDAL.Entity.Entities.Car;

namespace KursovaWorkBLL.Services.MainServices.OrderService
{
    /// <summary>
    /// Інтерфейс для бізнес-логіки зв'язаної з замовленнями
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Метод отримання замовлення за ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор замовлення</param>
        /// <returns>Замовлення</returns>
        Order GetOrderById(int id);

        /// <summary>
        /// Метод додавання замовлення до бази даних
        /// </summary>
        /// <param name="order">Замовлення</param>
        void AddOrder(Order order);

        /// <summary>
        /// Метод додавання замовлення до бази даних для залогіненого користувача
        /// </summary>
        /// <param name="_curCar">Машина, яку вибрав користувач</param>
        /// <param name="configurator">Конфігурація автомобіля, якщо його обрали через конфігуратор</param>
        /// <returns>Номер замовлення</returns>
        int AddOrderLoggedIn(CarInfo _curCar, ConfiguratorOptions? configurator);

        /// <summary>
        /// Метод оновлення замовлення в базі даних
        /// </summary>
        /// <param name="order">Замовлення</param>
        void UpdateOrder(Order order);

        /// <summary>
        /// Метод видалення замовлення з бази даних
        /// </summary>
        /// <param name="order">Замовлення</param>
        void DeleteOrder(Order order);

        /// <summary>
        /// Метод отримання всіх замовлень
        /// </summary>
        /// <returns>Список всіх замовлень</returns>
        IEnumerable<Order> GetAll();

        /// <summary>
        /// Метод отримання замовлень, які пов'язані з користувачем за певним ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор користувача</param>
        /// <returns>Список замовлень</returns>
        IEnumerable<Order> FindAll(int id);

        /// <summary>
        /// Метод отримання замовлень, які пов'язані з користувачем, який є залогіненим
        /// </summary>
        /// <returns>Список замовлень</returns>
        IEnumerable<Order> FindAllLoggedIn(); 
    }
}
