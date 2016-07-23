using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        public static void Main(String[] args)
        {
            GameResultsGenerator.GenerateTestOutputs(10, "expected");
        }
    }

}

