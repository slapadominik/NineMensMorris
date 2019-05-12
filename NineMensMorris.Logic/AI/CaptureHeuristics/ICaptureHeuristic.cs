using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.CaptureHeuristics
{
    public interface ICaptureHeuristic
    {
        string ChoosePieceToCapture(Board board, Color currentPlayer);
    }
}