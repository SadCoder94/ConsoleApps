using MetroApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp.DataRepo
{
    public interface ICardInfoRepo
    {
        void AddCardBalance(string cardNo, int amount);
        void DeductCardBalance(string cardNo, int amount);
        int GetCardBalanceByCardNumber(string cardNo);
        void LoadCardInfo(Card card);
    }

    public class CardInfoRepo : ICardInfoRepo
    {
        private List<Card> _cards;
        public CardInfoRepo()
        {
            _cards = new List<Card>();
        }

        public void LoadCardInfo(Card card)
        {
            _cards.Add(card);
        }

        public int GetCardBalanceByCardNumber(string cardNo)
        {
            return _cards.Find(x => x.CardNumber == cardNo).CardBalance;
        }

        public void AddCardBalance(string cardNo, int amount)
        {
            var cardToRecharge = _cards.Find(x => x.CardNumber == cardNo);
            cardToRecharge.CardBalance += amount;
        }

        public void DeductCardBalance(string cardNo, int amount)
        {
            var cardToRecharge = _cards.Find(x => x.CardNumber == cardNo);
            cardToRecharge.CardBalance -= amount;
        }
    }
}
