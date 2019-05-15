using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class MoveResult
    {
        public MoveResult(Board board, MoveType moveType, Color playerColor)
        {
            Board = board;
            MoveType = moveType;
            PlayerColor = playerColor;
        }

        public Board Board { get; set; }
        public MoveType MoveType { get; set; }
        public Color PlayerColor { get; set; }
    }
}