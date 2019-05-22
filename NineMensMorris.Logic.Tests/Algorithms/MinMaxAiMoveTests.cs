using NineMensMorris.Logic.AI.Algorithms;
using NineMensMorris.Logic.AI.CaptureHeuristics;
using NineMensMorris.Logic.AI.GameEvaluationHeuristics;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;
using NUnit.Framework;

namespace NineMensMorris.Logic.Tests.Algorithms
{
    [TestFixture]
    public class MinMaxAiMoveTests
    {
        private MinMaxAiMove _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MinMaxAiMove(new PiecesCountGameEvaluationHeuristic(), new PiecesToMillCaptureHeuristic());
        }

    }
}