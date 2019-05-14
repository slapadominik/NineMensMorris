using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.MoveHeuristics
{
    public interface IGameEvaluationHeuristic
    {
        int EvaluateGameState(Node state, Color currentPlayer);
    }
}