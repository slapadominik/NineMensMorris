using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public static class GameConfiguration
    {
        public static int Moves = 0;
        public const int MaxPiecesInitialized = 9;
        public const int WhitePieces = 0;
        public const int BlackPieces = 0;
        private const int MovesToGameStage2 = 18;
        private const int PiecesToGameStage3 = 3;

        public static GameStatus GameStatus
        {
            get
            {
                if (Moves < MovesToGameStage2)
                {
                    return GameStatus.Initialization;
                }
                return GameStatus.Middle;
            }
        }
    }
}