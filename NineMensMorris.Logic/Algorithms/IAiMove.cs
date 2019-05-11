using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.Algorithms
{
    public interface IAiMove
    {
        MoveStatus Move(Board board, Color color);
    }
}