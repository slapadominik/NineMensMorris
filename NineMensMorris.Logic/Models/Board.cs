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
        private static Dictionary<string, List<string>> _neighbours;
        private static List<List<string>> _rowMillsLocations;
        private static List<List<string>> _columnMillsLocations;
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

        static Board()
        {
            _neighbours = new Dictionary<string, List<string>>()
            {
                { Locations.A7, new List<string>{Locations.A4, Locations.D7}},
                { Locations.D7,new List<string>{Locations.A7, Locations.G7, Locations.D6}},
                { Locations.G7,new List<string>{Locations.D7, Locations.G4}},
                { Locations.B6,new List<string>{Locations.B4, Locations.D6}},
                { Locations.D6,new List<string>{Locations.B6, Locations.F6, Locations.D5, Locations.D7}},
                { Locations.F6,new List<string>{Locations.D6, Locations.F4}},
                { Locations.C5,new List<string>{Locations.C4, Locations.D5}},
                { Locations.D5,new List<string>{Locations.C5, Locations.E5, Locations.D6}},
                { Locations.E5,new List<string>{Locations.E4, Locations.D5}},
                { Locations.A4,new List<string>{Locations.A1, Locations.A7, Locations.B4}},
                { Locations.B4,new List<string>{Locations.A4, Locations.C4, Locations.B6, Locations.B2}},
                { Locations.C4,new List<string>{Locations.C3, Locations.C5, Locations.B4}},
                { Locations.E4,new List<string>{Locations.E3, Locations.E5, Locations.F4}},
                { Locations.F4,new List<string>{Locations.E4, Locations.G4, Locations.F6, Locations.F2}},
                { Locations.G4,new List<string>{Locations.G1, Locations.G7, Locations.F4}},
                { Locations.C3,new List<string>{Locations.C4, Locations.D3}},
                { Locations.D3,new List<string>{Locations.C3, Locations.E3, Locations.D2}},
                { Locations.E3,new List<string>{Locations.D3, Locations.E4}},
                { Locations.B2,new List<string>{Locations.B4, Locations.D2}},
                { Locations.D2,new List<string>{Locations.B2, Locations.F2, Locations.D3, Locations.D1}},
                { Locations.F2,new List<string>{Locations.F4, Locations.D2}},
                { Locations.A1,new List<string>{Locations.A4, Locations.D1}},
                { Locations.D1,new List<string>{Locations.A1, Locations.G1, Locations.D2}},
                { Locations.G1,new List<string>{Locations.D1, Locations.G4}},
            };
            _rowMillsLocations = new List<List<string>>
            {
                new List<string>{Locations.A7, Locations.D7, Locations.G7},
                new List<string>{Locations.B6, Locations.D6, Locations.F6},
                new List<string>{Locations.C5, Locations.D5, Locations.E5},
                new List<string>{Locations.A4, Locations.B4, Locations.C4},
                new List<string>{Locations.E4, Locations.F4, Locations.G4},
                new List<string>{Locations.C3, Locations.D3, Locations.E3},
                new List<string>{Locations.B2, Locations.D2, Locations.F2},
                new List<string>{Locations.A1, Locations.D1, Locations.G1},
            };
            _columnMillsLocations = new List<List<string>>
            {
                new List<string>{Locations.A7, Locations.A4, Locations.A1},
                new List<string>{Locations.B6, Locations.B4, Locations.B2},
                new List<string>{Locations.C5, Locations.C4, Locations.C3},
                new List<string>{Locations.D7, Locations.D6, Locations.D5},
                new List<string>{Locations.D3, Locations.D2, Locations.D1},
                new List<string>{Locations.E5, Locations.E4, Locations.E3},
                new List<string>{Locations.F6, Locations.F4, Locations.F2},
                new List<string>{Locations.G7, Locations.G4, Locations.G1},
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

        public int CountMills(Color player)
        {
            int mills = 0;
            foreach (var row in _rowMillsLocations)
            {
                bool isMill = true;
                foreach (var tile in row)
                {
                    if (_board[tile]?.Color != player)
                    {
                        isMill = false;
                    }
                }
                if (isMill)
                {
                    mills++;
                }
            }
            foreach (var row in _columnMillsLocations)
            {
                bool isMill = true;
                foreach (var tile in row)
                {
                    if (_board[tile]?.Color != player)
                    {
                        isMill = false;
                    }
                }
                if (isMill)
                {
                    mills++;
                }
            }

            return mills;
        }

        public IEnumerable<PossibleMove> GetPossibleMoves(Color player)
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
                return  _board.Values.Where(x => x != null && x.Color == player).SelectMany(x => GetPossibleMoves(x));
            }

            throw new InvalidOperationException();
        }

        private IEnumerable<PossibleMove> GetPossibleMoves(Piece piece)
        {
            if (Game.GameStatus == GameStatus.Middle)
            {
                return _neighbours[piece.Location].Where(x => _board[x] == null).Select(x => new PossibleMove{From = piece.Location, To = x, MoveType = MoveType.Normal});
            }

            if (Game.GameStatus == GameStatus.Last)
            {
                List<PossibleMove> possibleMoves = new List<PossibleMove>();
                foreach (var key in _board.Keys)
                {
                    if (_board[key] == null)
                    {
                        possibleMoves.Add(new PossibleMove { From = piece.Location, To = key, MoveType = MoveType.Normal });
                    }
                }
                return possibleMoves;
            }
            throw new InvalidOperationException();
        }
    }
}