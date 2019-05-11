using NineMensMorris.Logic.Algorithms;
using NineMensMorris.Logic.Algorithms.Heuristics;
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
            _sut = new MinMaxAiMove(new PiecesCountHeuristic());
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