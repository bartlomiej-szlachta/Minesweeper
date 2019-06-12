using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.model
{
    /// <summary>
    /// Klasa reprezentująca planszę do gry.
    /// </summary>
    internal class Board
    {
        #region Properties

        /// <summary>
        /// Tabela pól planszy.
        /// </summary>
        private Field[][] Fields { get; set; }

        /// <summary>
        /// Poziom trudności.
        /// </summary>
        private IDifficultyMode Difficulty { get; set; }

        /// <summary>
        /// Informacja o tym, czy gra jest rozpoczęta (czy pozycje bomb są wylosowane).
        /// </summary>
        private bool IsStarted { get; set; }

        /// <summary>
        /// Informacja o tym, czy gra jest już zakończona.
        /// </summary>
        internal bool IsFinished { get; private set; }

        /// <summary>
        /// Informacja o tym, czy wynik gry jest pozytywny. Dotyczy tylko zakończonej gry.
        /// </summary>
        internal bool IsResultPositive { get; private set; }

        /// <summary>
        /// Szerokość planszy.
        /// </summary>
        internal int Width { get { return Difficulty.Width; } }

        /// <summary>
        /// Wysokość planszy.
        /// </summary>
        internal int Height { get { return Difficulty.Height; } }

        /// <summary>
        /// Ilość bomb na planszy.
        /// </summary>
        private int NumberOfBombs { get { return Difficulty.NumberOfBombs; } }

        #endregion

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="width">Szerokość planszy</param>
        /// <param name="height">Wysokość planszy</param>
        /// <param name="numberOfBombs">Ilość bomb na planszy</param>
        internal Board(IDifficultyMode difficulty)
        {
            Difficulty = difficulty;
            IsStarted = false;
            IsFinished = false;
            Fields = new Field[Height + 2][];
            for (int y = 0; y < Height + 2; y++)
            {
                Fields[y] = new Field[Width + 2];
                for (int x = 0; x < Width + 2; x++)
                {
                    Fields[y][x] = new Field
                    {
                        Value = 0,
                        IsABomb = false,
                        IsMarked = false,
                        IsOpened = false
                    };
                }
            }
        }

        /// <summary>
        /// Metoda losująca położenia bomb oraz inicjalizująca planszę.
        /// </summary>
        /// <param name="x">Współrzędna pozioma wybranego pola</param>
        /// <param name="y">Współrzędna pionowa wybranego pola</param>
        internal void Fill(int startX, int startY)
        {
            Random random = new Random();
            for (int i = 0; i < NumberOfBombs; i++)
            {
                bool randomiseAgain;
                do
                {
                    randomiseAgain = false;
                    int y = random.Next(Height) + 1;
                    int x = random.Next(Width) + 1;

                    if (((x == startX - 1) && (y == startY - 1))
                        || ((x == startX - 1) && (y == startY))
                        || ((x == startX - 1) && (y == startY + 1))
                        || ((x == startX) && (y == startY - 1))
                        || ((x == startX) && (y == startY))
                        || ((x == startX) && (y == startY + 1))
                        || ((x == startX + 1) && (y == startY - 1))
                        || ((x == startX + 1) && (y == startY))
                        || ((x == startX + 1) && (y == startY + 1))
                        || (Fields[y][x].IsABomb))
                    {
                        randomiseAgain = true;
                    }
                    else
                    {
                        Fields[y][x].IsABomb = true;
                        Fields[y][x].Value = -1;
                    }
                } while (randomiseAgain);
            }

            for (int y = 1; y <= Height; y++)
            {
                for (int x = 1; x <= Width; x++)
                {
                    if (!Fields[y][x].IsABomb)
                    {
                        int counter = 0;
                        if (Fields[y - 1][x - 1].IsABomb) { counter++; }
                        if (Fields[y - 1][x].IsABomb) { counter++; }
                        if (Fields[y - 1][x + 1].IsABomb) { counter++; }
                        if (Fields[y][x - 1].IsABomb) { counter++; }
                        if (Fields[y][x + 1].IsABomb) { counter++; }
                        if (Fields[y + 1][x - 1].IsABomb) { counter++; }
                        if (Fields[y + 1][x].IsABomb) { counter++; }
                        if (Fields[y + 1][x + 1].IsABomb) { counter++; }
                        Fields[y][x].Value = counter;
                    }
                }
            }

            IsStarted = true;
        }

        /// <summary>
        /// Metoda otwierająca wybrane pole planszy.
        /// </summary>
        /// <param name="x">Współrzędna pozioma otwieranego pola</param>
        /// <param name="y">Współrzędna pionowa otwieranego pola</param>
        internal void OpenField(int x, int y)
        {
            if (!IsStarted)
            {
                Fill(x, y);
            }

            if (Fields[y][x].IsOpened)
            {
                throw new Exception("The field is already opened.");
            }

            Fields[y][x].IsOpened = true;

            if (Fields[y][x].IsABomb)
            {
                IsFinished = true;
                IsResultPositive = false;
            }
            else
            {
                if (Fields[y][x].Value == 0)
                {
                    OpenBlankAreas();
                }

                bool victory = true;
                for (int yi = 1; yi <= Height; yi++)
                {
                    for (int xi = 1; xi <= Width; xi++)
                    {
                        if (!Fields[yi][xi].IsOpened && !Fields[yi][xi].IsABomb)
                        {
                            victory = false;
                            break;
                        }
                    }
                }
                if (victory)
                {
                    IsFinished = true;
                    IsResultPositive = true;
                }
            }
        }

        /// <summary>
        /// Metoda zaznaczająca / odznaczająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        internal void MarkField(int x, int y)
        {
            if (Fields[y][x].IsMarked)
            {
                Fields[y][x].IsMarked = false;
            }
            if (!Fields[y][x].IsMarked)
            {
                Fields[y][x].IsMarked = true;
            }
        }

        /// <summary>
        /// Metoda uzyskująca wartość danego pola - sumę bomb na sąsiednich polach.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        /// <returns>Wartość pola</returns>
        internal int GetValue(int x, int y)
        {
            return Fields[y][x].Value;
        }

        /// <summary>
        /// Metoda uzyskująca stan zaznaczenia danego pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        /// <returns>True w przypadku, gdy pole jest zaznaczone, false w przeciwnym przypadku</returns>
        internal bool GetMarked(int x, int y)
        {
            return Fields[y][x].IsMarked;
        }

        /// <summary>
        /// Metoda zwracająca liczbę pozostałych bomb do zlokalizowania.
        /// </summary>
        /// <returns>Liczba pozostałych bomb do zlokalizowania</returns>
        internal int GetBombsRemaining()
        {
            int numberOfBombsAlreadyLocalised = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Fields[y][x].IsMarked)
                    {
                        numberOfBombsAlreadyLocalised++;
                    }
                }
            }
            return NumberOfBombs - numberOfBombsAlreadyLocalised;
        }

        /// <summary>
        /// Pomocnicza metoda odsłaniająca puste fragmenty planszy.
        /// </summary>
        private void OpenBlankAreas()
        {
            int zerosNow = 0;
            int zerosBefore = 0;
            do
            {
                zerosBefore = zerosNow;
                zerosNow = 0;
                for (int y = 1; y <= Height; y++)
                {
                    for (int x = 1; x <= Width; x++)
                    {
                        if (Fields[y][x].IsOpened && Fields[y][x].Value == 0)
                        {
                            Fields[y - 1][x - 1].IsOpened = true;
                            Fields[y - 1][x].IsOpened = true;
                            Fields[y - 1][x + 1].IsOpened = true;
                            Fields[y][x - 1].IsOpened = true;
                            Fields[y][x + 1].IsOpened = true;
                            Fields[y + 1][x - 1].IsOpened = true;
                            Fields[y + 1][x].IsOpened = true;
                            Fields[y + 1][x + 1].IsOpened = true;

                            zerosNow++;
                        }
                    }
                }
            } while (zerosNow != zerosBefore);
        }
    }    
}
