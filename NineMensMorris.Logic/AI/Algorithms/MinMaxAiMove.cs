using System;
using System.Collections.Generic;
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
        private IGameEvaluationHeuristic _gameEvaluationHeuristic;
        private ICaptureHeuristic _captureHeuristic;
        private Random _random;
        private const int Depth = 4;
        private PossibleMove _nextMove;

        public MinMaxAiMove(IGameEvaluationHeuristic gameEvaluationHeuristic, ICaptureHeuristic captureHeuristic)
        {
            _gameEvaluationHeuristic = gameEvaluationHeuristic;
            _captureHeuristic = captureHeuristic;
            _random = new Random();
        }

        public MoveResult Move(Board board, Color currentPlayer)
        {
            var bestMoveValue = Minimax(board, Depth, currentPlayer);
            var moveResult = board.Move(_nextMove.From, _nextMove.To, currentPlayer);
            if (moveResult.MoveType == MoveType.NewMill)
            {
                CapturePiece(board, currentPlayer);
            }
            return new MoveResult (moveResult.Board, moveResult.MoveType, currentPlayer);
        }

        public int Minimax(Board board, int depth, Color currentPlayer)
        {
            if (depth == 0)
            {
                var value = _gameEvaluationHeuristic.EvaluateGameState(board, currentPlayer);
                board.Value = value;
                return value;
            }

            if (currentPlayer == Color.White)
            {
                var maxEval = Int32.MinValue;
                foreach (var possibleMove in board.GetPossibleMoves(currentPlayer))
                {
                    var newBoard = new Board(board);
                    var moveResult = newBoard.Move(possibleMove.From, possibleMove.To, currentPlayer);
                    if (moveResult.MoveType == MoveType.NewMill)
                    {
                        CapturePiece(newBoard, currentPlayer);
                    }
                    var eval = Minimax(newBoard, depth-1, ColorHelper.GetOpponentColor(currentPlayer));
                    if (eval > maxEval)
                    {
                        _nextMove = possibleMove;
                    }
                    maxEval = Math.Max(maxEval, eval);
                }
                board.Value = maxEval;
                return maxEval;
            }
            else
            {
                var minEval = Int32.MaxValue;
                foreach (var possibleMove in board.GetPossibleMoves(currentPlayer))
                {
                    var newBoard = new Board(board);
                    var moveResult = newBoard.Move(possibleMove.From, possibleMove.To, currentPlayer);
                    if (moveResult.MoveType == MoveType.NewMill)
                    {
                        CapturePiece(newBoard, currentPlayer);
                    }
                    var eval = Minimax(newBoard, depth - 1, ColorHelper.GetOpponentColor(currentPlayer));
                    if (eval < minEval)
                    {
                        _nextMove = possibleMove;
                    }
                    minEval = Math.Min(minEval, eval);
                }
                board.Value = minEval;
                return minEval;
            }
        }

        private void CapturePiece(Board board, Color currentPlayer)
        {
            var locationCapture = _captureHeuristic.ChoosePieceToCapture(board, currentPlayer);
            board.SetPiece(locationCapture, null);
        }
    }
}