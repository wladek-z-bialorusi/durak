using Durak.Logic.Enums;
using Durak.Logic.Models;

namespace Durak.Logic.Tests
{
    public static class PlayTableTestsSource
    {
        internal static CardSuit Trump = CardSuit.Hearts;

        internal static IEnumerable<TestCaseData> GetPlayTableTestsData
        {
            get
            {
                yield return new TestCaseData(new Card(CardNumber.Eight, CardSuit.Diamonds), new Card(CardNumber.Ten, CardSuit.Diamonds), true);
                yield return new TestCaseData(new Card(CardNumber.Eight, CardSuit.Diamonds), new Card(CardNumber.Six, CardSuit.Diamonds), false);
                yield return new TestCaseData(new Card(CardNumber.Eight, Trump), new Card(CardNumber.Ten, Trump), true);
                yield return new TestCaseData(new Card(CardNumber.Eight, Trump), new Card(CardNumber.Ten, CardSuit.Diamonds), false);
            }
        }
    }
}