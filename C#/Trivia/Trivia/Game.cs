﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {


        List<string> players = new List<string>();

        int[] places = new int[6];
        int[] purses = new int[6];

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
                AddNewQuestion(popQuestions, "Pop", i);
                AddNewQuestion(scienceQuestions, "Science", i);
                AddNewQuestion(sportsQuestions, "Sports", i);
                AddNewQuestion(rockQuestions, "Rock", i);
            }
        }

        private void AddNewQuestion(LinkedList<string> deck, string category, int i)
        {
            deck.AddLast($"{category} Question {i}");
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            InitialiseNewPlayerDetails(playerName);

            ReportNewPlayerAdded(playerName);

            return true;
        }

        private void ReportNewPlayerAdded(string playerName)
        {
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + howManyPlayers());
        }

        private void InitialiseNewPlayerDetails(string playerName)
        {
            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            ReportCurrentPlayersRoll(roll);

            if (IsCurrentPlayerInPenaltyBox())
            {
                if (RollIsOdd(roll))
                {
                    CanLeavePenaltyBox();
                    MoveAndAskPlayer(roll);
                }
                else
                {
                    CantLeavePenaltyBox();
                }
            }
            else
            {
                MoveAndAskPlayer(roll);
            }
        }

        private void ReportCurrentPlayersRoll(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
        }

        private static bool RollIsOdd(int roll)
        {
            return roll % 2 != 0;
        }

        private void MoveAndAskPlayer(int roll)
        {
            MovePlayerBy(roll);

            ReportNewPlayerPosition();

            AskQuestion();
        }

        private void ReportNewPlayerPosition()
        {
            Console.WriteLine(players[currentPlayer]
                              + "'s new location is "
                              + CurrentPlayerPlace);
        }

        private void MovePlayerBy(int roll)
        {
            CurrentPlayerPlace += roll;
            if (IsPlayerBeyondEndOfTheBoard) CurrentPlayerPlace -= 12;
        }

        private void CantLeavePenaltyBox()
        {
            Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
            isGettingOutOfPenaltyBox = false;
        }

        private void CanLeavePenaltyBox()
        {
            isGettingOutOfPenaltyBox = true;
            Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
        }

        private bool IsPlayerBeyondEndOfTheBoard => CurrentPlayerPlace > 11;

        private int CurrentPlayerPlace
        {
            get { return places[currentPlayer]; }
            set { places[currentPlayer] = value; }
        }

        private void AskQuestion()
        {
            Console.WriteLine("The category is " + currentCategory());
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

        private bool IsCurrentPlayerInPenaltyBox()
        {
            return inPenaltyBox[currentPlayer];
        }


        private String currentCategory()
        {
            if (places[currentPlayer] == 0) return "Pop";
            if (places[currentPlayer] == 4) return "Pop";
            if (places[currentPlayer] == 8) return "Pop";
            if (places[currentPlayer] == 1) return "Science";
            if (places[currentPlayer] == 5) return "Science";
            if (places[currentPlayer] == 9) return "Science";
            if (places[currentPlayer] == 2) return "Sports";
            if (places[currentPlayer] == 6) return "Sports";
            if (places[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
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
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }

}
