using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.model.difficultymodes
{
    /// <summary>
    /// Klasa reprezentująca niski poziom trudności.
    /// </summary>
    internal class BeginnerDifficultyMode : IDifficultyMode
    {
        public string Name => "beginner";

        public int Height => 9;

        public int Width => 9;

        public int NumberOfBombs => 10;
    }
}
