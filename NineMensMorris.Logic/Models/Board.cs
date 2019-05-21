using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Exceptions;
using NineMensMorris.Logic.Extensions;

namespace NineMensMorris.Logic.Models
{
    public class Board
    {
        public int Value { get; set; }
        public IDictionary<string, Piece> BoardState => DeepCopyBoard(_board);

        private readonly IDictionary<string, Piece> _board;
        public static readonly IDictionary<string, List<string>> Neighbours;
        public static readonly IList<List<string>> RowMillsLocations;
        public static readonly IList<List<string>> ColumnMillsLocations;


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

        public Board(Board board)
        {
            _board = board.BoardState;
        }

        static Board()
        {
            Neighbours = new Dictionary<string, List<string>>()
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
            RowMillsLocations = new List<List<string>>
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
            ColumnMillsLocations = new List<List<string>>
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

        public IEnumerable<string> GetLocations()
        {
            return _board.Keys;
        }

        public List<Piece> GetPlayerPieces(Color color)
        {
            return _board.Values.Where(x => x != null && x.Color == color).ToList();
        }

        public MoveResult Move(string from, string to, Color currentPlayer)
        {
            if (GameConfiguration.GameStatus(currentPlayer) == GameStatus.Initialization)
            {
                var existingPiece = GetPiece(to);
                if (existingPiece != null)
                {
                    throw new IllegalMoveException($"Illegal move! Location {to} already contains piece.");
                }

                var pieceTo = new Piece(currentPlayer, to);
                SetPiece(to, pieceTo);
                var mills = GetMills(to,currentPlayer);
                if (mills.Count>0)
                {
                    return new MoveResult(this, MoveType.NewMill, currentPlayer);
                }
                return new MoveResult(this, MoveType.AddPiece, currentPlayer);
            }

            if (!IsMoveValid(currentPlayer, from, to))
            {
                throw new InvalidMoveException($"Illegal move! Cannot make move from {from} to {to}");
            }
            var piece = GetPiece(from);
            SetPiece(from, null);
            SetPiece(to, piece);
            var newMills = GetMills(to, currentPlayer);
            if (newMills.Count > 0)
            {
                return new MoveResult(this, MoveType.NewMill, currentPlayer);
            }
            return new MoveResult(this, MoveType.Normal, currentPlayer);
        }

        public IList<PossibleMove> GetPossibleMoves(Color player)
        {
            if (GameConfiguration.GameStatus(player) == GameStatus.Initialization)
            {
                List<PossibleMove> possibleMovesInit = new List<PossibleMove>();
                foreach (var key in _board.Keys)
                {
                    if (_board[key] == null)
                    {
                        possibleMovesInit.Add(new PossibleMove { To = key, MoveType = MoveType.AddPiece });
                    }
                }
                return possibleMovesInit;
            }

            return GetPlayerPieces(player).SelectMany(x => GetPossibleMoves(x)).ToList();                     
        }

        private IList<PossibleMove> GetPossibleMoves(Piece piece)
        {
            if (_board[piece.Location].Location != piece.Location)
            {
                throw new InvalidOperationException($"WRONG");
            }
            if (GameConfiguration.GameStatus(piece.Color) == GameStatus.BlackLastStage || GameConfiguration.GameStatus(piece.Color) == GameStatus.WhiteLastStage)
            {
                List<PossibleMove> possibleMoves = new List<PossibleMove>();
                foreach (var key in _board.Keys)
                {
                    if (_board[key] == null)
                    {
                        possibleMoves.Add(new PossibleMove { From = piece.Location, To = key });
                    }
                }
                return possibleMoves;
            }

            return Board.Neighbours[piece.Location].Where(x => _board[x] == null).Select(x => new PossibleMove { From = piece.Location, To = x, MoveType = MoveType.Normal}).ToList();
        }

        private bool IsMoveValid(Color player, string from, string to)
        {
            if (!_board.ContainsKey(from) || !_board.ContainsKey(to))
            {
                return false;
            }

            if (_board[to] != null || _board[from] == null || _board[from]?.Color != player)
            {
                return false;
            }

            if (GameConfiguration.GameStatus(player) == GameStatus.Middle)
            {
                var row = GetRow(from);
                var column = GetColumn(from);
                return row.Contains(to) || column.Contains(to);
            }

            if (GameConfiguration.GameStatus(player) == GameStatus.WhiteLastStage || GameConfiguration.GameStatus(player) == GameStatus.BlackLastStage)
            {
                return true;
            }

            return false;
        }

        public void SetPiece(string location, Piece piece)
        {
            if (!_board.ContainsKey(location))
            {
                throw new InvalidMoveException($"Location {location} does not exist.");
            }
            if (piece != null)
            {
                piece.Location = location;
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
            foreach (var row in RowMillsLocations)
            {
                var playerPieces = row.Where(x => _board[x]?.Color == player);
                if (playerPieces.Count() == 3)
                {
                    mills++;
                }
            }
            foreach (var column in ColumnMillsLocations)
            {
                var playerPieces = column.Where(x => _board[x]?.Color == player);
                if (playerPieces.Count() == 3)
                {
                    mills++;
                }
            }

            return mills;
        }

        private List<Mill> GetMills(string location, Color color)
        {
            List<Mill> mills = new List<Mill>();
            var row = GetRow(location);
            var column = GetColumn(location);
            var piecesInRow = row.Where(x => _board[x]?.Color == color);
            var piecesInColumn = column.Where(x => _board[x]?.Color == color);
            if (piecesInRow.Count() == 3)
            {
                mills.Add(new Mill{Locations = row.ToList()});
            }
            if (piecesInColumn.Count() == 3)
            {
                mills.Add(new Mill{Locations = column.ToList()});
            }
            return mills;
        }

        private IEnumerable<string> GetRow(string location)
        {
            List<string> row = null;
            for (int i = 0; i < RowMillsLocations.Count && row == null; i++)
            {
                if (RowMillsLocations[i].Contains(location))
                {
                    row = RowMillsLocations[i];
                }
            }
            return row;
        }
        private IEnumerable<string> GetColumn(string location)
        {
            List<string> column = null;
            for (int i = 0; i < ColumnMillsLocations.Count && column == null; i++)
            {
                if (ColumnMillsLocations[i].Contains(location))
                {
                    column = ColumnMillsLocations[i];
                }
            }
            return column;
        }

        public IEnumerable<AlmostMill> GetAlmostMills(Color player)
        {
            List<AlmostMill> mills = new List<AlmostMill>();
            foreach (var row in RowMillsLocations)
            {
                var playersPiecesInRow = row.Where(x => _board[x]?.Color == player);
                if (playersPiecesInRow.Count() >= 2)
                {
                    mills.Add(new AlmostMill {Position = Position.Row, Tiles = playersPiecesInRow.ToList()});
                }
            }
            foreach (var column in ColumnMillsLocations)
            {
                var playersPiecesInColumn = column.Where(x => _board[x]?.Color == player);
                if (playersPiecesInColumn.Count() >= 2)
                {
                    mills.Add(new AlmostMill { Position = Position.Column, Tiles = playersPiecesInColumn.ToList() });
                }
            }
            return mills;
        }

        private IDictionary<string, Piece> DeepCopyBoard(IDictionary<string, Piece> board)
        {
            var newBoard = new Dictionary<string, Piece>()
            {
                {Locations.A7,null}, {Locations.D7,null},{Locations.G7,null},
                {Locations.B6, null},  {Locations.D6, null},{Locations.F6, null},
                {Locations.C5, null}, {Locations.D5, null}, {Locations.E5, null},
                {Locations.A4, null}, {Locations.B4, null}, {Locations.C4, null}, {Locations.E4, null}, {Locations.F4, null}, {Locations.G4, null},
                {Locations.C3, null}, {Locations.D3, null}, {Locations.E3, null},
                {Locations.B2, null}, {Locations.D2, null}, {Locations.F2, null},
                {Locations.A1,null}, {Locations.D1, null}, {Locations.G1, null},
            };
            foreach (var tile in board.Values.Where(x => x != null))
            {
                newBoard[tile.Location] = new Piece(tile.Color, tile.Location);       
            }
            return newBoard;
        }
    }
}