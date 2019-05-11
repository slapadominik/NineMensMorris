using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.Helpers
{
    public static class ColorHelper
    {
        public static Color GetOpponentColor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }
    }
}