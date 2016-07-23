using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {


        List<string> players = new List<string>();

        int[] playersPlaces = new int[6];
        int[] playersPurses = new int[6];

        bool[] playersInPenaltyBox = new bool[6];

        LinkedList<string> popQuestionsDeck = new LinkedList<string>();
        LinkedList<string> scienceQuestionsDeck = new LinkedList<string>();
        LinkedList<string> sportsQuestionsDeck = new LinkedList<string>();
        LinkedList<string> rockQuestionsDeck = new LinkedList<string>();

        int currentPlayer = 0;
        bool isCurrentPlayerGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestionsDeck.AddLast("Pop Question " + i);
                scienceQuestionsDeck.AddLast(("Science Question " + i));
                sportsQuestionsDeck.AddLast(("Sports Question " + i));
                rockQuestionsDeck.AddLast(createRockQuestion(i));
            }
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool hasEnoughPlayers()
        {
            return (howManyPlayers() >= 2);
        }

        public bool addPlayer(String playerName)
        {


            players.Add(playerName);
            playersPlaces[howManyPlayers()] = 0;
            playersPurses[howManyPlayers()] = 0;
            playersInPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void advancePlayerAndAskQuestion(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (playersInPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isCurrentPlayerGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    playersPlaces[currentPlayer] = playersPlaces[currentPlayer] + roll;
                    if (playersPlaces[currentPlayer] > 11) playersPlaces[currentPlayer] = playersPlaces[currentPlayer] - 12;

                    Console.WriteLine(players[currentPlayer]
                            + "'s new location is "
                            + playersPlaces[currentPlayer]);
                    Console.WriteLine("The category is " + questionCategoryForCurrentPlayer());
                    drawCardAndAskQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                    isCurrentPlayerGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                playersPlaces[currentPlayer] = playersPlaces[currentPlayer] + roll;
                if (playersPlaces[currentPlayer] > 11) playersPlaces[currentPlayer] = playersPlaces[currentPlayer] - 12;

                Console.WriteLine(players[currentPlayer]
                        + "'s new location is "
                        + playersPlaces[currentPlayer]);
                Console.WriteLine("The category is " + questionCategoryForCurrentPlayer());
                drawCardAndAskQuestion();
            }

        }

        private void drawCardAndAskQuestion()
        {
            if (questionCategoryForCurrentPlayer() == "Pop")
            {
                Console.WriteLine(popQuestionsDeck.First());
                popQuestionsDeck.RemoveFirst();
            }
            if (questionCategoryForCurrentPlayer() == "Science")
            {
                Console.WriteLine(scienceQuestionsDeck.First());
                scienceQuestionsDeck.RemoveFirst();
            }
            if (questionCategoryForCurrentPlayer() == "Sports")
            {
                Console.WriteLine(sportsQuestionsDeck.First());
                sportsQuestionsDeck.RemoveFirst();
            }
            if (questionCategoryForCurrentPlayer() == "Rock")
            {
                Console.WriteLine(rockQuestionsDeck.First());
                rockQuestionsDeck.RemoveFirst();
            }
        }


        private String questionCategoryForCurrentPlayer()
        {
            if (playersPlaces[currentPlayer] == 0) return "Pop";
            if (playersPlaces[currentPlayer] == 4) return "Pop";
            if (playersPlaces[currentPlayer] == 8) return "Pop";
            if (playersPlaces[currentPlayer] == 1) return "Science";
            if (playersPlaces[currentPlayer] == 5) return "Science";
            if (playersPlaces[currentPlayer] == 9) return "Science";
            if (playersPlaces[currentPlayer] == 2) return "Sports";
            if (playersPlaces[currentPlayer] == 6) return "Sports";
            if (playersPlaces[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool shouldContinueOnCorrectAnswer()
        {
            if (playersInPenaltyBox[currentPlayer])
            {
                if (isCurrentPlayerGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    playersPurses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + playersPurses[currentPlayer]
                            + " Gold Coins.");

                    bool continueGame = shouldContinue();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return continueGame;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                playersPurses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + playersPurses[currentPlayer]
                        + " Gold Coins.");

                bool continueGame = shouldContinue();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return continueGame;
            }
        }

        public bool shouldContinueOnIncorrectAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            playersInPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }

        private bool shouldContinue()
        {
            return !(playersPurses[currentPlayer] == 6);
        }
    }

}
