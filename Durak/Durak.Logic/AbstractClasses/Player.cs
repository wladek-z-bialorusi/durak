using Durak.Logic.Models;
using Durak.Shared;

namespace Durak.Logic.AbstractClasses
{
    public abstract class Player
    {
        protected PlayTable _playTable;
        protected List<Card> _cards;
        protected Stack<Card> _deck;

        public Player(PlayTable playTable, Stack<Card> deck)
        {
            _playTable = playTable;
            _cards = new List<Card>();
            _deck = deck;
        }

        public abstract bool Play();

        public void TakeCardsFromDeck()
        {
            while (_cards.Count < Constants.NumberOfCards && _deck.Count > 0)
            {
                _cards.Add(_deck.Pop());
            }
        }

        public void TakeCardsFromTable()
        {
            _cards.AddRange(_playTable.Cards);
            _playTable.RemoveAllCards();
        }
    }
}
