using KursovaWorkDAL.Entity.Entities.Car;

namespace KursovaWorkDAL.Repositories.CarRepository
{
    /// <summary>
    /// Інтерфейс для обробки запитів зв'язаних з автомобілями
    /// </summary>
    public interface ICarRepository
    {
        /// <summary>
        /// Метод получення інформації автомобіля за його ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор автомобіля</param>
        /// <returns>Інформація про автомобіль</returns>
        CarInfo GetById(int id);

        /// <summary>
        /// Метод получення інформації автомобіля за його маркою, моделлю та роком виробнитцва
        /// </summary>
        /// <param name="make">Марка автомобіля</param>
        /// <param name="model">Модель автомобіля</param>
        /// <param name="year">Рік виробництва автомобіля</param>
        /// <returns>Інформація про автомобіль</returns>
        CarInfo GetByCarInfo(string make, string model, int year);

        /// <summary>
        /// Метод додавання інформації про автомобіль до бази даних
        /// </summary>
        /// <param name="car">Інформація про автомобіль</param>
        void Add(CarInfo car);

        /// <summary>
        /// Метод оновлення інформації про автомобіль у базі даних
        /// </summary>
        /// <param name="car">Інформація про автомобіль</param>
        void Update(CarInfo car);

        /// <summary>
        /// Метод видалення інформації про автомобіль з бази даних
        /// </summary>
        /// <param name="car">Інформація про автомобіль</param>
        void Delete(CarInfo car);

        /// <summary>
        /// Метод заполучення списку інформації про всі автомобілі
        /// </summary>
        /// <returns>Список інформації про всі автомобілі</returns>
        IEnumerable<CarInfo> GetAll();
    }
}
