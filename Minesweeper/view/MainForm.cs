using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper.view
{
    /// <summary>
    /// Główny formularz widoku.
    /// </summary>
    internal partial class MainForm : Form, IView
    {
        #region Private fields

        /// <summary>
        /// Wymiar jednego pola planszy (w pikselach).
        /// </summary>
        private const int FIELD_SIZE = 25;

        /// <summary>
        /// Nazwa aktywnego poziomu trudności.
        /// </summary>
        private string currentDifficultyMode;

        /// <summary>
        /// Szerokość planszy.
        /// </summary>
        private int boardWidth;

        /// <summary>
        /// Wysokość planszy.
        /// </summary>
        private int boardHeight;

        /// <summary>
        /// Tablica zdjęć przedstawiających otwarte pola o wartościach numerycznych lub pustym.
        /// </summary>
        private Image[] numberFieldImages;

        /// <summary>
        /// Zdjęcie przedstawiające otwarte pole z bombą.
        /// </summary>
        private Image bombFieldImage;

        /// <summary>
        /// Zdjęcie przedstawiające zakryte, niezaznaczone pole.
        /// </summary>
        private Image hiddenFieldImage;

        /// <summary>
        /// Zdjęcie przedstawiające zakryte, zaznaczone pole.
        /// </summary>
        private Image markedFieldImage;

        /// <summary>
        /// Obiekt umożliwiający rysowanie na panelu.
        /// </summary>
        private Graphics canvas;

        #endregion

        #region Constructor

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            numberFieldImages = new Image[9];
            for (int i = 0; i < 9; i++)
            {
                numberFieldImages[i] = new Bitmap(i + ".png");
            }
            bombFieldImage = new Bitmap("-1.png");
            hiddenFieldImage = new Bitmap("hidden.png");
            markedFieldImage = new Bitmap("marked.png");
            canvas = boardPanel.CreateGraphics();
            infoControl1.SetLabel("Bombs remaining:");
            infoControl1.SetValue(0);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Metoda transformująca współrzędne punktu na panelu na współrzędne na planszy.
        /// </summary>
        /// <param name="clickedX">Współrzędna pozioma z panelu</param>
        /// <param name="clickedY">Współrzędna pionowa z panelu</param>
        /// <returns>Współrzędne na planszy</returns>
        private Point GetLocationInBoard(int clickedX, int clickedY)
        {
            int fieldWidthPixels = boardPanel.Width / boardWidth;
            int fieldHeightPixels = boardPanel.Height / boardHeight;
            int resultX = clickedX / fieldWidthPixels + 1;
            int resultY = clickedY / fieldHeightPixels + 1;
            return new Point(resultX, resultY);
        }

        /// <summary>
        /// Metoda transformująca współrzędne punktu na planszy na współrzędne na panelu.
        /// </summary>
        /// <param name="boardX">Współrzędna pozioma z planszy</param>
        /// <param name="boardY">Współrzędna pionowa z planszy</param>
        /// <returns>Współrzędne górnego lewego wierzchołka na panelu</returns>
        private Point GetLocationInPanel(int boardX, int boardY)
        {
            int fieldWidthPixels = boardPanel.Width / boardWidth;
            int fieldHeightPixels = boardPanel.Height / boardHeight;
            int resultX = (boardX - 1) * fieldHeightPixels;
            int resultY = (boardY - 1) * fieldWidthPixels;
            return new Point(resultX, resultY);
        }
        
        #endregion

        #region IView members

        public event Action<string> RequestStartNewGame;
        public event Action<int, int> RequestOpenField;
        public event Action<int, int> RequestMarkOrUnmarkField;

        public void Initialize(int width, int height, int numberOfBombs)
        {
            infoControl1.SetValue(numberOfBombs);
            boardHeight = height;
            boardWidth = width;
            boardPanel.Height = boardHeight * FIELD_SIZE;
            boardPanel.Width = boardWidth * FIELD_SIZE;
            for (int y = 1; y <= boardHeight; y++)
            {
                for (int x = 1; x <= boardWidth; x++)
                {
                    Point location = GetLocationInPanel(x, y);
                    canvas.DrawImage(hiddenFieldImage, location.X, location.Y, FIELD_SIZE, FIELD_SIZE);
                }
            }
        }

        public void SetOpened(int x, int y, int value)
        {
            Point location = GetLocationInPanel(x, y);
            if (value == -1)
            {
                canvas.DrawImage(bombFieldImage, location.X, location.Y, FIELD_SIZE, FIELD_SIZE);
            }
            else
            {
                canvas.DrawImage(numberFieldImages[value], location.X, location.Y, FIELD_SIZE, FIELD_SIZE);
            }
        }

        public void SetMarked(int x, int y, bool marked, int bombsRemaining)
        {
            Point location = GetLocationInPanel(x, y);
            if (marked)
            {
                canvas.DrawImage(markedFieldImage, location.X, location.Y, FIELD_SIZE, FIELD_SIZE);
            }
            else
            {
                canvas.DrawImage(hiddenFieldImage, location.X, location.Y, FIELD_SIZE, FIELD_SIZE);
            }
            infoControl1.SetValue(bombsRemaining);
        }

        public void SetGameResult(bool success)
        {
            DialogResult result = MessageBox.Show(success ? "Zwycięstwo!" : "Porażka", "Koniec gry", MessageBoxButtons.OK);
            if (result != DialogResult.None)
            {
                RequestStartNewGame?.Invoke(currentDifficultyMode);
            }
        }

        public void SetGameError(string message)
        {
            DialogResult result = MessageBox.Show(message, "Error", MessageBoxButtons.OK);
            if (result != DialogResult.None)
            {
                RequestStartNewGame?.Invoke(currentDifficultyMode);
            }
        }

        #endregion

        #region Controls event handlers

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentDifficultyMode = "beginner";
            RequestStartNewGame?.Invoke(currentDifficultyMode);
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentDifficultyMode = "intermediate";
            RequestStartNewGame?.Invoke(currentDifficultyMode);
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentDifficultyMode = "expert";
            RequestStartNewGame?.Invoke(currentDifficultyMode);
        }

        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Point location = GetLocationInBoard(e.X, e.Y);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        RequestOpenField?.Invoke(location.X, location.Y);
                    }
                    break;
                case MouseButtons.Right:
                    {
                        RequestMarkOrUnmarkField?.Invoke(location.X, location.Y);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
