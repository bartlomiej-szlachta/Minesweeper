using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.dto;

namespace Minesweeper.view
{
    /// <summary>
    /// Główny formularz widoku.
    /// </summary>
    internal partial class MainForm : Form, IView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public event Action<GameModeEnum> RequestStartNewGame;
        public event Action<int, int> RequestOpenField;
        public event Action<int, int> RequestMarkOrUnmarkField;

        public void Initialize(int width, int height)
        {
            throw new NotImplementedException();
        }

        public void SetGameError(string message)
        {
            throw new NotImplementedException();
        }

        public void SetGameResult(bool success)
        {
            throw new NotImplementedException();
        }

        public void SetMarked(int x, int y, bool marked)
        {
            throw new NotImplementedException();
        }

        public void SetOpened(int x, int y, int value)
        {
            throw new NotImplementedException();
        }
    }
}
