using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.model
{
    /// <summary>
    /// Struktura reprezentująca pojedyncze pole na planszy.
    /// </summary>
    internal class Field
    {
        internal int Value { get; set; }
        internal bool IsABomb { get; set; }
        internal bool IsMarked { get; set; }
        internal bool IsOpened { get; set; }
    }
}
