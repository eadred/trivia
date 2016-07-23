using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        List<Player> _players = new List<Player>();

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(createRockQuestion(i));
            }
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {

            _players.Add(new Player() {Name = playerName});
            _players[howManyPlayers() - 1].Place = 0;
            _players[howManyPlayers() - 1].Purse = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return _players.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(_players[currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[currentPlayer].Name + " is getting out of the penalty box");
                    _players[currentPlayer].Place = _players[currentPlayer].Place + roll;
                    if (_players[currentPlayer].Place > 11)
                        _players[currentPlayer].Place = _players[currentPlayer].Place - 12;

                    Console.WriteLine(_players[currentPlayer].Name
                            + "'s new location is "
                            + _players[currentPlayer].Place);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(_players[currentPlayer].Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                _players[currentPlayer].Place = _players[currentPlayer].Place + roll;
                if (_players[currentPlayer].Place > 11)
                    _players[currentPlayer].Place = _players[currentPlayer].Place - 12;

                Console.WriteLine(_players[currentPlayer].Name
                        + "'s new location is "
                        + _players[currentPlayer].Place);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }

        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private String currentCategory()
        {
            if (_players[currentPlayer].Place == 0) return "Pop";
            if (_players[currentPlayer].Place == 4) return "Pop";
            if (_players[currentPlayer].Place == 8) return "Pop";
            if (_players[currentPlayer].Place == 1) return "Science";
            if (_players[currentPlayer].Place == 5) return "Science";
            if (_players[currentPlayer].Place == 9) return "Science";
            if (_players[currentPlayer].Place == 2) return "Sports";
            if (_players[currentPlayer].Place == 6) return "Sports";
            if (_players[currentPlayer].Place == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players[currentPlayer].Purse++;
                    Console.WriteLine(_players[currentPlayer].Name
                            + " now has "
                            + _players[currentPlayer].Purse
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == _players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == _players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _players[currentPlayer].Purse++;
                Console.WriteLine(_players[currentPlayer].Name
                        + " now has "
                        + _players[currentPlayer].Purse
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == _players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[currentPlayer].Name + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == _players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(_players[currentPlayer].Purse == 6);
        }
    }
}
