using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.MoveHeuristics
{
    public class MillsCountGameEvaluationHeuristic : IGameEvaluationHeuristic
    {
        public int EvaluateGameState(Board board, Color currentPlayer)
        {
            if (currentPlayer == Color.White)
            {
                return board.CountMills(currentPlayer)*10;
            }
            else return board.CountMills(currentPlayer) * (-10);
        }
    }
}