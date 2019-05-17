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

        public static GameStatus GameStatus(Color currentPlayer)
        {
            if (Moves < MovesToGameStage2)
            {
                return Consts.GameStatus.Initialization;
            }

            if (currentPlayer == Color.White && WhitePieces <= PiecesToGameStage3)
            {
                return Consts.GameStatus.WhiteLastStage;
            }

            if (currentPlayer == Color.Black && BlackPieces <= PiecesToGameStage3)
            {
                return Consts.GameStatus.BlackLastStage;
            }
            return Consts.GameStatus.Middle;
        }
    }
}