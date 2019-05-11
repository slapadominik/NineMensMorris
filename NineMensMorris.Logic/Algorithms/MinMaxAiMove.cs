using NineMensMorris.Logic.AI;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.Algorithms
{
    public class MinMaxAiMove : IAiMove
    {
        private Node _stateSpace;

        public MoveStatus Move(Board board, Color color)
        {
            throw new System.NotImplementedException();
        }

        private void BuildStateSpace(Board board, Color currentPlayer)
        {
            _stateSpace = new Node(currentPlayer);
            //przelicz mozliwe ruchy dla gracza bialego i stworz tyle dzieci ile jest mozliwych ruchow

        }
    }
}