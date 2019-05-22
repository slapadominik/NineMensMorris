using NineMensMorris.Logic.AI.GameEvaluationHeuristics.Interfaces;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.GameEvaluationHeuristics
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