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

        public event Action RequestStartNewGame;
        public event Action<int, int> RequestOpenField;
        public event Action<int, int> RequestMarkOrUnmarkField;

        public void LoadGame(FieldResponse[][] fields)
        {
            throw new NotImplementedException();
        }

        public void SetField(FieldResponse field)
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
    }
}
