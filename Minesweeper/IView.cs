using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    /// <summary>
    /// Interfejs określający funkcjonalność widoku.
    /// </summary>
    internal interface IView
    {
        /// <summary>
        /// Event reprezentujący żądanie rozpoczęcia nowej gry o danym poziomie trudności.
        /// </summary>
        event Action<string> RequestStartNewGame;

        /// <summary>
        /// Event reprezentujący żądanie otwarcia danego pola.
        /// </summary>
        event Action<int, int> RequestOpenField;

        /// <summary>
        /// Event reprezentujący żądanie zaznaczenia / odznaczenia danego pola.
        /// </summary>
        event Action<int, int> RequestMarkOrUnmarkField;

        /// <summary>
        /// Metoda tworząca planszę.
        /// </summary>
        /// <param name="width">Szerokość planszy</param>
        /// <param name="height">Wysokość planszy</param>
        /// <param name="numberOfBombs">Ilość bomb na planszy</param>
        void Initialize(int width, int height, int numberOfBombs);

        /// <summary>
        /// Metoda otwierająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma otwieranego pola</param>
        /// <param name="y">Współrzędna pionowa otwieranego pola</param>
        /// <param name="value">Wartość odkrytego pola</param>
        void SetOpened(int x, int y, int value);

        /// <summary>
        /// Metoda zaznaczająca / odznaczająca pole planszy.
        /// </summary>
        /// <param name="marked"></param>
        /// <param name="x">Współrzędna poziona zaznaczanego / odznaczanego pola</param>
        /// <param name="y">Współrzędna pionowa zaznaczanego / odznaczanego pola</param>
        /// <param name="bombsRemaining">Liczba pozostałych bomb do zlokalizowania</param>
        void SetMarked(int x, int y, bool marked, int bombsRemaining);

        /// <summary>
        /// Metoda informująca o sukcesie lub porażce rozgrywki.
        /// </summary>
        /// <param name="success">Wartość logiczna reprezentująca pozytywny rezultat rozgrywki</param>
        void SetGameResult(bool success);

        /// <summary>
        /// Metoda informująca o błędzie działania aplikacji.
        /// </summary>
        /// <param name="message">Treść komunikatu</param>
        void SetGameError(string message);
    }
}
