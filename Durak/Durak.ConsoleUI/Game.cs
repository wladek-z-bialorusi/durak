using Durak.Logic;
using Durak.Logic.AbstractClasses;
using Durak.Logic.Models;

namespace Durak.ConsoleUI
{
    internal class Game
    {
        private PlayTable _playTable;
        private Player _attacker;
        private Player _defender;
        private ConsoleGraphics _consoleGraphics;

        public Game(Stack<Card> deck, PlayTable playTable, Player firstPlayer, Player secondPlayer, ConsoleGraphics consoleGraphics)
        {
            _playTable = playTable;
            (_attacker, _defender) = (firstPlayer, secondPlayer);//FirstAttackerAndDefender.Find(firstPlayer, secondPlayer);
            _consoleGraphics = consoleGraphics;
        }

        /// <summary>
        /// Returns the winner.
        /// </summary>
        /// <returns>Player winner.</returns>
        public Player Play()
        {
            _attacker.TakeCardsFromDeck();
            _defender.TakeCardsFromDeck();
            while (PlayOneTurn())
            {
            }

            return new Player[] { _attacker, _defender }.Where(p => !p.Cards.Any()).First();
        }


        public bool PlayOneTurn()
        {
            bool successfulAttack = false;
            while (true)
            {
                _consoleGraphics.Display();
                if (!_attacker.Play())
                {
                    _playTable.RemoveAllCards();
                    break;
                }

                _consoleGraphics.Display();
                if (!_defender.Play())
                {
                    successfulAttack = true;
                    _defender.TakeCardsFromTable();
                    break;
                }
            }

            if (!successfulAttack)
            {
                SwapPlayers();
            }

            _attacker.TakeCardsFromDeck();
            _defender.TakeCardsFromDeck();
            return _attacker.Cards.Any() && _defender.Cards.Any();
        }

        private void SwapPlayers()
        {
            Player temp = _attacker;
            _attacker = _defender;
            _defender = temp;
        }
    }
}
