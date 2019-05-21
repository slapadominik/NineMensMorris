using System;
using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class Piece
    {
        public Color Color { get; }
        public string Location { get; set; }

        public Piece(Color color, string location)
        {
            Color = color;
            Location = location;
        }
    }
}