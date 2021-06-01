using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication.Classes
{
    class Export
    {
        public static string ParseDataFromBoard(AbstractBoard board)
        {
            StringBuilder text = new StringBuilder();

            for (int i = 0; i != board.GetBoardSize(); i++)
            {
                if (i > 0)
                    text.Append(Environment.NewLine);

                for (int j = 0; j != board.GetBoardSize(); j++)
                {
                    if (j > 0)
                        text.Append(",");

                    if (!board.IsNumberBlank(i, j))
                    {
                        text.Append(board.GetNumber(i, j));
                        text.Append(board.IsNumberPredefined(i, j) ? "T" : "F");
                    }
                }


            }

            return text.ToString();
        }
    }
}
