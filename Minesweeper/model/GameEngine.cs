using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.model.difficultymodes;

namespace Minesweeper.model
{
    /// <summary>
    /// Silnik gry.
    /// </summary>
    internal class GameEngine : IEngine
    {
        #region Private properties

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
        private bool IsFinished { get; set; }

        /// <summary>
        /// Szerokość planszy.
        /// </summary>
        private int Width { get { return Difficulty.Width; } }

        /// <summary>
        /// Wysokość planszy.
        /// </summary>
        private int Height { get { return Difficulty.Height; } }

        /// <summary>
        /// Ilość bomb na planszy.
        /// </summary>
        private int NumberOfBombs { get { return Difficulty.NumberOfBombs; } }

        #endregion

        #region IEngine members

        public event Action<int, int, int> GameStarted;
        public event Action<int, int, int> FieldOpened;
        public event Action<int, int, bool, int> FieldMarkedOrUnmarked;
        public event Action<bool> GameFinished;

        /// <summary>
        /// Metoda rozpoczynająca nową grę.
        /// </summary>
        /// <param name="difficultyName">Nazwa poziomu trudności nowej gry</param>
        public void StartNewGame(string difficultyName)
        {
            switch (difficultyName)
            {
                case "beginner":
                    {
                        Difficulty = new BeginnerDifficultyMode();
                    }
                    break;
                case "intermediate":
                    {
                        Difficulty = new IntermediateDifficultyMode();
                    }
                    break;
                case "expert":
                    {
                        Difficulty = new ExpertDifficultyMode();
                    }
                    break;
            }
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
            GameStarted?.Invoke(Width, Height, NumberOfBombs);
        }

        /// <summary>
        /// Metoda otwierająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma otwieranego pola</param>
        /// <param name="y">Współrzędna pionowa otwieranego pola</param>
        public void OpenField(int x, int y)
        {
            if (!IsStarted)
            {
                Fill(x, y);
                IsStarted = true;
            }

            if (!Fields[y][x].IsMarked)
            {
                Fields[y][x].IsOpened = true;
                FieldOpened?.Invoke(x, y, Fields[y][x].Value);

                if (Fields[y][x].IsABomb)
                {
                    IsFinished = true;
                    GameFinished?.Invoke(false);
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
                        GameFinished?.Invoke(true);
                    }
                }
            }
        }

        /// <summary>
        /// Metoda zaznaczająca / odznaczająca wybrane pole.
        /// </summary>
        /// <param name="x">Współrzędna pozioma zaznaczanego / odznaczanego pola</param>
        /// <param name="y">Współrzędna pionowa zaznaczanego / odznaczanego pola</param>
        public void MarkOrUnmarkField(int x, int y)
        {
            if (!Fields[y][x].IsOpened)
            {
                Fields[y][x].IsMarked = !Fields[y][x].IsMarked;
                FieldMarkedOrUnmarked?.Invoke(x, y, Fields[y][x].IsMarked, GetBombsRemaining());
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Metoda losująca położenia bomb oraz inicjalizująca planszę.
        /// </summary>
        /// <param name="x">Współrzędna pozioma wybranego pola</param>
        /// <param name="y">Współrzędna pionowa wybranego pola</param>
        private void Fill(int startX, int startY)
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
        }

        /// <summary>
        /// Pomocnicza metoda zwracająca liczbę pozostałych bomb do zlokalizowania.
        /// </summary>
        /// <returns>Liczba pozostałych bomb do zlokalizowania</returns>
        private int GetBombsRemaining()
        {
            int numberOfBombsAlreadyLocalised = 0;
            for (int y = 1; y <= Height; y++)
            {
                for (int x = 1; x <= Width; x++)
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
                            FieldOpened?.Invoke(x - 1, y - 1, Fields[y - 1][x - 1].Value);
                            Fields[y - 1][x].IsOpened = true;
                            FieldOpened?.Invoke(x, y - 1, Fields[y - 1][x].Value);
                            Fields[y - 1][x + 1].IsOpened = true;
                            FieldOpened?.Invoke(x + 1, y - 1, Fields[y - 1][x + 1].Value);
                            Fields[y][x - 1].IsOpened = true;
                            FieldOpened?.Invoke(x - 1, y, Fields[y][x - 1].Value);
                            Fields[y][x + 1].IsOpened = true;
                            FieldOpened?.Invoke(x + 1, y, Fields[y][x + 1].Value);
                            Fields[y + 1][x - 1].IsOpened = true;
                            FieldOpened?.Invoke(x - 1, y + 1, Fields[y + 1][x - 1].Value);
                            Fields[y + 1][x].IsOpened = true;
                            FieldOpened?.Invoke(x, y + 1, Fields[y + 1][x].Value);
                            Fields[y + 1][x + 1].IsOpened = true;
                            FieldOpened?.Invoke(x + 1, y + 1, Fields[y + 1][x + 1].Value);

                            zerosNow++;
                        }
                    }
                }
            } while (zerosNow != zerosBefore);
        }

        #endregion
    }
}
