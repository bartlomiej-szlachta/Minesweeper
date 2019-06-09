using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.model;
using Minesweeper.dto;

namespace Minesweeper
{
    /// <summary>
    /// Prezenter modelu MVP.
    /// </summary>
    internal class Presenter
    {
        private GameEngine engine;
        private IView view;

        /// <summary>
        /// Konstrukor.
        /// </summary>
        /// <param name="engine">Obiekt silnika gry</param>
        /// <param name="view">Obiekt widoku</param>
        internal Presenter(GameEngine engine, IView view)
        {
            this.engine = engine;
            this.view = view;
            view.RequestStartNewGame += HandleStartNewGame;
            view.RequestOpenField += HandleOpenField;
            view.RequestMarkOrUnmarkField += HandleMarkOrUnmarkField;
        }

        /// <summary>
        /// Metoda obsługująca event rozpoczęcia nowej gry.
        /// </summary>
        /// <param name="mode">Tryb nowej gry</param>
        private void HandleStartNewGame(GameMode mode)
        {

        }

        /// <summary>
        /// Metoda obsługująca event otwarcia pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma otwieranego pola</param>
        /// <param name="y">Współrzędna pionowa otwieranego pola</param>
        private void HandleOpenField(int x, int y)
        {

        }

        /// <summary>
        /// Metoda obsługująca event zaznaczenia / odznaczenia pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma zaznaczanego / odznaczanego pola</param>
        /// <param name="y">Współrzędna pionowa zaznaczanego / odznaczanego pola</param>
        private void HandleMarkOrUnmarkField(int x, int y)
        {

        }
    }
}
