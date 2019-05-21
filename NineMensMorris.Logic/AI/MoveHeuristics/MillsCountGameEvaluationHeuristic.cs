using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.MoveHeuristics
{
    public class MillsCountGameEvaluationHeuristic : IGameEvaluationHeuristic
    {
        public int EvaluateGameState(Board board, Color currentPlayer)
        {
            return board.CountMills(currentPlayer);
        }
    }
}