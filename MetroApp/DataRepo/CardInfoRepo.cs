using MetroApp.Models;
using System;
using System.Collections.Generic;

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

        private Card GetCardIfExists(string cardNo)
        {
            var card = _cards.Find(x => x.CardNumber == cardNo);
            return card == null ? throw new CardNotFoundException() : card;
        }

        public int GetCardBalanceByCardNumber(string cardNo)
        {
            var card = GetCardIfExists(cardNo);
            return card.CardBalance;
        }


        public void AddCardBalance(string cardNo, int amount)
        {
            var cardToRecharge = GetCardIfExists(cardNo);
            cardToRecharge.CardBalance += amount;
        }

        public void DeductCardBalance(string cardNo, int amount)
        {
            var cardToRecharge = GetCardIfExists(cardNo);
            cardToRecharge.CardBalance -= amount;
        }
    }

    public class CardNotFoundException: Exception
    {

    }
}
