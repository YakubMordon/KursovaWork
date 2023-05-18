using KursovaWorkDAL.Entity.Entities;

namespace KursovaWorkDAL.Repositories.OrderRepository
{
    /// <summary>
    /// Інтерфейс для обробки запитів зв'язаних з замовленнями
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Метод получення замовлення по ідентифікатору
        /// </summary>
        /// <param name="id">Ідентифікатор замовлення</param>
        /// <returns>Замовлення</returns>
        Order GetById(int id);

        /// <summary>
        /// Метод добавлення замовлення в базу даних
        /// </summary>
        /// <param name="order">Замовлення</param>
        void Add(Order order);

        /// <summary>
        /// Метод оновлення замовлення в базі даних
        /// </summary>
        /// <param name="order">Замовлення</param>
        void Update(Order order);

        /// <summary>
        /// Метод видалення замовлення з бази даних
        /// </summary>
        /// <param name="order">Замовлення</param>
        void Delete(Order order);

        /// <summary>
        /// Метод получення всіх можливих замовлень
        /// </summary>
        /// <returns>Список всіх можливих замовлень</returns>
        IEnumerable<Order> GetAll();

        /// <summary>
        /// Метод получення замовлень, які є прив'язані до користувача з певним ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор користувача</param>
        /// <returns>Список замовлень</returns>
        IEnumerable<Order> FindAll(int id);
    }
}
