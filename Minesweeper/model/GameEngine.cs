using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.dto;

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
        internal int BombsRemaining { get { throw new NotImplementedException(); } }
        #endregion

        #region Methods

        /// <summary>
        /// Metoda rozpoczynająca nową grę.
        /// </summary>
        /// <param name="mode">Tryb nowej gry</param>
        /// <param name="x">Współrzędna pozioma startowego pola</param>
        /// <param name="y">Współrzędna pionowa startowego pola</param>
        internal void StartNewGame(GameModeEnum mode, int x, int y)
        {
            switch (mode)
            {
                case GameModeEnum.EASY:
                    {
                        board = new Board(9, 9, 10);
                    }
                    break;
                case GameModeEnum.MEDIUM:
                    {
                        board = new Board(16, 16, 40);
                    }
                    break;
                case GameModeEnum.HARD:
                    {
                        board = new Board(30, 16, 99);
                    }
                    break;
                default:
                    break;
            }
            board.Mode = mode;
        }

        /// <summary>
        /// Metoda otwierająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        internal void OpenField(int x, int y)
        {
            if (!board.IsStarted)
            {
                board.Fill(x, y);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metoda zaznaczająca / odznaczająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        internal void MarkField(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metoda uzyskująca wartość danego pola - sumę bomb na sąsiednich polach.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        /// <returns>Wartość pola</returns>
        internal int GetValue(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metoda uzyskująca stan zaznaczenia danego pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        /// <returns>True w przypadku, gdy pole jest zaznaczone, false w przeciwnym przypadku</returns>
        internal bool GetMarked(int x, int y)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
