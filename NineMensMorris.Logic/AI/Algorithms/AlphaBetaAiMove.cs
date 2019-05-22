using System;
using System.Collections.Generic;
using System.Diagnostics;
using NineMensMorris.Logic.AI.Algorithms.Interfaces;
using NineMensMorris.Logic.AI.CaptureHeuristics.Interfaces;
using NineMensMorris.Logic.AI.MoveHeuristics;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Helpers;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.Algorithms
{
    public class AlphaBetaAiMove : IAiMove
    {
        private IGameEvaluationHeuristic _gameEvaluationHeuristic;
        private ICaptureHeuristic _captureHeuristic;
        private PossibleMove _nextMaxMove;
        private PossibleMove _nextMinMove;
        private Stopwatch _stopwatch;
        private int _nodesVisited = 0;
        private const int Depth = 5;

        public AlphaBetaAiMove(IGameEvaluationHeuristic gameEvaluationHeuristic, ICaptureHeuristic captureHeuristic)
        {
            _gameEvaluationHeuristic = gameEvaluationHeuristic;
            _captureHeuristic = captureHeuristic;
            _stopwatch = new Stopwatch();
        }

        public AiMoveResult Move(Board board, Color currentPlayer)
        {
            _stopwatch.Start();
            var bestMoveValue = Alphabeta(board, Depth, Int32.MinValue, Int32.MaxValue, currentPlayer);
            _stopwatch.Stop();
            var elapsed = _stopwatch.Elapsed;
            _stopwatch.Reset();
            MoveResult moveResult;
            if (currentPlayer == Color.White)
            {
                moveResult = board.Move(_nextMaxMove.From, _nextMaxMove.To, currentPlayer);
            }
            else
            {
                moveResult = board.Move(_nextMinMove.From, _nextMinMove.To, currentPlayer);
            }

            if (moveResult.MoveType == MoveType.NewMill)
            {
                CapturePiece(board, currentPlayer);
            }

            var aiMoveResult = new AiMoveResult(moveResult.Board, moveResult.MoveType, currentPlayer)
            { Elapsed = elapsed, NodesVisited = _nodesVisited };
            _nodesVisited = 0;
            return aiMoveResult;
        }

        public int Alphabeta(Board board, int depth, int alpha, int beta, Color currentPlayer)
        {
            if (depth == 0)
            {
                _nodesVisited++;
                var value = _gameEvaluationHeuristic.EvaluateGameState(board, currentPlayer);
                return value;
            }

            if (currentPlayer == Color.White)
            {
                var maxEval = Int32.MinValue;
                var possibleMoves = board.GetPossibleMoves(currentPlayer);
                foreach (var possibleMove in possibleMoves)
                {
                    var newBoard = new Board(board);
                    var moveResult = newBoard.Move(possibleMove.From, possibleMove.To, currentPlayer);
                    if (moveResult.MoveType == MoveType.NewMill)
                    {
                        CapturePiece(newBoard, currentPlayer);
                    }
                    var eval = Alphabeta(newBoard, depth - 1, alpha, beta, ColorHelper.GetOpponentColor(currentPlayer));
                    if (depth == Depth && eval > maxEval)
                    {
                        _nextMaxMove = possibleMove;
                    }
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                    _nodesVisited++;
                }
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
                    var eval = Alphabeta(newBoard, depth - 1, alpha, beta, ColorHelper.GetOpponentColor(currentPlayer));
                    if (depth == Depth && eval < minEval)
                    {
                        _nextMinMove = possibleMove;
                    }
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                    _nodesVisited++;
                }
                return minEval;
            }
        }

        private void CapturePiece(Board board, Color currentPlayer)
        {
            var locationCapture = _captureHeuristic.ChoosePieceToCapture(board, currentPlayer);
            if (locationCapture != null)
            {
                board.SetPiece(locationCapture, null);
            }
        }
    }
}