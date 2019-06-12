using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.model.difficultymodes
{
    /// <summary>
    /// Klasa reprezentująca średniozaawansowany tryb trudności.
    /// </summary>
    internal class IntermediateDifficultyMode : IDifficultyMode
    {
        public string Name => "intermediate";

        public int Height => 16;

        public int Width => 16;

        public int NumberOfBombs => 40;
    }
}
