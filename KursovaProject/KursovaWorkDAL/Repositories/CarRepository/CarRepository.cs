using KursovaWorkDAL.Entity;
using KursovaWorkDAL.Entity.Entities.Car;
using Microsoft.EntityFrameworkCore;

namespace KursovaWorkDAL.Repositories.CarRepository
{
    /// <summary>
    /// Імплементація інтерфейсу для обробки запитів зв'язаних з автомобілями
    /// </summary>
    public class CarRepository : ICarRepository
    {
        /// <summary>
        /// Контекст для роботи з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CarRepository"/>.
        /// </summary>
        /// <param name="context">Контекст для роботи з бд</param>
        public CarRepository(CarSaleContext context)
        {
            _context = context;
        }
        public void Add(CarInfo car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }
        public void Delete(CarInfo car)
        {
            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
        public IEnumerable<CarInfo> GetAll()
        {
            return _context.Cars
                .Include(c => c.Detail)
                .Include(c => c.Images).ToList();
        }
        public CarInfo GetByCarInfo(string make, string model, int year)
        {
            return _context.Cars
                .Include(c => c.Detail)
                .Include(c => c.Images)
                .FirstOrDefault(c => c.Make == make && c.Model == model && c.Year == year);
        }
        public CarInfo GetById(int id)
        {
            return _context.Cars
                .Include(c => c.Detail)
                .Include(c => c.Images)
                .FirstOrDefault(c => c.Id == id);
        }
        public void Update(CarInfo car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }
    }
}
