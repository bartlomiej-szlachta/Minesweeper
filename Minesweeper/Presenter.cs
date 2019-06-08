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
        }
    }
}
