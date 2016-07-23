using System;
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
        private Action<int>[] rollAction = new Action<int>[6];

        Dictionary<string, LinkedList<string>> decks = new Dictionary<string, LinkedList<string>>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < rollAction.Length; i++)
            {
                rollAction[i] = rollWhenNotInPenaltyBox;
            }

            LinkedList<string> popQuestions = new LinkedList<string>();
            LinkedList<string> scienceQuestions = new LinkedList<string>();
            LinkedList<string> sportsQuestions = new LinkedList<string>();
            LinkedList<string> rockQuestions = new LinkedList<string>();

            for (int i = 0; i < 50; i++)
            {
                
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(createRockQuestion(i));
            }

            decks["Pop"] = popQuestions;
            decks["Science"] = scienceQuestions;
            decks["Sports"] = sportsQuestions;
            decks["Rock"] = rockQuestions;
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


            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;
            rollAction[howManyPlayers()] = rollWhenNotInPenaltyBox;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        

        public void roll(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            rollAction[currentPlayer](roll);

        }

        private void rollWhenNotInPenaltyBox(int roll)
        {
            places[currentPlayer] = (places[currentPlayer] + roll)%12;

            Console.WriteLine(players[currentPlayer]
                              + "'s new location is "
                              + places[currentPlayer]);
            Console.WriteLine("The category is " + currentCategory());
            askQuestion();
        }

        private void rollWhenInPenaltyBox(int roll)
        {
            if (roll%2 != 0)
            {
                isGettingOutOfPenaltyBox = true;

                Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");

                places[currentPlayer] = (places[currentPlayer] + roll)%12;

                Console.WriteLine(players[currentPlayer]
                                  + "'s new location is "
                                  + places[currentPlayer]);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }
            else
            {
                Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox = false;
            }
        }

        private void askQuestion()
        {
            var deck = decks[currentCategory()];
            Console.WriteLine(deck.First());
            deck.RemoveFirst();
        }


        private String currentCategory()
        {
            var cats = new Dictionary<int, string>
            {
                {0, "Pop"},
                {1, "Science"},
                {2, "Sports"},
                {3, "Rock"}
            };

            return cats[places[currentPlayer]%4];
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

                    currentPlayer = (currentPlayer + 1) % players.Count;

                    return winner;
                }
                else
                {
                    currentPlayer = (currentPlayer + 1) % players.Count;
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
                currentPlayer = (currentPlayer + 1) % players.Count;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;
            rollAction[currentPlayer] = rollWhenInPenaltyBox;

            currentPlayer = (currentPlayer + 1) % players.Count;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }

}
