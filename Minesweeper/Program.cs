using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.view;
using Minesweeper.model;

namespace Minesweeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IView view = new MainForm();
            GameEngine engine = new GameEngine();
            Presenter presenter = new Presenter(engine, view);

            Application.Run((Form)view);
        }
    }
}
