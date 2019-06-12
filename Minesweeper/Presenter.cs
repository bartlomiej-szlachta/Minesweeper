using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.model;

namespace Minesweeper
{
    /// <summary>
    /// Prezenter modelu MVP.
    /// </summary>
    internal class Presenter
    {
        private IEngine engine;
        private IView view;

        /// <summary>
        /// Konstrukor.
        /// </summary>
        /// <param name="engine">Obiekt silnika gry</param>
        /// <param name="view">Obiekt widoku</param>
        internal Presenter(IEngine engine, IView view)
        {
            this.engine = engine;
            this.view = view;
            view.RequestStartNewGame += HandleStartNewGame;
            view.RequestOpenField += HandleOpenField;
            view.RequestMarkOrUnmarkField += HandleMarkOrUnmarkField;
            engine.GameStarted += HandleGameStarted;
            engine.FieldOpened += HandleFieldOpened;
            engine.FieldMarkedOrUnmarked += HandleFieldMarkedOrUnmarked;
            engine.GameFinished += HandleGameFinished;
        }

        /// <summary>
        /// Metoda obsługująca event żądania rozpoczęcia nowej gry.
        /// </summary>
        /// <param name="difficultyName">Nazwa poziomu trudności nowej gry</param>
        private void HandleStartNewGame(string difficultyName)
        {
            try
            {
                engine.StartNewGame(difficultyName);
            }
            catch(Exception ex)
            {
                view.SetGameError(ex.Message);
            }
        }

        /// <summary>
        /// Metoda obsługująca event żądania otwarcia pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma otwieranego pola</param>
        /// <param name="y">Współrzędna pionowa otwieranego pola</param>
        private void HandleOpenField(int x, int y)
        {
            try
            {
                engine.OpenField(x, y);
            }
            catch(Exception ex)
            {
                view.SetGameError(ex.Message);
            }
        }

        /// <summary>
        /// Metoda obsługująca event żądania zaznaczenia / odznaczenia pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma zaznaczanego / odznaczanego pola</param>
        /// <param name="y">Współrzędna pionowa zaznaczanego / odznaczanego pola</param>
        private void HandleMarkOrUnmarkField(int x, int y)
        {
            try
            {
                engine.MarkOrUnmarkField(x, y);
            }
            catch(Exception ex)
            {
                view.SetGameError(ex.Message);
            }
        }

        /// <summary>
        /// Metoda obsługująca event wykonania rozpoczęcia nowej gry.
        /// </summary>
        /// <param name="width">Szerokość planszy</param>
        /// <param name="height">Wysokość planszy</param>
        /// <param name="numberOfBombs">Ilość bomb na planszy</param>
        private void HandleGameStarted(int width, int height, int numberOfBombs)
        {
            view.Initialize(width, height, numberOfBombs);
        }

        /// <summary>
        /// Metoda obsługująca event wykonania otwarcia pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma otwieranego pola</param>
        /// <param name="y">Współrzędna pionowa otwieranego pola</param>
        /// <param name="value">Wartość otwartego pola</param>
        private void HandleFieldOpened(int x, int y, int value)
        {
            view.SetOpened(x, y, value);
        }

        /// <summary>
        /// Metoda obsługująca event wykonania zaznaczenia / odznaczenia pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma zaznaczanego / odznaczanego pola</param>
        /// <param name="y">Współrzędna pionowa zaznaczanego / odznaczanego pola</param>
        /// <param name="marked">Wartość logiczna informująca o stanie zaznaczenia pola</param>
        /// <param name="bombsRemaining">Ilość pozostałych bomb do zlokalizowania</param>
        private void HandleFieldMarkedOrUnmarked(int x, int y, bool marked, int bombsRemaining)
        {
            view.SetMarked(x, y, marked, bombsRemaining);
        }

        /// <summary>
        /// Metoda obsługująca event zakończenia gry.
        /// </summary>
        /// <param name="result">Wartość logiczna informujaca o pozytywnym rezultacie gry</param>
        private void HandleGameFinished(bool result)
        {
            view.SetGameResult(result);
        }
    }
}
