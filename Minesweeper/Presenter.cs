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
            view.RequestStartNewGame += HandleStartNewGame;
            view.RequestOpenField += HandleOpenField;
            view.RequestMarkOrUnmarkField += HandleMarkOrUnmarkField;
        }

        /// <summary>
        /// Metoda obsługująca event rozpoczęcia nowej gry.
        /// </summary>
        /// <param name="difficultyName">Nazwa poziomu trudności nowej gry</param>
        private void HandleStartNewGame(string difficultyName)
        {
            try
            {
                engine.StartNewGame(difficultyName);
                view.Initialize(engine.Width, engine.Height, engine.BombsRemaining);
            }
            catch (Exception ex)
            {
                view.SetGameError(ex.Message);
            }
        }

        /// <summary>
        /// Metoda obsługująca event otwarcia pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma otwieranego pola</param>
        /// <param name="y">Współrzędna pionowa otwieranego pola</param>
        private void HandleOpenField(int x, int y)
        {
            try
            {
                engine.OpenField(x, y);
                if (engine.IsGameFinished)
                {
                    for (int i = 0; i < engine.Height; i++)
                    {
                        for (int j = 0; j < engine.Width; j++)
                        {
                            view.SetOpened(j, i, engine.GetValue(j, i));
                            view.SetMarked(j, i, engine.GetMarked(j, i), engine.BombsRemaining);
                        }
                    }
                    view.SetGameResult(engine.IsResultPositive);
                }
                else
                {
                    view.SetOpened(x, y, engine.GetValue(x, y));
                }
            }
            catch (Exception ex)
            {
                view.SetGameError(ex.Message);
            }
        }

        /// <summary>
        /// Metoda obsługująca event zaznaczenia / odznaczenia pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma zaznaczanego / odznaczanego pola</param>
        /// <param name="y">Współrzędna pionowa zaznaczanego / odznaczanego pola</param>
        private void HandleMarkOrUnmarkField(int x, int y)
        {
            try
            {
                engine.MarkField(x, y);
                view.SetMarked(x, y, engine.GetMarked(x, y), engine.BombsRemaining);
            }
            catch(Exception ex)
            {
                view.SetGameError(ex.Message);
            }
        }
    }
}
