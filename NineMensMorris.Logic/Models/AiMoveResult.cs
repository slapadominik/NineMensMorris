using System;
using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class AiMoveResult
    {
        public AiMoveResult(Board board, MoveType moveType, Color playerColor)
        {
            Board = board;
            MoveType = moveType;
            PlayerColor = playerColor;
        }

        public Board Board { get; set; }
        public MoveType MoveType { get; set; }
        public Color PlayerColor { get; set; }
        public TimeSpan Elapsed { get; set; }
        public int NodesVisited { get; set; }
    }
}