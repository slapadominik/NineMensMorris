using System;
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

        public MinMaxAiMove(IMoveHeuristic moveHeuristic)
        {
            _moveHeuristic = moveHeuristic;
        }

        public MoveType Move(Board board, Color currentPlayer)
        {
            var stateSpace = BuildStateSpace(board, 3, currentPlayer);
            Minimax(stateSpace, 3, currentPlayer);
            return MoveType.Normal;
        }

        public Node BuildStateSpace(Board board, int depth, Color currentPlayer)
        {
            if (depth == 0)
            {
                return null;
            }

            if (Game.GameStatus == GameStatus.Initialization)
            {
                Node root = new Node(currentPlayer) {Board = board};
                var possibleMoves = board.GetPossibleMoves(currentPlayer);
                foreach (var move in possibleMoves)
                {
                    var newBoard = board.DeepClone();
                    newBoard.SetPiece(move.To, new Piece(currentPlayer, move.To));
                    root.AddState(BuildStateSpace(newBoard, depth - 1, ColorHelper.GetOpponentColor(currentPlayer)));
                }
                return root;
            }

            if (Game.GameStatus == GameStatus.Middle)
            {
                Node root = new Node(currentPlayer) { Board = board };
                var possibleMoves = board.GetPossibleMoves(currentPlayer);
                foreach (var move in possibleMoves)
                {
                    var newBoard = board.DeepClone();
                    var piece = newBoard.GetPiece(move.From);
                    newBoard.SetPiece(move.To, piece);
                    var millsCount = newBoard.CountMills(currentPlayer);
                    CapturePiece(board, "miejsce", currentPlayer);
                    root.AddState(BuildStateSpace(newBoard, depth - 1, ColorHelper.GetOpponentColor(currentPlayer)));
                }
                return root;
            }

            throw new InvalidOperationException();
        }

        private bool CapturePiece(Board board, string location, Color currentPlayer)
        {
            if (board.GetPiece(location)?.Color == ColorHelper.GetOpponentColor(currentPlayer))
            {
                board.SetPiece(location, null);
                return true;
            }

            return false;

        }

        public void Minimax(Node position, int depth, Color color)
        {
            if (depth == 0)
            {
                //evalute node
                position.Value = _moveHeuristic.EvaluateGameState(position);
            }
        }
    }
}