using System;
using NineMensMorris.Logic.AI;
using NineMensMorris.Logic.Algorithms.Heuristics;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Extensions;
using NineMensMorris.Logic.Helpers;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.Algorithms
{
    public class MinMaxAiMove : IAiMove
    {
        private IHeuristic _heuristic;

        public MinMaxAiMove(IHeuristic heuristic)
        {
            _heuristic = heuristic;
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
                //przelicz mozliwe ruchy dla gracza bialego i stworz tyle dzieci ile jest mozliwych ruchow
                board.GetPossibleMoves(currentPlayer);
            }

            throw new InvalidOperationException();
        }

        public void Minimax(Node position, int depth, Color color)
        {
            if (depth == 0)
            {
                //evalute node
                position.Value = _heuristic.EvaluateGameState(position);
            }
        }
    }
}