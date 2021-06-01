using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SudokuApplication
{
    class Generator
    {

        AbstractBoard _board;
        int Backtracking = 0;

        public Generator(AbstractBoard board)
        {
            _board = board;
        }

        private static readonly Random random = new Random();
        public static int RandomNumberGen(int min, int max)
        {            
            { 
                return random.Next(min, max);
            }
        }

        private void fillBoard(int row, int column, ref List<int>[,] triedNumbers)
        {             
            
            List<int> possibleValuesInCell;
            do
            { 

                _board.ClearNumber(row, column);
                possibleValuesInCell = new List<int>().GenerateAllPossibleValues(_board.GetBoardSize());
                RemoveInvalidValuesFromCell(ref possibleValuesInCell, row, column);
                foreach (int triedNumber in triedNumbers[row, column]) 
                {
                    possibleValuesInCell.Remove(triedNumber);
                }


                if (possibleValuesInCell.Count == 0)
                {
                    triedNumbers[row, column].Clear();
                    int prevRow;
                    int prevColumn;
                    if (column == 0)
                    {
                        prevRow = row - 1;
                        prevColumn = _board.GetBoardSize() - 1;
                    }
                    else
                    {
                        prevRow = row;
                        prevColumn = column - 1;
                        
                    }
                    fillBoard(prevRow, prevColumn, ref triedNumbers);
                }

                else
                {
                    int randomIndex = RandomNumberGen(0, possibleValuesInCell.Count);  
                    int randomNumber = possibleValuesInCell.ElementAt<int>(randomIndex);

                    _board.SetNumber(row, column, randomNumber, false); 
                    triedNumbers[row, column].Add(randomNumber);
                }

                Backtracking = Backtracking + 1;
            } while (possibleValuesInCell.Count == 0);                        
        }

        public void Generate(int difficulty)
        {
            bool maxBacktacking;
            do
            {
                maxBacktacking = false;
                Backtracking = 0;
                _board.Clear();
                List<int>[,] triedNumbers = new List<int>[_board.GetBoardSize(), _board.GetBoardSize()];
                for (int i = 0; i < _board.GetBoardSize(); i++)
                {
                    for (int j = 0; j < _board.GetBoardSize(); j++)
                    {
                        if (Backtracking > 40000)
                        {
                            maxBacktacking = true;
                            break;
                        }
                        triedNumbers[i, j] = new List<int>();
                        fillBoard(i, j, ref triedNumbers);
                        
                    }
                    if (maxBacktacking)
                        break;
                }
                
            } while (maxBacktacking);

            switch (_board.GetBoardSize())
            { 
                case 4:
                    remove(4, 6, 8, 10, difficulty);
                    break;
                case 6:
                    remove(12, 18, 24, 30, difficulty);
                    break;
                case 9:
                    remove(13, 30, 47, 64, difficulty);
                    break;
                case 12:
                    remove(40, 60, 80, 100, difficulty);
                    break;
                case 16:
                    remove(50, 75, 100, 150, difficulty);
                    break;
            }

            _board.ConvertExistingNumbersToPredefined();   
            
        }

        private void remove(int easy, int medium, int hard, int expert, int difficulty)
        {          
            switch (difficulty)
            {
                case 0:
                    RemoveNumbersInCell(easy);
                    break;
                case 1:
                    RemoveNumbersInCell(medium);
                    break;
                case 2:
                    RemoveNumbersInCell(hard);
                    break;
                case 3:
                    RemoveNumbersInCell(expert);
                    break;               
            }            
        }
                
        private void RemoveNumbersInCell( int CellsToClear) 
        {           
            int j = 0;
            do
            {
                int randomRow = RandomNumberGen(0, _board.GetBoardSize());
                int randomColumn = RandomNumberGen(0, _board.GetBoardSize());

                if (!_board.IsNumberBlank(randomRow, randomColumn))
                {
                    _board.ClearNumber(randomRow, randomColumn);
                    j++;
                }                
            } while (j < CellsToClear);            
        }

        private void RemoveInvalidValuesFromCell(ref List<int> possibleValuesInCell, int row, int column)
        {
            RemoveInvalidValuesFromCellForSection(ref possibleValuesInCell, _board.GetRowCells(row));
            RemoveInvalidValuesFromCellForSection(ref possibleValuesInCell, _board.GetColumnCells(column));
            RemoveInvalidValuesFromCellForSection(ref possibleValuesInCell, _board.GetBlockCells(row, column));
        }

        private void RemoveInvalidValuesFromCellForSection(ref List<int> possibleValuesInCell, IEnumerable<Coordinate> cells)
        {
            foreach (Coordinate cell in cells)
            {
                int value = _board.GetNumber(cell.Row, cell.Column);
                possibleValuesInCell.Remove(value);
            }
        }
    }
}
