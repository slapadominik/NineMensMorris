﻿using System;
using System.Diagnostics;
using NineMensMorris.Logic.AI.Algorithms.Interfaces;
using NineMensMorris.Logic.AI.CaptureHeuristics.Interfaces;
using NineMensMorris.Logic.AI.GameEvaluationHeuristics.Interfaces;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Helpers;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI.Algorithms
{
    public class MinMaxAiMove : IAiMove
    {
        private IGameEvaluationHeuristic _gameEvaluationHeuristic;
        private ICaptureHeuristic _captureHeuristic;
        private PossibleMove _nextMaxMove;
        private PossibleMove _nextMinMove;
        private Board _nextMaxBoard;
        private Board _nextMinBoard;
        private Stopwatch _stopwatch;
        private int _nodesVisited = 0;
        private const int Depth = 5;

        public MinMaxAiMove(IGameEvaluationHeuristic gameEvaluationHeuristic, ICaptureHeuristic captureHeuristic)
        {
            _gameEvaluationHeuristic = gameEvaluationHeuristic;
            _captureHeuristic = captureHeuristic;
            _stopwatch = new Stopwatch();
        }

        public AiMoveResult Move(Board board, Color currentPlayer)
        {
            _stopwatch.Start();
            var bestMoveValue = Minimax(board, Depth, currentPlayer);
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
                {Elapsed = elapsed, NodesVisited = _nodesVisited};
            _nodesVisited = 0;
            return aiMoveResult;
        }

        public int Minimax(Board board, int depth, Color currentPlayer)
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
                foreach (var possibleMove in board.GetPossibleMoves(currentPlayer))
                {
                    var newBoard = new Board(board);
                    var moveResult = newBoard.Move(possibleMove.From, possibleMove.To, currentPlayer);
                    if (moveResult.MoveType == MoveType.NewMill)
                    {
                        CapturePiece(newBoard, currentPlayer);
                    }
                    var eval = Minimax(newBoard, depth - 1, ColorHelper.GetOpponentColor(currentPlayer));
                    if (depth == Depth && eval > maxEval)
                    {
                        _nextMaxMove = possibleMove;
                    }
                    maxEval = Math.Max(maxEval, eval);
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
                    var eval = Minimax(newBoard, depth - 1, ColorHelper.GetOpponentColor(currentPlayer));
                    if (depth == Depth && eval < minEval)
                    {
                        _nextMinMove = possibleMove;
                    }
                    minEval = Math.Min(minEval, eval);
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