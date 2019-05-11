using System;
using System.Collections.Generic;
using System.Linq;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Exceptions;

namespace NineMensMorris.Logic.Models
{
    [Serializable]
    public class Board
    {
        private Dictionary<string, Piece> _board;

        public Board()
        {
            _board = new Dictionary<string, Piece>()
            {
                {Locations.A7,null}, {Locations.D7,null},{Locations.G7,null},
                {Locations.B6, null},  {Locations.D6, null},{Locations.F6, null},
                {Locations.C5, null}, {Locations.D5, null}, {Locations.E5, null},
                {Locations.A4, null}, {Locations.B4, null}, {Locations.C4, null}, {Locations.E4, null}, {Locations.F4, null}, {Locations.G4, null},
                {Locations.C3, null}, {Locations.D3, null}, {Locations.E3, null},
                {Locations.B2, null}, {Locations.D2, null}, {Locations.F2, null},
                {Locations.A1,null}, {Locations.D1, null}, {Locations.G1, null},
            };
        }

        public void SetPiece(string location, Piece piece)
        {
            if (!_board.ContainsKey(location))
            {
                throw new InvalidMoveException($"Location {location} does not exist.");
            }
            _board[location] = piece;
        }

        public Piece GetPiece(string location)
        {
            if (!_board.ContainsKey(location))
            {
                throw new InvalidMoveException($"Location {location} does not exist.");
            }
            return _board[location];
        }

        public IEnumerable<PossibleMove> GetPossibleMoves(Color currentPlayer)
        {
            if (Game.GameStatus == GameStatus.Initialization)
            {
                List<PossibleMove> possibleMoves = new List<PossibleMove>();
                foreach (var key in _board.Keys)
                {
                    if (_board[key] == null)
                    {
                        possibleMoves.Add(new PossibleMove{To = key, MoveType = MoveType.AddPiece});
                    }
                }
                return possibleMoves;
            }

            if (Game.GameStatus == GameStatus.Middle)
            {
                return  _board.Values.Where(x => x != null && x.Color == currentPlayer).SelectMany(x => GetPossibleMoves(x));
            }

            return null;
        }

        private IEnumerable<PossibleMove> GetPossibleMoves(Piece piece)
        {
            throw new NotImplementedException();
        }
    }
}