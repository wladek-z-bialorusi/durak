using Durak.Logic.Enums;
using Durak.Logic.Models;

namespace Durak.Logic
{
    /// <summary>
    /// Contains logic for putting cards on the table.
    /// </summary>
    public class PlayTable
    {
        private List<Card> _cards;

        public PlayTable(CardSuit cardSuit)
        {
            Trump = cardSuit;
            _cards = new List<Card>();
        }

        public CardSuit Trump { get; }

        public IEnumerable<Card> Cards
        {
            get
            {
                foreach (var card in _cards)
                {
                    yield return card;
                }
            }
        }

        /// <summary>
        /// Add a new card to the table.
        /// It can be attack card (number of cards is even) or defend card (number of cards is odd).
        /// </summary>
        /// <param name="card">Card to add.</param>
        /// <returns>
        /// In case of attack card: false if cannot add this card (card doesn't match ny of cards on the table).
        /// In case of defend card: false if cannot use this card for defend (card is weaker that last card).
        /// </returns>
        public bool AddCard(Card card)
        {
            return _cards.Count % 2 == 0 ? Attack(card) : Defend(card);
        }

        public void RemoveAllCards()
        {
            _cards.Clear();
        }

        private bool Attack(Card card)
        {
            if (_cards.Count == 0 ||
                Cards.Any(c => c.CardNumber == card.CardNumber))
            {
                _cards.Add(card);
                return true;
            }

            return false;
        }

        private bool Defend(Card card)
        {
            if (_cards[^1].CardSuit != Trump)
            {
                if (card.CardNumber > _cards[^1].CardNumber || card.CardSuit == Trump)
                {
                    _cards.Add(card);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (card.CardSuit == Trump && card.CardNumber > _cards[^1].CardNumber)
                {
                    _cards.Add(card);
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}