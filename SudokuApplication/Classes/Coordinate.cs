using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    public class Coordinate : IEquatable<Coordinate>
    {

        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Equals(Coordinate coordinate)
        {
            return (this.Row == coordinate.Row) && (this.Column == coordinate.Column);
        }
    }
}
