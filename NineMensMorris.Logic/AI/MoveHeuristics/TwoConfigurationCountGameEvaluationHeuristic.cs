using System.Linq;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.MoveHeuristics
{
    public class TwoConfigurationCountGameEvaluationHeuristic : IGameEvaluationHeuristic
    {
        public int EvaluateGameState(Board board, Color currentPlayer)
        {
            if (currentPlayer == Color.White)
            {
                return board.GetAlmostMills(currentPlayer).Count() * 10;
            }
            else
            {
                return board.GetAlmostMills(currentPlayer).Count() * -10;
            }
        }
    }
}