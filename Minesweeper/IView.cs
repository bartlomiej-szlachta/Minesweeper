using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.dto;

namespace Minesweeper
{
    /// <summary>
    /// Interfejs określający funkcjonalność widoku.
    /// </summary>
    internal interface IView
    {
        /// <summary>
        /// Event reprezentujący żądanie rozpoczęcia nowej gry.
        /// </summary>
        event Action<GameModeEnum> RequestStartNewGame;

        /// <summary>
        /// Event reprezentujący żądanie otwarcia pola.
        /// </summary>
        event Action<int, int> RequestOpenField;

        /// <summary>
        /// Event reprezentujący żądanie zaznaczenia / odznaczenia pola.
        /// </summary>
        event Action<int, int> RequestMarkOrUnmarkField;

        /// <summary>
        /// Metoda ustawiająca wszystkie pola planszy jednocześnie.
        /// </summary>
        /// <param name="fields">Informacje o polach planszy</param>
        void LoadGame(FieldResponse[][] fields);

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
        void SetMarked(int x, int y, bool marked);

        /// <summary>
        /// Metoda informująca o sukcesie lub porażce rozgrywki.
        /// </summary>
        /// <param name="success">Wartość logiczna reprezentująca pozytywny rezultat rozgrywki</param>
        void SetGameResult(bool success);

        /// <summary>
        /// Metoda informująca o błędzie.
        /// </summary>
        /// <param name="message">Treść komunikatu</param>
        void SetGameError(string message);
    }
}
