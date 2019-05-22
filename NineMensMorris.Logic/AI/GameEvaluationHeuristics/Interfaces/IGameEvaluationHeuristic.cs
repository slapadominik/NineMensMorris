using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.GameEvaluationHeuristics.Interfaces
{
    public interface IGameEvaluationHeuristic
    {
        int EvaluateGameState(Board board, Color currentPlayer);
    }
}