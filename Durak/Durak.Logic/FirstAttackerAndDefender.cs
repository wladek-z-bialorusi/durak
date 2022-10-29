using Durak.Logic.AbstractClasses;
using Durak.Logic.Models;

namespace Durak.Logic
{
    public static class FirstAttackerAndDefender
    {
        public static (Player attacker, Player defender) Find(Player first, Player second)
        {
            (Player attacker, Player defender) result;
            Card? lowestTrumpCardFirstPlayer = first.LowestTrumpCard();
            Card lowestCardFirstPlayer = first.LowestCard();
            Card? lowestTrumpCardSecondPlayer = first.LowestTrumpCard();
            Card lowestCardSecondPlayer = first.LowestCard();
            if (lowestTrumpCardFirstPlayer == null && lowestTrumpCardSecondPlayer == null)
            {
                result.attacker = new Player[] { first, second }.MinBy(c => c.LowestCard());
                result.defender = new Player[] { first, second }.MaxBy(c => c.LowestCard());
            }
            else if (lowestTrumpCardFirstPlayer == null)
            {
                result.attacker = second;
                result.defender = first;
            }
            else if (lowestTrumpCardSecondPlayer == null)
            {
                result.attacker = first;
                result.defender = second;
            }
            else
            {
                result.attacker = new Player[] { first, second }.MinBy(c => c.LowestTrumpCard());
                result.defender = new Player[] { first, second }.MaxBy(c => c.LowestTrumpCard());
            }

            return result;
        }
    }
}
