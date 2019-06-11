using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.dto;

namespace Minesweeper.model
{
    internal class Board
    {
        private Field[][] fields;

        internal GameModeEnum Mode { get; set; }
        internal bool IsFinished { get; set; }
        internal bool IsResultPositive { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int NumberOfBombs { get; set; }

        internal Board(int width, int height, int numberOfBombs)
        {
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
        internal void RandomizeAndInitialize(int startX, int startY)
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
        }
        
    }
}
