using MetroApp.DataRepo;
using MetroApp.Models;

namespace MetroApp.Services
{
    public interface ICardService
    {
        int GetCardBalanceByCardNumber(string cardNo);
        Card SetCardInfo(string cardNo, int balance);
        Card AddCardBalance(string cardNo, int amount);
        Card DeductCardBalance(string cardNo, int amount);
    }

    public class CardService : ICardService
    {
        private readonly ICardInfoRepo _cardInfoRepo;
        public CardService(ICardInfoRepo cardInfoRepo)
        {
            _cardInfoRepo = cardInfoRepo;
        }

        public int GetCardBalanceByCardNumber(string cardNo)
        {
            return _cardInfoRepo.GetCardBalanceByCardNumber(cardNo);
        }

        public Card SetCardInfo(string cardNo, int balance)
        {
            var cardInfo = new Card { CardNumber = cardNo, CardBalance = balance };
            _cardInfoRepo.LoadCardInfo(cardInfo);

            return cardInfo;
        }

        public Card AddCardBalance(string cardNo, int amount)
        {
            var cardInfo = _cardInfoRepo.AddCardBalance(cardNo, amount);
            return cardInfo;
        }

        public Card DeductCardBalance(string cardNo, int amount)
        {
            var cardInfo = _cardInfoRepo.DeductCardBalance(cardNo, amount);
            return cardInfo;
        }
    }
}
