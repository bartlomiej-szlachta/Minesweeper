using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.dto;

namespace Minesweeper.model
{
    /// <summary>
    /// Silnik gry.
    /// </summary>
    internal class GameEngine
    {
        #region Properties
        internal bool IsGameFinished { get { throw new NotImplementedException(); } }
        internal bool IsResultPositive { get { throw new NotImplementedException(); } }
        internal int Width { get { throw new NotImplementedException(); } }
        internal int Height { get { throw new NotImplementedException(); } }
        #endregion

        internal void StartNewGame(GameModeEnum mode)
        {
            throw new NotImplementedException();
        }

        internal void OpenField(int x, int y)
        {
            throw new NotImplementedException();
        }

        internal void MarkField(int x, int y)
        {
            throw new NotImplementedException();
        }

        internal int GetValue(int x, int y)
        {
            throw new NotImplementedException();
        }

        internal bool GetMarked(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
