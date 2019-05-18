using System;
using System.Linq;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Helpers;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.CaptureHeuristics
{
    public class PiecesToMillCaptureHeuristic : ICaptureHeuristic
    {
        private readonly Random random = new Random();
        public string ChoosePieceToCapture(Board board, Color currentPlayer)
        {
            var almostMills = board.GetAlmostMills(ColorHelper.GetOpponentColor(currentPlayer));
            if (almostMills.Any())
            {
                var almostMill = almostMills.ElementAt(random.Next(0, almostMills.Count()));
                return almostMill.Tiles[random.Next(0, almostMill.Tiles.Count)];
            }

            var opponentPieces = board.GetPlayerPieces(ColorHelper.GetOpponentColor(currentPlayer));
            return opponentPieces[random.Next(0, opponentPieces.Count)].Location;
        }
    }
}