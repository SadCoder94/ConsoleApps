using MetroApp.Models;
using System;
using System.Collections.Generic;

namespace MetroApp.DataRepo
{
    public interface ICardInfoRepo
    {
        Card AddCardBalance(string cardNo, int amount);
        Card DeductCardBalance(string cardNo, int amount);
        int GetCardBalanceByCardNumber(string cardNo);
        Card LoadCardInfo(Card card);
    }

    public class CardInfoRepo : ICardInfoRepo
    {
        private List<Card> _cards;
        public CardInfoRepo()
        {
            _cards = new List<Card>();
        }

        public Card LoadCardInfo(Card card)
        {
            _cards.Add(card);
            return card;
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


        public Card AddCardBalance(string cardNo, int amount)
        {
            var cardToRecharge = GetCardIfExists(cardNo);
            cardToRecharge.CardBalance += amount;
            return cardToRecharge;
        }

        public Card DeductCardBalance(string cardNo, int amount)
        {
            var cardInfo = GetCardIfExists(cardNo);
            cardInfo.CardBalance -= amount;
            return cardInfo;
        }
    }

    public class CardNotFoundException: Exception
    {

    }
}
