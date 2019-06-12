using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper.view
{
    public partial class InfoControl : UserControl
    {
        public InfoControl()
        {
            InitializeComponent();
        }

        internal void SetLabel(string label)
        {
            Label.Text = label;
        }

        internal void SetValue(int value)
        {
            Value.Text = value.ToString();
        }
    }
}
