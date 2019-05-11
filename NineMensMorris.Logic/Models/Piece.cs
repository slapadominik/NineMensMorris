using System;

namespace NineMensMorris.Logic.Models
{
    [Serializable]
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