using Durak.Logic;
using Durak.Logic.AbstractClasses;
using Durak.Logic.Enums;
using Durak.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak.ConsoleUI
{
    internal class ConsoleGraphics
    {
        private Player _human;
        private Player _bot;
        private PlayTable _playTable;
        private Stack<Card> _deck;
        private Card _trump;
        private static Dictionary<CardSuit, string> _cardSuits;
        private static Dictionary<CardNumber, string> _cardNumbers;

        public ConsoleGraphics(Player human, Player bot, PlayTable playTable, Stack<Card> deck, Card trump)
        {
            _human = human;
            _bot = bot;
            _playTable = playTable;
            _deck = deck;
            _trump = trump;
            _cardSuits = GenerateCardSuits();
            _cardNumbers = GenerateCardNumbers();
        }

        public void Display()
        {
            Console.Clear();
            Card[] tableCards = _playTable.Cards.ToArray();
            Console.Write("        |");
            for (int i = 0; i < _bot.Cards.Count(); ++i)
            {
                Console.Write($"##|");
            }

            Console.WriteLine();
            Console.WriteLine();
            if (_deck.Count >= 2)
            {
                Console.Write($"##|{CardToString(_trump)}|  ");
            }
            else if (_deck.Count == 1)
            {
                Console.Write($"  |{CardToString(_trump)}|  ");
            }
            else
            {
                Console.Write($"  |  |  ");
            }

            for (int i = 0; i < tableCards.Length; )
            {
                Console.Write($"|{CardToString(tableCards[i])} : ");
                ++i;
                if (i == tableCards.Length)
                {
                    break;
                }

                Console.Write($"{CardToString(tableCards[i])}|");
                ++i;
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("        |");
            int cardNumber = 1;
            foreach (var c in _human.Cards)
            {
                Console.Write($"{CardToString(c)}|");
                Console.CursorTop += 2;
                Console.CursorLeft -= 2;
                Console.Write(cardNumber++);
                Console.CursorTop -= 2;
                Console.CursorLeft += 1;
            }

            Console.WriteLine("\n\n");
        }

        private static string CardToString(Card card)
        {
            return _cardNumbers[card.CardNumber] + _cardSuits[card.CardSuit];
        }

        private Dictionary<CardSuit, string> GenerateCardSuits()
        {
            return new Dictionary<CardSuit, string>()
            {
                { CardSuit.Clubs, "♣" },
                { CardSuit.Diamonds, "♦" },
                { CardSuit.Hearts, "♥" },
                { CardSuit.Spades, "♠" },
            };
        }

        private Dictionary<CardNumber, string> GenerateCardNumbers()
        {
            return new Dictionary<CardNumber, string>()
            {
                { CardNumber.Six, "6" },
                { CardNumber.Seven, "7" },
                { CardNumber.Eight, "8" },
                { CardNumber.Nine, "9" },
                { CardNumber.Ten, "10" },
                { CardNumber.Jack, "J" },
                { CardNumber.Queen, "Q" },
                { CardNumber.King, "K" },
                { CardNumber.Ace, "A" },
            };
        }
    }
}
