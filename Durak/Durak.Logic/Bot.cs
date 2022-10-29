using Durak.Logic.AbstractClasses;
using Durak.Logic.Models;

namespace Durak.Logic
{
    public class Bot : Player
    {
        public Bot(PlayTable playTable, Stack<Card> deck) : base(playTable, deck)
        {
        }

        public override bool Play()
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

        private bool Attack()
        {
            IEnumerable<Card> cardsAllowedForAttack = _playTable.Cards.Any() ?
                _cards.Where(c => _playTable.Cards.Any(pc => pc.CardNumber == c.CardNumber)) :
                _cards;

            if (!cardsAllowedForAttack.Any())
            {
                return false;
            }

            Card cardAttacker = cardsAllowedForAttack.Any(c => c.CardSuit != _playTable.Trump) ?
                cardsAllowedForAttack.Where(c => c.CardSuit != _playTable.Trump).MinBy(c => c.CardNumber) :
                cardsAllowedForAttack.MinBy(c => c.CardNumber);

            bool success = _playTable.AddCard(cardAttacker);
            if (!success)
            {
                throw new InvalidOperationException("Bot's logic allowed an invalid card on the table");
            }

            _cards.Remove(cardAttacker);

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
            _cards.Remove(cardDefender);
            if (!success)
            {
                throw new InvalidOperationException("Bot's logic allowed an invalid card on the table");
            }

            return true;
        }
    }
}
