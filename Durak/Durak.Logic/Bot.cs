using Durak.Logic.Models;
using Durek.Shared;

namespace Durak.Logic
{
    public class Bot
    {
        private PlayTable _playTable;
        private List<Card> _cards;
        private Stack<Card> _deck;


        public Bot(PlayTable playTable, Stack<Card> deck)
        {
            _playTable = playTable;
            _cards = new List<Card>();
            _deck = deck;
        }

        public bool Play()
        {
            if (_playTable.Cards.Count() % 2 == 0)
            {
                return Attack();
            }
            else
            {
                return Defend();
            }
        }

        public void TakeCardsFromDeck()
        {
            while(_cards.Count < Constants.NumberOfCards && _deck.Count > 0)
            {
                _cards.Add(_deck.Pop());
            }
        }

        public void TakeCardsFromTable()
        {
            _cards.AddRange(_playTable.Cards);
            _playTable.RemoveAllCards();
        }

        private bool Attack()
        {
            IEnumerable<Card> cardsAllowedForAttack = _playTable.Cards.Any() ?
                _cards.Where(c => _playTable.Cards.Any(pc => pc.CardNumber == c.CardNumber)) :
                _cards;

            if (!cardsAllowedForAttack.Any())
            {
                return false;
            }

            _playTable.AddCard(_cards.Any(c => c.CardSuit != _playTable.Trump) ?
                _cards.Where(c => c.CardSuit != _playTable.Trump).MinBy(c => c.CardNumber) :
                _cards.MinBy(c => c.CardNumber));

            return true;
        }

        private bool Defend()
        {
            Card cardAttacker = _playTable.Cards.Last();
            Card cardDefender = null;

            if (cardAttacker.CardSuit == _playTable.Trump)
            {
                cardDefender = _cards.Where(c => c.CardSuit == _playTable.Trump && c.CardNumber > cardAttacker.CardNumber)
                                     .MinBy(c => c.CardNumber);
            }
            else
            {
                cardDefender = _cards.Any(c => c.CardSuit != _playTable.Trump && c.CardSuit == cardAttacker.CardSuit && c.CardNumber > cardAttacker.CardNumber) ?
                    _cards.Where(c => c.CardSuit != _playTable.Trump && c.CardSuit == cardAttacker.CardSuit && c.CardNumber > cardAttacker.CardNumber)
                          .MinBy(c => c.CardNumber) :
                    _cards.Where(c => c.CardSuit == _playTable.Trump)
                          .MinBy(c => c.CardNumber);
            }

            if (cardDefender == null)
            {
                return false;
            }

            bool success = _playTable.AddCard(cardDefender);
            if (!success)
            {
                throw new InvalidOperationException("Bot's logic allowed an invalid card on the table");
            }

            return true;
        }
    }
}
