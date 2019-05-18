using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.MoveHeuristics
{
    public class PiecesCountGameEvaluationHeuristic : IGameEvaluationHeuristic
    {
        public int EvaluateGameState(Board board, Color currentPlayer)
        {
            return board.GetPlayerPieces(currentPlayer).Count;
        }
    }
}