using NineMensMorris.Logic.AI.Algorithms;
using NineMensMorris.Logic.AI.CaptureHeuristics;
using NineMensMorris.Logic.AI.MoveHeuristics;
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

        [Test]
        public void BuildStateSpace_Tests()
        {
            //Arrange
            var board = new Board();
            //Act
            var stateSpace = _sut.BuildStateSpace(board, 3, Color.White);
            //Assert
            Assert.NotNull(stateSpace);
        }
    }
}