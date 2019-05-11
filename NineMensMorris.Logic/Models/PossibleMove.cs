using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class PossibleMove
    {
        public string From { get; set; }
        public string To { get; set; }
        public MoveType MoveType { get; set; }
    }
}