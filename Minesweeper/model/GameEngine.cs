using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.model.difficultymodes;

namespace Minesweeper.model
{
    /// <summary>
    /// Silnik gry.
    /// </summary>
    internal class GameEngine
    {
        #region Private fields

        private Board board;

        #endregion

        #region Properties

        /// <summary>
        /// Informacja o tym, czy gra została zakończona.
        /// </summary>
        internal bool IsGameFinished { get { return board.IsFinished; } }

        /// <summary>
        /// Informacja o tym, czy zakończona gra została wygrana.
        /// </summary>
        internal bool IsResultPositive { get { return board.IsResultPositive; } }

        /// <summary>
        /// Szerokość planszy.
        /// </summary>
        internal int Width { get { return board.Width; } }

        /// <summary>
        /// Wysokość planszy.
        /// </summary>
        internal int Height { get { return board.Height; } }

        /// <summary>
        /// Liczba bomb pozostałych do końca gry.
        /// </summary>
        internal int BombsRemaining { get { return board.GetBombsRemaining(); } }
        #endregion

        #region Methods

        /// <summary>
        /// Metoda rozpoczynająca nową grę.
        /// </summary>
        /// <param name="difficultyName">Nazwa poziomu trudności nowej gry</param>
        internal void StartNewGame(string difficultyName)
        {
            switch (difficultyName)
            {
                case "beginner":
                    {
                        board = new Board(new BeginnerDifficultyMode());
                    }
                    break;
                case "intermediate":
                    {
                        board = new Board(new IntermediateDifficultyMode());
                    }
                    break;
                case "expert":
                    {
                        board = new Board(new ExpertDifficultyMode());
                    }
                    break;
            }
        }

        /// <summary>
        /// Metoda otwierająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        internal void OpenField(int x, int y)
        {
            board.OpenField(x, y);
        }

        /// <summary>
        /// Metoda zaznaczająca / odznaczająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        internal void MarkOrUnmarkField(int x, int y)
        {
            board.MarkOrUnmarkField(x, y);
        }

        /// <summary>
        /// Metoda uzyskująca wartość danego pola - sumę bomb na sąsiednich polach.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        /// <returns>Wartość pola</returns>
        internal int GetValue(int x, int y)
        {
            return board.GetValue(x, y);
        }

        /// <summary>
        /// Metoda uzyskująca stan zaznaczenia danego pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        /// <returns>True w przypadku, gdy pole jest zaznaczone, false w przeciwnym przypadku</returns>
        internal bool GetMarked(int x, int y)
        {
            return board.GetMarked(x, y);
        }

        #endregion
    }
}
