using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace SudokuApplication
{
    class Validator
    {

        public static bool ValidateBoard(AbstractBoard board, bool completeCheck = false)
        {
            bool validation = true;

            for (int i = 0; i != board.GetBoardSize(); i++)
            {
                validation &= ValidateSection(board, board.GetRowCells(i), completeCheck);
                validation &= ValidateSection(board, board.GetColumnCells(i), completeCheck);
                validation &= ValidateSection(board, board.GetBlockCells(i), completeCheck);
            }

            return validation;
        }

        public static bool ValidateCell(AbstractBoard board, int row, int column, bool oneSectionIsCompleted = false)
        {
            bool rowValidation = ValidateSection(board, board.GetRowCells(row), oneSectionIsCompleted);
            bool columnValidation = ValidateSection(board, board.GetColumnCells(column), oneSectionIsCompleted);
            bool blockValidation = ValidateSection(board, board.GetBlockCells(row, column), oneSectionIsCompleted);
            bool andResult = (rowValidation && columnValidation && blockValidation);
            bool orResult = (rowValidation || columnValidation || blockValidation);

            return (!oneSectionIsCompleted) ? andResult : orResult;
        }

        public static bool ValidateSection(AbstractBoard board, IEnumerable<Coordinate> cells, bool completeCheck = false)
        {
            HashSet<int> previousValues = new HashSet<int>();
            foreach (Coordinate cell in cells)
            {
                int value = board.GetNumber(cell.Row, cell.Column);

                if (board.IsNumberBlank(cell.Row, cell.Column))
                {
                    if (completeCheck)
                        return false;
                    else
                        continue;
                }

                if (previousValues.Contains(value))
                    return false;

                previousValues.Add(value);
            }
            return true;
        }
    }
}
