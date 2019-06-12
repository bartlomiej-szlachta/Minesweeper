using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.model
{
    /// <summary>
    /// Interfejs określający parametry stopnia trudności gry.
    /// </summary>
    internal interface IDifficultyMode
    {
        /// <summary>
        /// Nazwa poziomu trudności.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Wysokość planszy.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Szerokość planszy.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Ilość bomb na planszy przy rozpoczęciu gry.
        /// </summary>
        int NumberOfBombs { get; }
    }
}
