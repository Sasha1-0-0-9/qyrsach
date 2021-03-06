using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Imaging;

using SudokuApplication.Properties;

namespace SudokuApplication
{
    public partial class SudokuForm : Form, INumberObserver
    {
        private Sudoku _sudoku;
        private Padding _applicationMargin;
        private string _filePath;
        private bool _unsavedChanges = false;

        private Bitmap _bitmap;
        private int _cellSize;
        private Padding _boardMargin;
        private int _boardSize;
        private int _blockWidth;
        private int _blockHeight;
        private List<int>[,] _notes;

        private Coordinate _selectedCell;
        private StringBuilder _valueBuffer = new StringBuilder();
        private bool _noteFlag = false;

        public SudokuForm()
        {
            InitializeComponent();

            _sudoku = new Sudoku();
            _applicationMargin = new Padding(0, menuStrip.Height, 0, 0);
            _cellSize = 48;
            _boardMargin = new Padding(15);

            GenerateBoard(9, 1);
        }


        private Coordinate CalculateClickedCell(int x, int y)
        {
            x -= _applicationMargin.Left;
            y -= _applicationMargin.Top;

            if (x < _boardMargin.Left || x >= (_bitmap.Width - _boardMargin.Right) ||
                y < _boardMargin.Top || y >= (_bitmap.Height - _boardMargin.Bottom))
            {
                return null;
            }

            int row = (y - _boardMargin.Top) / _cellSize;
            int column = (x - _boardMargin.Left) / _cellSize;

            return new Coordinate(row, column);
        }

        private Rectangle CalculateRectangleForCell(int row, int column)
        {
            int width = _cellSize;
            int height = _cellSize;
            int x = _applicationMargin.Left + _boardMargin.Left + column * width;
            int y = _applicationMargin.Top + _boardMargin.Top + row * height;

            return new Rectangle(x, y, width, height);
        }

        private void CommitNumber()
        {
            if (_valueBuffer.Length == 0)
                return;

            bool wasCompleted = _sudoku.IsSectionCompleted(_selectedCell.Row, _selectedCell.Column);

            int value = Convert.ToInt32(_valueBuffer.ToString());
            _valueBuffer.Clear();

            if (!_noteFlag)
            {
                _sudoku.SetNumber(_selectedCell.Row, _selectedCell.Column, value);
            }
            else
            {
                if (value >= 1 && value <= _boardSize)
                {
                    if (!_notes[_selectedCell.Row, _selectedCell.Column].Contains(value))
                        _notes[_selectedCell.Row, _selectedCell.Column].Add(value);
                    else
                        _notes[_selectedCell.Row, _selectedCell.Column].Remove(value);
                }

                UpdateCells(_selectedCell);
            }

            if (wasCompleted)
                UpdateBoard();

            _unsavedChanges = true;
        }


        private bool ConfirmEndGamePrompt()
        {
            if (_unsavedChanges)
            {
                DialogResult endResult = MessageBox.Show(Resources.EndGameDescription, Resources.EndGameTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return (endResult == DialogResult.No);
            }

            return false;
        }

        private bool ConfirmExitPrompt()
        {
            if (_unsavedChanges)
            {
                DialogResult result = MessageBox.Show(Resources.ConfirmExitDescription, Resources.ConfirmExitTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return (result == DialogResult.No);
            }

            return false;
        }

        private void BoardCompleteAlert()
        {
            MessageBox.Show(Resources.BoardCompleteDescription, Resources.BoardCompleteTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InvalidBoardAlert()
        {
            MessageBox.Show(Resources.InvalidBoardDescription, Resources.InvalidBoardTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void InvalidImportAlert()
        {
            MessageBox.Show(Resources.InvalidImportDescription, Resources.InvalidImportTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void UnsolvableBoardAlert()
        {
            MessageBox.Show(Resources.UnsolvableBoardDescription, Resources.UnsolvableBoardTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        public void CreateBlankBoard(int boardSize)
        {
            if (ConfirmEndGamePrompt())
                return;
            if (!_sudoku.NewGame(boardSize))
                return;

            InitializeBoard();

        }

        public void GenerateBoard(int boardSize, int difficulty)
        {
            if (ConfirmEndGamePrompt())
                return;
            if (!_sudoku.NewGame(boardSize, difficulty))
                return;

            InitializeBoard();
        }
        private void InitializeBoard()
        {
            _sudoku.SubscribeToNumberChanges(this);

            _boardSize = _sudoku.GetBoardSize();
            _blockWidth = _sudoku.GetBlockWidth();
            _blockHeight = _sudoku.GetBlockHeight();

            _notes = new List<int>[_boardSize, _boardSize];
            _selectedCell = null;

            for (int i = 0; i != _boardSize; i++)
            {
                for (int j = 0; j != _boardSize; j++)
                {
                    _notes[i, j] = new List<int>();
                }
            }

            UpdateBoard();

            int borderWidth = (this.Width - this.ClientSize.Width) / 2;
            int titlebarHeight = this.Height - this.ClientSize.Height - 2 * borderWidth;

            this.Width = _applicationMargin.Left + _bitmap.Width + _applicationMargin.Right + 2 * borderWidth;
            this.Height = _applicationMargin.Top + _bitmap.Height + _applicationMargin.Bottom + titlebarHeight + 2 * borderWidth;

            _unsavedChanges = false;
        }



        private void SudokuForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphicsObject = e.Graphics;
            graphicsObject.DrawImage(_bitmap, _applicationMargin.Left, _applicationMargin.Top, _bitmap.Width, _bitmap.Height);
            graphicsObject.Dispose();
        }

        private void SaveBoard(string filePath)
        {
            try
            {
                string boardData = _sudoku.ExportBoard();
                File.WriteAllText(filePath, boardData);

                // Save file path
                UpdateFilePath(filePath);

                // Reset unsaved changes
                _unsavedChanges = false;
            }
            catch (IOException)
            { }
        }

        private void UpdateFilePath(string filePath)
        {
            // Store filepath
            _filePath = filePath;

            // Update title bar
            if (_filePath != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                this.Text = String.Format("{0} - {1}", fileName, Resources.ApplicationName);
            }
            else
                this.Text = Resources.ApplicationName;
        }

        private void UpdateBoard()
        {
            int bitmapWidth = _boardMargin.Left + _cellSize * _boardSize + _boardMargin.Right;
            int bitmapHeight = _boardMargin.Top + _cellSize * _boardSize + _boardMargin.Bottom;
            _bitmap = new Bitmap(bitmapWidth, bitmapHeight, PixelFormat.Format24bppRgb);
            Graphics graphicsContext = Graphics.FromImage(_bitmap);

            graphicsContext.Clear(BackColor);

            for (int row = 0; row != _boardSize; row++)
            {
                for (int column = 0; column != _boardSize; column++)
                {
                    DrawCell(row, column);
                }
            }

            Pen outerStroke = new Pen(Color.Black, 3);
            Rectangle boardRectangle = new Rectangle(_boardMargin.Left, _boardMargin.Top, _boardSize * _cellSize, _boardSize * _cellSize);
            graphicsContext.DrawRectangle(outerStroke, boardRectangle);

            graphicsContext.Dispose();

            Invalidate();
        }

        private void UpdateCells(params Coordinate[] cells)
        {
            HashSet<Coordinate> affectedBlocks = new HashSet<Coordinate>();

            foreach (Coordinate cell in cells)
            {
                if (cell == null)
                    continue;

                DrawCell(cell.Row, cell.Column);

                Invalidate(CalculateRectangleForCell(cell.Row, cell.Column));
            }
        }

        private void DrawCell(int row, int column)
        {
            SolidBrush selectedCellBrush = new SolidBrush(Color.Beige);
            Pen innerStroke = new Pen(Color.Black, 1);

            Rectangle valueRectangle = new Rectangle(_boardMargin.Left + column * _cellSize, _boardMargin.Top + row * _cellSize, _cellSize, _cellSize);


            Graphics graphicsContext = Graphics.FromImage(_bitmap);

            if (_selectedCell != null && row == _selectedCell.Row && column == _selectedCell.Column)
                graphicsContext.FillRectangle(selectedCellBrush, valueRectangle);
            else
                graphicsContext.FillRectangle(new SolidBrush(Color.White), valueRectangle);

            if (_selectedCell != null && row == _selectedCell.Row && column == _selectedCell.Column && _valueBuffer.Length > 0)
            {
                int value = Convert.ToInt32(_valueBuffer.ToString());
                if (!_noteFlag)
                {
                    SolidBrush valueBrush = new SolidBrush(Color.Black);
                    DrawNumber(valueRectangle, value, valueBrush);
                }
                else
                {
                    foreach (int noteValue in _notes[row, column])
                    {
                        DrawNote(valueRectangle, noteValue);
                    }

                    DrawNote(valueRectangle, value);
                }
            }
            else
            {
                if (!_sudoku.IsNumberBlank(row, column))
                {
                    SolidBrush predefinedBrush = new SolidBrush(Color.Blue);
                    SolidBrush valueBrush = new SolidBrush(Color.Black);
                    SolidBrush errorBrush = new SolidBrush(Color.Red);
                    SolidBrush validatedBrush = new SolidBrush(Color.Green);
                    int value = _sudoku.GetNumber(row, column);
                    bool predefined = _sudoku.IsNumberPredefined(row, column);
                    SolidBrush currentBrush = (predefined) ? predefinedBrush : valueBrush;

                    bool errorReporting = Properties.Settings.Default.IndicateWrongNumbers;
                    bool validationReporting = Properties.Settings.Default.IndicateValidatedSections;

                    DrawNumber(valueRectangle, value, currentBrush);
                }
                else
                {
                    foreach (int noteValue in _notes[row, column])
                    {
                        DrawNote(valueRectangle, noteValue);
                    }
                }
            }

            graphicsContext.DrawRectangle(innerStroke, valueRectangle);
            DrawBlockStroke(row, column);


            graphicsContext.Dispose();
        }

        private void DrawNumber(Rectangle rectangle, int value, SolidBrush textColor)
        {
            Font valueFont = new Font("Tahoma", 24.0f);
            StringFormat valueFormat = new StringFormat();
            valueFormat.Alignment = StringAlignment.Center;
            valueFormat.LineAlignment = StringAlignment.Center;

            Graphics graphicsContext = Graphics.FromImage(_bitmap);
            graphicsContext.DrawString(value.ToString(), valueFont, textColor, rectangle, valueFormat);
            graphicsContext.Dispose();
        }

        private void DrawNote(Rectangle rectangle, int noteValue)
        {
            SolidBrush noteBrush = new SolidBrush(Color.Black);
            Font noteFont = new Font("Tahoma", 6.0f);
            StringFormat noteFormat = new StringFormat();
            noteFormat.Alignment = StringAlignment.Center;
            noteFormat.LineAlignment = StringAlignment.Center;
            int noteWidth = _cellSize / _blockWidth;
            int noteHeight = _cellSize / _blockHeight;
            int noteLeftMargin = (_cellSize - noteWidth * _blockWidth) / 2;
            int noteTopMargin = (_cellSize - noteHeight * _blockHeight) / 2;

            int noteRow = (noteValue - 1) / _blockWidth;
            int noteColumn = (noteValue - 1) % _blockWidth;

            Rectangle noteRectangle = new Rectangle(rectangle.Left + noteLeftMargin + noteWidth * noteColumn, rectangle.Top + noteTopMargin + noteHeight * noteRow, noteWidth, noteHeight);

            Graphics graphicsContext = Graphics.FromImage(_bitmap);
            graphicsContext.DrawString(noteValue.ToString(), noteFont, noteBrush, noteRectangle, noteFormat);
            graphicsContext.Dispose();
        }

        private void DrawBlockStroke(int row, int column)
        {
            Pen outerStroke = new Pen(Color.Black, 3);

            if ((column % _blockWidth) == 0 || (column % _blockWidth) == (_blockWidth - 1))
            {
                int right = ((column % _blockWidth) == (_blockWidth - 1)) ? _blockWidth : 0;
                int blockWidth = (column / _blockWidth) * _blockWidth + right;

                int x1 = _boardMargin.Left + blockWidth * _cellSize;
                int x2 = x1;
                int y1 = _boardMargin.Top + row * _cellSize;
                int y2 = y1 + _cellSize;

                Point pt1 = new Point(x1, y1);
                Point pt2 = new Point(x2, y2);


                Graphics graphicsContext = Graphics.FromImage(_bitmap);
                graphicsContext.DrawLine(outerStroke, pt1, pt2);
                graphicsContext.Dispose();
            }

            if ((row % _blockHeight) == 0 || (row % _blockHeight) == (_blockHeight - 1))
            {
                int bottom = ((row % _blockHeight) == (_blockHeight - 1)) ? _blockHeight : 0;
                int blockHeight = (row / _blockHeight) * _blockHeight + bottom;

                int x1 = _boardMargin.Left + column * _cellSize;
                int x2 = x1 + _cellSize;
                int y1 = _boardMargin.Top + blockHeight * _cellSize;
                int y2 = y1;

                Point pt1 = new Point(x1, y1);
                Point pt2 = new Point(x2, y2);


                Graphics graphicsContext = Graphics.FromImage(_bitmap);
                graphicsContext.DrawLine(outerStroke, pt1, pt2);
                graphicsContext.Dispose();
            }
        }



        private void SudokuForm_MouseClick(object sender, MouseEventArgs e)
        {
            Coordinate clickedCell = CalculateClickedCell(e.X, e.Y);

            if (clickedCell == null || _sudoku.IsBoardCompleted())
                return;

            CommitNumber();

            Coordinate previousCell = _selectedCell;

            if (_selectedCell == null || !clickedCell.Equals(_selectedCell))
                _selectedCell = clickedCell;
            else
                _selectedCell = null;

            UpdateCells(previousCell, _selectedCell);
        }

        private void SudokuForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (_selectedCell == null || _sudoku.IsBoardCompleted())
                return;

            if (e.KeyValue == 46 || e.KeyValue == 8)
            {
                _valueBuffer.Clear();

                bool wasCompleted = _sudoku.IsSectionCompleted(_selectedCell.Row, _selectedCell.Column);

                _sudoku.ClearNumber(_selectedCell.Row, _selectedCell.Column);

                if (wasCompleted)
                    UpdateBoard();
            }
            else if (e.KeyValue == 27)
            {
                Coordinate previousCell = _selectedCell;

                _valueBuffer.Clear();
                _selectedCell = null;

                UpdateCells(previousCell);
            }

            else if (e.KeyValue >= 37 && e.KeyValue <= 40)
            {
                CommitNumber();

                Coordinate previousCell = _selectedCell;

                switch (e.KeyValue)
                {
                    case 37: // Left arrow
                        _selectedCell = new Coordinate(_selectedCell.Row, Math.Max(_selectedCell.Column - 1, 0));
                        break;
                    case 38: // Up arrow
                        _selectedCell = new Coordinate(Math.Max(_selectedCell.Row - 1, 0), _selectedCell.Column);
                        break;
                    case 39: // Right arrow
                        _selectedCell = new Coordinate(_selectedCell.Row, Math.Min(_selectedCell.Column + 1, _boardSize - 1));
                        break;
                    case 40: // Down arrow
                        _selectedCell = new Coordinate(Math.Min(_selectedCell.Row + 1, _boardSize - 1), _selectedCell.Column);
                        break;
                }

                UpdateCells(previousCell, _selectedCell);
            }

            else if (e.KeyValue == 9)
            {
                CommitNumber();

                Coordinate previousCell = _selectedCell;

                int direction = (!e.Shift) ? 1 : -1;
                int row = _selectedCell.Row;
                int column = _selectedCell.Column + direction;

                if (column < 0 || column >= _boardSize)
                {
                    column = (column + _boardSize) % _boardSize;
                    row += direction;

                    if (row < 0 || row >= _boardSize)
                    {
                        row = (row + _boardSize) % _boardSize;
                    }
                }

                _selectedCell = new Coordinate(row, column);

                UpdateCells(previousCell, _selectedCell);
            }

            else if (e.KeyValue == 13)
            {
                CommitNumber();
            }

            else if ((e.KeyValue >= 48 && e.KeyValue <= 57) ||
                     (e.KeyValue >= 96 && e.KeyValue <= 105))
            {
                if (_sudoku.IsNumberPredefined(_selectedCell.Row, _selectedCell.Column))
                    return;

                int keyValue = (e.KeyValue >= 48 && e.KeyValue <= 57) ? e.KeyValue - 48 : e.KeyValue - 96;

                if (_valueBuffer.Length == 0)
                {
                    _noteFlag = e.Control;

                    if (keyValue == 0)
                        return;
                }

                _valueBuffer.Append(keyValue);

                if (_valueBuffer.Length == _boardSize.ToString().Length)
                    CommitNumber();
                else
                    UpdateCells(_selectedCell);
            }
        }

        private void SudokuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ConfirmExitPrompt())
                e.Cancel = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGameDialog newGame = new NewGameDialog(this);
            newGame.ShowDialog();

        }

        private void LoadBoard(string filePath)
        {
            if (ConfirmEndGamePrompt())
                return;

            try
            {
                string boardData = File.ReadAllText(filePath);
                if (_sudoku.ImportBoard(boardData))
                {
                    InitializeBoard();

                    // Save file path
                    UpdateFilePath(filePath);
                }
                else
                {
                    InvalidImportAlert();
                }
            }
            catch (IOException)
            { }
        }


        public void UpdateNumber(Coordinate cell)
        {
            if (!_sudoku.IsSectionCompleted(cell.Row, cell.Column))
                UpdateCells(cell);
            else
                UpdateBoard();

            if (_sudoku.IsBoardCompleted())
            {
                Coordinate previousCell = _selectedCell;

                _valueBuffer.Clear();
                _selectedCell = null;

                UpdateCells(previousCell);

                _unsavedChanges = false;
                BoardCompleteAlert();
            }
        }

        private void SudokuForm_Load(object sender, EventArgs e)
        {

        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = Path.GetFileName(_filePath);
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                SaveBoard(fileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult openResult = openFileDialog.ShowDialog();
            if (openResult == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                LoadBoard(fileName);
            }
        }
    }
}