using System.Collections.Generic;
using System.Linq;
using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class Player
    {
        public PlayerType PlayerType { get; }
        public Color Color { get; }
        public IList<Piece> Pieces { get; }

        public Player(Color color, PlayerType playerType)
        {
            PlayerType = playerType;
            Color = color;
        }

        public void AddPiece(string location)
        {
            Pieces.Add(new Piece(Color, location));
        }

        public bool RemovePiece(string location)
        {
            var piece = Pieces.SingleOrDefault(x => x.Location == location);
            if (piece == null)
            {
                return false;
            }
            return Pieces.Remove(piece);
        }
    }
}