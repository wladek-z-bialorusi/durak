using Durak.Logic;
using Durak.Logic.Models;

namespace Durak.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var deckAndTrump = new DeckGenerator().Generate();
            var deck = deckAndTrump.deck;
            var trump = deckAndTrump.trump;
            var playTable = new PlayTable(trump.CardSuit);
            var human = new Human(playTable, deck);
            var bot = new Bot(playTable, deck);
            var consoleGraphics = new ConsoleGraphics(human, bot, playTable, deck, trump);

            var winner = new Game(deckAndTrump.deck, playTable, human, bot, consoleGraphics).Play();
            Console.WriteLine(winner.GetType() == typeof(Bot) ? "You lost" : "You won");
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}