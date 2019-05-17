using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public static class GameConfiguration
    {
        public static int WhitePieces = 0;
        public static int BlackPieces = 0;

        public static int Moves = 0;
        private const int MovesToGameStage2 = 18;
        private const int PiecesToGameStage3 = 3;

        public static GameStatus GameStatus
        {
            get
            {
                if (Moves <= MovesToGameStage2)
                {
                    return GameStatus.Initialization;
                }
                return GameStatus.Middle;
            }
        }
    }
}