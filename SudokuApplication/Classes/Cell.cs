using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace SudokuApplication
{
    class Cell 
    {

        public int Value { get; set; }
        public bool Blank { get; set; }
        public bool Predefined { get; set; }


        public Cell() : this(0, true, false) { }

        public Cell(int value) : this(value, false, true) { }

        public Cell(int value, bool predefined) : this(value, false, predefined) { }

        protected Cell(int value, bool blank, bool predefined)
        {
            Value = value;
            Blank = blank;
            Predefined = predefined;
        }
    }
}
