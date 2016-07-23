using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Trivia
{
    [TestFixture]
    public class GeneratorTests
    {
        [Test]
        public void CompareGoldenMaster()
        {
            var itCount = 10000;

            var outputDirectory = Path.Combine(Path.GetTempPath(), "UnderTest");
            GameResultsGenerator.GenerateTestOutputs(itCount, outputDirectory);

            var goldenMasterDirectory = @"C:\Users\eabi\Documents\Code Retreat\trivia\C#\Trivia\GoldenMaster";

            for (int iteration = 0; iteration < itCount; iteration++)
            {
                string fileExpected = Path.Combine(goldenMasterDirectory, $"Expected_{iteration}.txt");
                string fileActual = Path.Combine(outputDirectory, $"Run_{iteration}.txt");
                Assert.True(FileCompare.Compare(fileExpected, fileActual), $"Failed on iteration {iteration}.");
            }
        }
    }
}
