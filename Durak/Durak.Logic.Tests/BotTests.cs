using Durak.Logic.Enums;
using Durak.Logic.Models;

namespace Durak.Logic.Tests
{
    public class BotTests
    {
        private Stack<Card> deck;
        private PlayTable playTable;
        private Bot bot;

        [SetUp]
        public void Setup()
        {
            deck = new Stack<Card>();
            deck.Push(new Card(CardNumber.Six, CardSuit.Hearts));
            deck.Push(new Card(CardNumber.Seven, CardSuit.Diamonds));
            deck.Push(new Card(CardNumber.Eight, CardSuit.Diamonds));
            deck.Push(new Card(CardNumber.Nine, CardSuit.Diamonds));
            deck.Push(new Card(CardNumber.Ten, CardSuit.Diamonds));
            deck.Push(new Card(CardNumber.Jack, CardSuit.Diamonds));
            playTable = new PlayTable(CardSuit.Hearts);
            bot = new Bot(playTable, deck);
            bot.TakeCardsFromDeck();
        }

        [Test]
        public void Attack_EmptyTable()
        {
            Assert.AreEqual(true, bot.Play());
            Assert.AreEqual(new Card(CardNumber.Seven, CardSuit.Diamonds), playTable.Cards.Last());
        }

        [Test]
        public void Attack_ThereAreCardsEqualToPlayersCards_TakesMinimumCard()
        {
            playTable.AddCard(new Card(CardNumber.Seven, CardSuit.Clubs));
            playTable.AddCard(new Card(CardNumber.Eight, CardSuit.Clubs));

            Assert.AreEqual(true, bot.Play());
            Assert.AreEqual(3, playTable.Cards.Count());
            Assert.AreEqual(new Card(CardNumber.Seven, CardSuit.Diamonds), playTable.Cards.Last());
        }

        [Test]
        public void Attack_ThereAreNoCardsEqualToPlayersCards_ReturnsFalse()
        {
            playTable.AddCard(new Card(CardNumber.Queen, CardSuit.Clubs));
            playTable.AddCard(new Card(CardNumber.King, CardSuit.Clubs));

            Assert.AreEqual(false, bot.Play());
            Assert.AreEqual(2, playTable.Cards.Count());
        }

        [Test]
        public void Defend_HasCardToDefend()
        {
            playTable.AddCard(new Card(CardNumber.Six, CardSuit.Diamonds));

            Assert.AreEqual(true, bot.Play());
            Assert.AreEqual(2, playTable.Cards.Count());
            Assert.AreEqual(new Card(CardNumber.Seven, CardSuit.Diamonds), playTable.Cards.Last());
        }

        [Test]
        public void Defend_HasTrumpCardToDefend()
        {
            playTable.AddCard(new Card(CardNumber.Seven, CardSuit.Clubs));

            Assert.AreEqual(true, bot.Play());
            Assert.AreEqual(2, playTable.Cards.Count());
            Assert.AreEqual(new Card(CardNumber.Six, CardSuit.Hearts), playTable.Cards.Last());
        }

        [Test]
        public void Defend_HasNoCardToDefend()
        {
            playTable.AddCard(new Card(CardNumber.Seven, CardSuit.Hearts));

            Assert.AreEqual(false, bot.Play());
            Assert.AreEqual(1, playTable.Cards.Count());
        }
    }
}