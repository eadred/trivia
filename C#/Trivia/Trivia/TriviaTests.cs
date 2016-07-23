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
            
            Assert.IsFalse(sut.isPlayable());
        }

        [Test]
        public void AGameWithOnePlayerIsUnplayable()
        {
            Game sut = new Game();

            sut.add("Bob");

            Assert.IsFalse(sut.isPlayable());
        }

        [Test]
        public void AGameWithTwoPlayerIsPlayable()
        {
            Game sut = new Game();

            sut.add("Bob");
            sut.add("Jack");

            Assert.IsTrue(sut.isPlayable());
        }

        [Test]
        public void TotalNumberOfPlayersMatchesNumberOfPlayersAdded()
        {

            Game sut = new Game();

            sut.add("Bob");
            sut.add("Jack");

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
            sut.add("Bob");
            sut.add("Alice");

            bool didPlayerWin = false;
            for (int turn = 1; turn <= turnCount; turn++)
            {
                //Bob
                sut.roll(1);
                didPlayerWin = sut.wasCorrectlyAnswered();

                //Alice
                sut.roll(1);
                sut.wasCorrectlyAnswered();
            }

            Assert.AreEqual(expected, didPlayerWin);
        }
    }
}
