using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    /// <summary>
    /// Interfejs określający funkcjonalność silnika gry.
    /// </summary>
    internal interface IEngine
    {
        /// <summary>
        /// Event informujący o rozpoczęciu nowej gry.
        /// </summary>
        event Action<int, int, int> GameStarted;

        /// <summary>
        /// Event informujący o otwarciu pola.
        /// </summary>
        event Action<int, int, int> FieldOpened;

        /// <summary>
        /// Event informujący o zaznaczeniu / odznaczeniu pola.
        /// </summary>
        event Action<int, int, bool, int> FieldMarkedOrUnmarked;

        /// <summary>
        /// Event informujący o zakończeniu gry z określonym skutkiem.
        /// </summary>
        event Action<bool> GameFinished;

        /// <summary>
        /// Metoda rozpoczynająca nową grę.
        /// </summary>
        /// <param name="difficultyName">Nazwa poziomu trudności nowej gry</param>
        void StartNewGame(string difficultyName);

        /// <summary>
        /// Metoda otwierająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma otwieranego pola</param>
        /// <param name="y">Współrzędna pionowa otwieranego pola</param>
        void OpenField(int x, int y);

        /// <summary>
        /// Metoda zaznaczająca / odznaczająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma zaznaczanego / odznaczanego pola</param>
        /// <param name="y">Współrzędna pionowa zaznaczanego / odznaczanego pola</param>
        void MarkOrUnmarkField(int x, int y);
    }
}
