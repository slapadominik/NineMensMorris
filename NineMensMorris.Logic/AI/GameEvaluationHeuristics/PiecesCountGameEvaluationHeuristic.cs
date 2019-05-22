using NineMensMorris.Logic.AI.GameEvaluationHeuristics.Interfaces;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Helpers;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.GameEvaluationHeuristics
{
    public class PiecesCountGameEvaluationHeuristic : IGameEvaluationHeuristic
    {
        public int EvaluateGameState(Board board, Color currentPlayer)
        {
            return board.GetPlayerPieces(currentPlayer).Count -
                   board.GetPlayerPieces(ColorHelper.GetOpponentColor(currentPlayer)).Count;
        }
    }
}