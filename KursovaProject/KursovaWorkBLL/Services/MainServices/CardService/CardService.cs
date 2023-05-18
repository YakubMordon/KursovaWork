using KursovaWorkDAL.Entity.Entities;
using KursovaWorkDAL.Repositories.CardRepository;
using KursovaWorkBLL.Services.AdditionalServices;
using Microsoft.Extensions.Logging;

namespace KursovaWorkBLL.Services.MainServices.CardService
{
    /// <summary>
    /// Імплементація інтерфейсу ICardService для бізнес-логіки зв'язаної з картами
    /// </summary>
    public class CardService : ICardService
    {

        /// <summary>
        /// Репозиторій кредитних карток, завдяки якому працюємо з бд
        /// </summary>
        private readonly ICardRepository _cardRepository;

        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private readonly ILogger<CardService> _logger;

        /// <summary>
        /// Об'єкт класу IDRetriever для получення ідентифікатора користувача
        /// </summary>
        private readonly IDRetriever _idRetriever;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CardService"/>.
        /// </summary>
        /// <param name="cardRepository">Репозиторій кредитних карток.</param>
        /// <param name="logger">Логгер для запису логів.</param>
        /// <param name="idRetriever">Сервіс для отримання ідентифікатора користувача.</param>
        public CardService(ICardRepository cardRepository, ILogger<CardService> logger, IDRetriever idRetriever)
        {
            _cardRepository = cardRepository;
            _logger = logger;
            _idRetriever = idRetriever;
        }
        public Card GetById(int id)
        {
            _logger.LogInformation("Заполучення методу оплати певного користувача");
            return _cardRepository.GetById(id);
        }
        public Card GetByLoggedInUser()
        {
            _logger.LogInformation("Заполучення ідентифікатора користувача");
            int loggedInUserId = _idRetriever.GetLoggedInUserId();

            _logger.LogInformation("Заполучення методу оплати залогіненого користувача");
            return _cardRepository.GetById(loggedInUserId);
        }   
        public void AddCard(Card card)
        {
            _logger.LogInformation("Заполучення ідентифікатора користувача");
            int loggedInUserId = _idRetriever.GetLoggedInUserId();

            card.UserId = loggedInUserId;
            
            _cardRepository.Add(card);

            _logger.LogInformation("Метод оплати було успішно додано");
        }
        public void UpdateCard(Card card)
        {
            _cardRepository.Update(card);
            _logger.LogInformation("Метод оплати було успішно оновлено");
        }
        public void DeleteCard()
        {
            _logger.LogInformation("Заполучення ідентифікатора");
            int loggedInUserId = _idRetriever.GetLoggedInUserId();

            _logger.LogInformation("Пошук методу оплати в базі даних");
            var card = _cardRepository.GetById(loggedInUserId);
            if (card != null)
            {
                _cardRepository.Delete(card);
                _logger.LogInformation("Метод оплати було успішно видалено");
            }
        }
        public IEnumerable<Card> GetAllCards()
        {
            _logger.LogInformation("Заполучення всіх методів оплати");
            return _cardRepository.GetAll();
        }
        public bool CardExists()
        {
            _logger.LogInformation("Заполучення ідентифікатора користувача");
            int loggedInUserId = _idRetriever.GetLoggedInUserId();

            _logger.LogInformation("Перевірка чи у користувача є підключений метод оплати");
            return _cardRepository.IsExisting(loggedInUserId);
        }
    }
}
