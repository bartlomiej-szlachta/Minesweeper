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
        event Action RequestStartNewGame;

        /// <summary>
        /// Event reprezentujący żądanie otwarcia pola.
        /// </summary>
        event Action<int, int> RequestOpenField;

        /// <summary>
        /// Event reprezentujący żądanie zaznaczenia / odznaczenia pola.
        /// </summary>
        event Action<int, int> RequestMarkOrUnmarkField;

        /// <summary>
        /// Metoda aktualizująca jedno pole planszy.
        /// </summary>
        /// <param name="field">Informacje o aktualizowanym polu</param>
        void SetField(FieldResponse field);

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
