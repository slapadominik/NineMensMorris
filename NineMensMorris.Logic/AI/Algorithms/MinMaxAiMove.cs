using System;
using System.Linq;
using NineMensMorris.Logic.AI.CaptureHeuristics;
using NineMensMorris.Logic.AI.MoveHeuristics;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Extensions;
using NineMensMorris.Logic.Helpers;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.Algorithms
{
    public class MinMaxAiMove : IAiMove
    {
        private IMoveHeuristic _moveHeuristic;
        private ICaptureHeuristic _captureHeuristic;
        private const int Depth = 3;

        public MinMaxAiMove(IMoveHeuristic moveHeuristic, ICaptureHeuristic captureHeuristic)
        {
            _moveHeuristic = moveHeuristic;
            _captureHeuristic = captureHeuristic;
        }

        public MoveResult Move(Board board, Color currentPlayer)
        {
            var stateSpace = BuildStateSpace(board, Depth, currentPlayer);
            var value = Minimax(stateSpace, Depth, currentPlayer);
            var bestAvailableMove = stateSpace.Children.Single(x => x.Value == value);
            return new MoveResult {Board = board, MoveType = bestAvailableMove.MoveType};
        }

        public Node BuildStateSpace(Board board, int depth, Color currentPlayer)
        {
            if (depth == 0)
            {
                return null;
            }

            Node node = new Node(currentPlayer) { Board = board, MoveType = MoveType.Normal};
            var possibleMoves = board.GetPossibleMoves(currentPlayer);
            foreach (var move in possibleMoves)
            {
                var newBoard = board.DeepClone();
                if (GameConfiguration.GameStatus == GameStatus.Initialization)
                {
                    newBoard.SetPiece(move.To, new Piece(currentPlayer, move.To));
                    node.MoveType = MoveType.AddPiece;
                }
                else
                {
                    var piece = newBoard.GetPiece(move.From);
                    newBoard.SetPiece(move.To, piece);
                }
                var millsCount = newBoard.CountMills(currentPlayer);
                if (millsCount > 0)
                {
                    var captureLocation = _captureHeuristic.ChoosePieceToCapture(newBoard, currentPlayer);
                    board.SetPiece(captureLocation, null);
                    node.MoveType = MoveType.Capture;
                }
                node.AddState(BuildStateSpace(newBoard, depth - 1, ColorHelper.GetOpponentColor(currentPlayer)));
            }
            return node;
        }

        public int Minimax(Node position, int depth, Color currentPlayer)
        {
            if (depth == 0)
            {
                var value = _moveHeuristic.EvaluateGameState(position);
                position.Value = value;
                return value;
            }

            if (currentPlayer == Color.White)
            {
                var maxEval = Int32.MaxValue;
                foreach (var child in position.Children)
                {
                    var eval = Minimax(child, depth-1, ColorHelper.GetOpponentColor(currentPlayer));
                    maxEval = Math.Max(maxEval, eval);
                }
                position.Value = maxEval;
                return maxEval;
            }
            else
            {
                var minEval = Int32.MinValue;
                foreach (var child in position.Children)
                {
                    var eval = Minimax(child, depth - 1, ColorHelper.GetOpponentColor(currentPlayer));
                    minEval = Math.Min(minEval, eval);
                }
                position.Value = minEval;
                return minEval;
            }
        }
    }
}