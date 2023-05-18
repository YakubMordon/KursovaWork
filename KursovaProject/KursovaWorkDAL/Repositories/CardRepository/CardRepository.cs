using KursovaWorkDAL.Entity;
using KursovaWorkDAL.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace KursovaWorkDAL.Repositories.CardRepository
{
    /// <summary>
    /// Імплементація інтерфейсу ICardRepository для обробки запитів зв'язаних з картами
    /// </summary>
    public class CardRepository : ICardRepository
    {
        /// <summary>
        /// Контекст для роботи з бд
        /// </summary>
        private readonly CarSaleContext _context;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CardRepository"/>.
        /// </summary>
        /// <param name="context">Контекст для роботи з бд</param>
        public CardRepository(CarSaleContext context)
        {
            _context = context;
        }
        public void Add(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
        }
        public void Delete(Card card)
        {
            _context.Cards.Remove(card);
            _context.SaveChanges();
        }
        public IEnumerable<Card> GetAll()
        {
            return _context.Cards;
        }
        public Card GetById(int id)
        {
            return _context.Cards.FirstOrDefault(c => c.UserId == id);
        }
        public bool IsExisting(int id)
        {
            return _context.Cards.Any(u => u.UserId == id);
        }
        public void Update(Card card)
        {
            _context.Cards.Update(card);
            _context.SaveChanges();
        }
    }
}
