using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class Player
    {
        public PlayerType PlayerType { get; }
        public Color Color { get; }

        public Player(Color color, PlayerType playerType)
        {
            PlayerType = playerType;
            Color = color;
        }
    }
}