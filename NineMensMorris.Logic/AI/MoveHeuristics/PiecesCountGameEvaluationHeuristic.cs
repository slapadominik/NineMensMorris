using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.MoveHeuristics
{
    public class PiecesCountGameEvaluationHeuristic : IGameEvaluationHeuristic
    {
        public int EvaluateGameState(Node state, Color currentPlayer)
        {
            return state.Board.GetPlayerPieces(currentPlayer).Count;
        }
    }
}