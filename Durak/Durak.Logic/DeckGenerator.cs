using Durak.Logic.Enums;
using Durak.Logic.Models;

namespace Durak.Logic
{
    public class DeckGenerator
    {
        private Random _random;

        public DeckGenerator()
        {
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public (Stack<Card> deck, Card trump) Generate()
        {
            List<Card> list = new List<Card>();
            Stack<Card> deck = new Stack<Card>();
            for (int i = 0; i <= (int)CardNumber.Ace; ++i)
            {
                for (int j = 0; j <= (int)CardSuit.Spades; ++j)
                {
                    list.Add(new Card((CardNumber)i, (CardSuit)j));
                }
            }

            Card trump = list[_random.Next(list.Count)];
            deck.Push(trump);
            list.Remove(trump);

            for (int i = list.Count, next; i > 0; --i)
            {
                next = _random.Next(i);
                deck.Push(list[next]);
                list.RemoveAt(next);
            }

            return (deck, trump);
        }
    }
}
