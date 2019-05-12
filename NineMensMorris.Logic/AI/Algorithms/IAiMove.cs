using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.Algorithms
{
    public interface IAiMove
    {
        MoveType Move(Board board, Color currentPlayer);
    }
}