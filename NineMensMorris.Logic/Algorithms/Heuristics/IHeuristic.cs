using NineMensMorris.Logic.AI;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.Algorithms.Heuristics
{
    public interface IHeuristic
    {
        int EvaluateGameState(Node state);
    }
}