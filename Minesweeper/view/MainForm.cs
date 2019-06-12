using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper.view
{
    /// <summary>
    /// Główny formularz widoku.
    /// </summary>
    internal partial class MainForm : Form, IView
    {
        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region IView members

        public event Action<string> RequestStartNewGame;
        public event Action<int, int> RequestOpenField;
        public event Action<int, int> RequestMarkOrUnmarkField;

        public void Initialize(int width, int height, int numberOfBombs)
        {
            throw new NotImplementedException();
        }

        public void SetOpened(int x, int y, int value)
        {
            throw new NotImplementedException();
        }

        public void SetMarked(int x, int y, bool marked, int bombsRemaining)
        {
            throw new NotImplementedException();
        }

        public void SetGameResult(bool success)
        {
            DialogResult result = MessageBox.Show(success ? "Zwycięstwo!" : "Porażka", "Koniec gry", MessageBoxButtons.OK);
            if (result != DialogResult.None)
            {
                throw new NotImplementedException();
            }
        }

        public void SetGameError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK);
        }

        #endregion

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestStartNewGame?.Invoke("beginner");
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestStartNewGame?.Invoke("intermediate");
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestStartNewGame?.Invoke("expert");
        }
    }
}
