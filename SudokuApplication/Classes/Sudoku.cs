using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class Sudoku
    {
        /* Fields */

        private AbstractBoard _board;

        // Checks if a section is completed
        public bool IsSectionCompleted(int row, int column)
        {
            return Validator.ValidateCell(_board, row, column, true);
        }

        public bool IsBoardCompleted()
        {
            return Validator.ValidateBoard(_board, true);
        }

        // Checks if all cells are valid
        public bool IsBoardValid()
        {
            return Validator.ValidateBoard(_board, false);
        }


        /* Game */
        public bool NewGame(int size, int difficulty)
        {
            if (NewGame(size))
            {
                Generator generator = new Generator(_board);
                generator.Generate(difficulty);

                return true;
            }

            return false;
        }


        public bool NewGame(int size)
        {
            _board = BoardFactory.CreateBoard(size);
             return true;
        }

        // Locks the current numbers and makes them predefined
        public void LockNumbers()
        {
            if (Validator.ValidateBoard(_board))
            {
                _board.ConvertExistingNumbersToPredefined();
            }

        }

        public int GetBoardSize()
        {
            return _board.GetBoardSize();
        }

        public int GetBlockWidth()
        {
            return _board.GetBlockWidth();
        }

        public int GetBlockHeight()
        {
            return _board.GetBlockHeight();
        }

        public int GetNumber(int row, int column)
        {
            return _board.GetNumber(row, column);
        }

        public void SetNumber(int row, int column, int value)
        {
                _board.SetNumber(row, column, value);
        }

        public void ClearNumber(int row, int column)
        {
                _board.ClearNumber(row, column);
        }

        public bool IsNumberBlank(int row, int column)
        {
                return _board.IsNumberBlank(row, column);
        }

        public bool IsNumberPredefined(int row, int column)
        {
                return _board.IsNumberPredefined(row, column);
        }

        /* Subscriptions */

        public void SubscribeToNumberChanges(INumberObserver observer)
        {
            _board.AddNumberObserver(observer);
        }

        public void UnsubscribeToNumberChanges(INumberObserver observer)
        {
            _board.RemoveNumberObserver(observer);
        }
    }
}
