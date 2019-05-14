using System.Collections.Generic;
using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class AlmostMill
    {
        public Position Position { get; set; }

        public IList<string> Tiles { get; set; }

        public AlmostMill()
        {
            Tiles = new List<string>();
        }
    }
}