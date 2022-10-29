using Durak.Logic;
using Durak.Logic.AbstractClasses;
using Durak.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak.ConsoleUI
{
    internal class Human : Player
    {
        public Human(PlayTable playTable, Stack<Card> deck) : base(playTable, deck)
        {
        }

        public override bool Play()
        {
            string input;
            int index;
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out index) && index >= 1 && index <= _cards.Count && _playTable.AddCard(_cards[index - 1]))
                {
                    _cards.RemoveAt(index - 1);
                    return true;
                }
                else if (input == "" && _playTable.Cards.Any())
                {
                    return false;
                }
                else
                {
                    --Console.CursorTop;
                    Console.Write(" ");
                    --Console.CursorLeft;
                }
            }
        }
    }
}
