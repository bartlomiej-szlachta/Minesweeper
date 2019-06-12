using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.model.difficultymodes
{
    /// <summary>
    /// Klasa reprezentująca zaawansowany poziom trudności.
    /// </summary>
    internal class ExpertDifficultyMode : IDifficultyMode
    {
        public string Name => "expert";

        public int Height => 16;

        public int Width => 30;

        public int NumberOfBombs => 99;
    }
}
