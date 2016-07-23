using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UglyTrivia;

namespace Tests
{
    [TestFixture]
    public class TriviaTests
    {
        [Test]
        public void AGameWithNoPlayersIsUnplayable()
        {
            Game sut = new Game();
            
            Assert.IsFalse(sut.hasEnoughPlayers());
        }

        [Test]
        public void AGameWithOnePlayerIsUnplayable()
        {
            Game sut = new Game();

            sut.addPlayer("Bob");

            Assert.IsFalse(sut.hasEnoughPlayers());
        }

        [Test]
        public void AGameWithTwoPlayerIsPlayable()
        {
            Game sut = new Game();

            sut.addPlayer("Bob");
            sut.addPlayer("Jack");

            Assert.IsTrue(sut.hasEnoughPlayers());
        }

        [Test]
        public void TotalNumberOfPlayersMatchesNumberOfPlayersAdded()
        {

            Game sut = new Game();

            sut.addPlayer("Bob");
            sut.addPlayer("Jack");

            Assert.AreEqual(2, sut.howManyPlayers());
        }

        [Test]
        public void TotalNumberOfPlayersForANewGameIsZero()
        {

            Game sut = new Game();

            Assert.AreEqual(0, sut.howManyPlayers());
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(5, true)]
        [TestCase(6, false)]
        public void CorrectAnswersRequiredToWinAGame(int turnCount, bool expected)
        {
            var sut = new Game();
            sut.addPlayer("Bob");
            sut.addPlayer("Alice");

            bool didPlayerWin = false;
            for (int turn = 1; turn <= turnCount; turn++)
            {
                //Bob
                sut.advancePlayerAndAskQuestion(1);
                didPlayerWin = sut.shouldContinueOnCorrectAnswer();

                //Alice
                sut.advancePlayerAndAskQuestion(1);
                sut.shouldContinueOnCorrectAnswer();
            }

            Assert.AreEqual(expected, didPlayerWin);
        }
    }
}
