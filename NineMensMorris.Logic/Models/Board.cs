using System.Collections.Generic;
using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class Board
    {
        private Dictionary<string, Piece> _board;

        public Board()
        {
            _board = new Dictionary<string, Piece>()
            {
                {Locations.A8, null}, {Locations.B8, null}, {Locations.C8, null}, {Locations.D8,null}, {Locations.E8, null}, {Locations.F8, null}, {Locations.G8, null}, {Locations.H8, null},
                {Locations.A7,null}, {Locations.B7,  null}, {Locations.C7, null}, {Locations.D7,null}, {Locations.E7,  null}, {Locations.F7, null}, {Locations.G7,null}, {Locations.H7,  null},
                {Locations.A6, null}, {Locations.B6, null}, {Locations.C6, null}, {Locations.D6, null}, {Locations.E6, null}, {Locations.F6, null}, {Locations.G6, null}, {Locations.H6, null},
                {Locations.A5, null}, {Locations.B5, null}, {Locations.C5, null}, {Locations.D5, null}, {Locations.E5, null}, {Locations.F5, null}, {Locations.G5, null}, {Locations.H5, null},
                {Locations.A4, null}, {Locations.B4, null}, {Locations.C4, null}, {Locations.D4, null}, {Locations.E4, null}, {Locations.F4, null}, {Locations.G4, null}, {Locations.H4, null},
                {Locations.A3, null}, {Locations.B3, null}, {Locations.C3, null}, {Locations.D3, null}, {Locations.E3, null}, {Locations.F3, null}, {Locations.G3, null}, {Locations.H3, null},
                {Locations.A2, null}, {Locations.B2, null}, {Locations.C2, null}, {Locations.D2, null}, {Locations.E2, null}, {Locations.F2, null}, {Locations.G2, null}, {Locations.H2, null},
                {Locations.A1,null}, {Locations.B1, null}, {Locations.C1, null}, {Locations.D1, null}, {Locations.E1, null}, {Locations.F1, null}, {Locations.G1, null}, {Locations.H1, null}
            };
        }
    }
}