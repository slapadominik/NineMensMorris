using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.CaptureHeuristics.Interfaces
{
    public interface ICaptureHeuristic
    {
        string ChoosePieceToCapture(Board board, Color currentPlayer);
    }
}