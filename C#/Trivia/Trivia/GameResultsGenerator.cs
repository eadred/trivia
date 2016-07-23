using System;
using System.IO;
using UglyTrivia;

namespace Trivia
{
    public class GameResultsGenerator
    {
        public static void GenerateTestOutputs(int numberOfTests, string rootPath)
        {
            for (int iteration = 0; iteration < numberOfTests; iteration++)
            {
                RunTest(iteration, rootPath);
            }
        }

        private static void RunTest(int iteration, string rootPath)
        {
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var formattableString = $"Run_{iteration}.txt";
            var path = Path.Combine(rootPath, formattableString);
            using (var str = new FileStream(path, FileMode.Create))
            {
                using (var tw = new StreamWriter(str))
                {
                    Console.SetOut(tw);
                    RunGame(iteration);
                    tw.Close();
                }
            }
        }

        private static void RunGame(int seed)
        {
            bool shouldContinue = false;
            Game aGame = new Game();

            aGame.addPlayer("Chet");
            aGame.addPlayer("Pat");
            aGame.addPlayer("Sue");


            Random rand = new Random(seed);

            do
            {
                aGame.playTurn(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    shouldContinue = aGame.shouldContinueOnIncorrectAnswer();
                }
                else
                {
                    shouldContinue = aGame.shouldContinueOnCorrectAnswer();
                }
            } while (shouldContinue);
        }
    }
}