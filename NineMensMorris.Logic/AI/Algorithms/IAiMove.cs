using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.Algorithms
{
    public interface IAiMove
    {
        MoveResult Move(Board board, Color currentPlayer);
    }
}