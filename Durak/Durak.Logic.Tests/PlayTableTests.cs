using Durak.Logic.Enums;
using Durak.Logic.Models;

namespace Durak.Logic.Tests
{
    public class PlayTableTests
    {
        private PlayTable _playTable;
        private CardSuit _trump;

        [SetUp]
        public void Setup()
        {
            _trump = PlayTableTestsSource.Trump;
            _playTable = new PlayTable(_trump);
        }

        [Test]
        public void FirstCard()
        {
            bool actual = _playTable.AddCard(new Card(CardNumber.Eight, CardSuit.Diamonds));
            Assert.AreEqual(true, actual);
        }

        [TestCaseSource(typeof(PlayTableTestsSource), nameof(PlayTableTestsSource.GetPlayTableTestsData))]
        public void OneFignt(Card attack, Card defend, bool expected)
        {
            _playTable.AddCard(attack);
            bool actual = _playTable.AddCard(defend);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveAllCards()
        {
            _playTable.AddCard(new Card(CardNumber.Eight, CardSuit.Diamonds));
            _playTable.RemoveAllCards();
            Assert.AreEqual(0, _playTable.Cards.Count());
        }
    }
}