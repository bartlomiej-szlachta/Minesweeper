using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.dto;

namespace Minesweeper.model
{
    /// <summary>
    /// Klasa reprezentująca planszę do gry.
    /// </summary>
    internal class Board
    {
        private Field[][] fields;

        internal GameModeEnum Mode { get; set; }
        internal bool IsStarted { get; set; }
        internal bool IsFinished { get; set; }
        internal bool IsResultPositive { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int NumberOfBombs { get; set; }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="width">Szerokość planszy</param>
        /// <param name="height">Wysokość planszy</param>
        /// <param name="numberOfBombs">Ilość bomb na planszy</param>
        internal Board(int width, int height, int numberOfBombs)
        {
            IsStarted = false;
            IsFinished = false;
            Width = width;
            Height = height;
            NumberOfBombs = numberOfBombs;
            fields = new Field[width][];
            for (int i = 0; i < width + 2; i++)
            {
                fields[i] = new Field[height + 2];
                foreach (Field field in fields[i])
                {
                    field.IsOpened = false;
                    field.IsMarked = false;
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
                        || (fields[y][x].IsABomb))
                    {
                        randomiseAgain = true;
                    }
                    else
                    {
                        fields[y][x].IsABomb = true;
                        fields[y][x].Value = -1;
                    }
                } while (randomiseAgain);
            }

            for (int y = 1; y <= Height; y++)
            {
                for (int x = 1; x <= Width; x++)
                {
                    if (!fields[y][x].IsABomb)
                    {
                        int counter = 0;
                        if (fields[y - 1][x - 1].IsABomb) { counter++; }
                        if (fields[y - 1][x].IsABomb) { counter++; }
                        if (fields[y - 1][x + 1].IsABomb) { counter++; }
                        if (fields[y][x - 1].IsABomb) { counter++; }
                        if (fields[y][x + 1].IsABomb) { counter++; }
                        if (fields[y + 1][x - 1].IsABomb) { counter++; }
                        if (fields[y + 1][x].IsABomb) { counter++; }
                        if (fields[y + 1][x + 1].IsABomb) { counter++; }
                        fields[y][x].Value = counter;
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

            if (fields[y][x].IsOpened)
            {
                throw new Exception("The field is already opened.");
            }

            fields[y][x].IsOpened = true;

            if (fields[y][x].IsABomb)
            {
                IsFinished = true;
                IsResultPositive = false;
            }
            else
            {
                if (fields[y][x].Value == 0)
                {
                    OpenBlankAreas();
                }

                bool victory = true;
                for (int yi = 1; yi <= Height; yi++)
                {
                    for (int xi = 1; xi <= Width; xi++)
                    {
                        if (!fields[yi][xi].IsOpened && !fields[yi][xi].IsABomb)
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
            if (fields[y][x].IsMarked)
            {
                fields[y][x].IsMarked = false;
            }
            if (!fields[y][x].IsMarked)
            {
                fields[y][x].IsMarked = true;
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
            return fields[y][x].Value;
        }

        /// <summary>
        /// Metoda uzyskująca stan zaznaczenia danego pola.
        /// </summary>
        /// <param name="x">Współrzędna pozioma pola</param>
        /// <param name="y">Współrzędna pionowa pola</param>
        /// <returns>True w przypadku, gdy pole jest zaznaczone, false w przeciwnym przypadku</returns>
        internal bool GetMarked(int x, int y)
        {
            return fields[y][x].IsMarked;
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
                    if (fields[y][x].IsMarked)
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
                        if (fields[y][x].IsOpened && fields[y][x].Value == 0)
                        {
                            fields[y - 1][x - 1].IsOpened = true;
                            fields[y - 1][x].IsOpened = true;
                            fields[y - 1][x + 1].IsOpened = true;
                            fields[y][x - 1].IsOpened = true;
                            fields[y][x + 1].IsOpened = true;
                            fields[y + 1][x - 1].IsOpened = true;
                            fields[y + 1][x].IsOpened = true;
                            fields[y + 1][x + 1].IsOpened = true;

                            zerosNow++;
                        }
                    }
                }
            } while (zerosNow != zerosBefore);
        }
    }    
}
