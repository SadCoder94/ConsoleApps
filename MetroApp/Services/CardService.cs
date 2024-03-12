using MetroApp.DataRepo;
using MetroApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp.Services
{
    public interface ICardService
    {
        int GetCardBalanceByCardNumber(string cardNo);
        void SetCardInfo(string cardNo, int balance);
        void AddCardBalance(string cardNo, int amount);
        void DeductCardBalance(string cardNo, int amount);
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

        public void SetCardInfo(string cardNo, int balance)
        {
            _cardInfoRepo.LoadCardInfo(new Card { CardNumber = cardNo, CardBalance = balance });
        }

        public void AddCardBalance(string cardNo, int amount)
        {
            _cardInfoRepo.AddCardBalance(cardNo, amount);
        }

        public void DeductCardBalance(string cardNo, int amount)
        {
            _cardInfoRepo.DeductCardBalance(cardNo, amount);
        }
    }
}
