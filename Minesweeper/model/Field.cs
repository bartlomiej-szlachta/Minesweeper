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
        /// <summary>
        /// Wartość pola - łączna liczba bomb na sąsiednich polach.
        /// </summary>
        internal int Value { get; set; }

        /// <summary>
        /// Informacja o istnieniu bomby na tym polu.
        /// </summary>
        internal bool IsABomb { get; set; }

        /// <summary>
        /// Informacja o zaznaczeniu pola przez gracza.
        /// </summary>
        internal bool IsMarked { get; set; }

        /// <summary>
        /// Informacja o tym, czy pole jest otwarte.
        /// </summary>
        internal bool IsOpened { get; set; }
    }
}
