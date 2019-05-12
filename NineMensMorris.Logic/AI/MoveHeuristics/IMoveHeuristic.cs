namespace NineMensMorris.Logic.AI.MoveHeuristics
{
    public interface IMoveHeuristic
    {
        int EvaluateGameState(Node state);
    }
}