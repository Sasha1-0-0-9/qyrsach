using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace SudokuApplication
{
    abstract class AbstractBoard
    {

        private Cell[,] _boardCells;

        private Coordinate[][] _rowCells;
        private Coordinate[][] _columnCells;
        private Coordinate[][] _blockCells;

        private HashSet<INumberObserver> _numberObservers = new HashSet<INumberObserver>();


        public AbstractBoard()
        {
            _boardCells = new Cell[GetBoardSize(), GetBoardSize()];

            _rowCells = new Coordinate[GetBoardSize()][];
            _columnCells = new Coordinate[GetBoardSize()][];
            _blockCells = new Coordinate[GetBoardSize()][];

            for (int i = 0; i != GetBoardSize(); i++)
            {
                _rowCells[i] = new Coordinate[GetBoardSize()];
                _columnCells[i] = new Coordinate[GetBoardSize()];
                _blockCells[i] = new Coordinate[GetBoardSize()];
            }

            for (int i = 0; i != GetBoardSize(); i++)
            {
                for (int j = 0; j != GetBoardSize(); j++)
                {
                    _boardCells[i, j] = new Cell();
                    Coordinate cell = new Coordinate(i, j);
                    _rowCells[i][j] = cell;
                    _columnCells[j][i] = cell;
                    _blockCells[GetBlockPosition(i, j)][GetCellPosition(i, j)] = cell;
                }
            }

        }

        public abstract int GetBoardSize();

        public abstract int GetBlockWidth();

        public abstract int GetBlockHeight();

        public void Clear(bool completely = false)
        {
            for (int i = 0; i != GetBoardSize(); i++)
            {
                for (int j = 0; j != GetBoardSize(); j++)
                {
                    if (!IsNumberBlank(i, j))
                    {
                        ClearNumber(i, j, completely);
                    }
                }
            }
        }

        public void ConvertExistingNumbersToPredefined()
        {
            for (int i = 0; i != GetBoardSize(); i++)
            {
                for (int j = 0; j != GetBoardSize(); j++)
                {
                    if (!IsNumberBlank(i, j))
                    {
                        SetNumber(i, j, GetNumber(i, j), true);
                    }
                }
            }
        }


        public IEnumerable<Coordinate> GetRowCells(int row)
        {
            return _rowCells[row];}

        public IEnumerable<Coordinate> GetColumnCells(int column)
        {
            return _columnCells[column];
        }

        public IEnumerable<Coordinate> GetBlockCells(int position)
        {
            return _blockCells[position];
        }

        public IEnumerable<Coordinate> GetBlockCells(int row, int column)
        {
            return GetBlockCells(GetBlockPosition(row, column));
        }


        public int GetNumber(int row, int column)
        {
            return _boardCells[row, column].Value;
        }

        public void SetNumber(int row, int column, int value)
        {
            if (!IsNumberPredefined(row, column))
            {
                SetNumber(row, column, value, false);
            }
        }

        public void SetNumber(int row, int column, int value, bool predefined)
        {
            if (value >= 1 && value <= GetBoardSize())
            {
                _boardCells[row, column].Value = value;
                _boardCells[row, column].Blank = false;
                _boardCells[row, column].Predefined = predefined;

                NotifyNumberObservers(new Coordinate(row, column));
            }
            else
            {
                ClearNumber(row, column);
            }
        }

        public void ClearNumber(int row, int column, bool completely = false)
        {
            if (completely || !IsNumberPredefined(row, column))
            {
                _boardCells[row, column].Value = 0;
                _boardCells[row, column].Blank = true;

                if (completely)
                    _boardCells[row, column].Predefined = false;

                NotifyNumberObservers(new Coordinate(row, column));
            }
        }

        public bool IsNumberBlank(int row, int column)
        {
            return _boardCells[row, column].Blank;
        }

        public bool IsNumberPredefined(int row, int column)
        {
            return _boardCells[row, column].Predefined;
        }


        public int GetBlockPosition(int row, int column)
        {
            int horizontalBlockPosition = column / GetBlockWidth();
            int verticalBlockPosition = row / GetBlockHeight();

            return (GetBoardSize() / GetBlockWidth()) * verticalBlockPosition + horizontalBlockPosition;
        }

        public int GetCellPosition(int row, int column)
        {
            int horizontalCellPosition = column % GetBlockWidth();
            int verticalCellPosition = row % GetBlockHeight();

            return GetBlockWidth() * verticalCellPosition + horizontalCellPosition;
        }


        public void AddNumberObserver(INumberObserver observer)
        {
            _numberObservers.Add(observer);
        }

        public void RemoveNumberObserver(INumberObserver observer)
        {
            _numberObservers.Remove(observer);
        }

        public void NotifyNumberObservers(Coordinate cell)
        {
            foreach (INumberObserver observer in _numberObservers)
            {
                observer.UpdateNumber(cell);
            }
        }
    }
}
