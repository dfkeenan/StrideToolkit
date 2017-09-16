using SiliconStudio.Xenko.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit.Engine
{
    /// <summary>
    /// Extensions for <see cref="IGame"/>
    /// </summary>
    public static class GameExtensions
    {
        /// <summary>
        /// Gets the elapsed total seconds since last update.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>Update elapsed total seconds.</returns>
        public static float GetDeltaTime(IGame game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            return (float)game.UpdateTime.Elapsed.TotalSeconds;
        }
    }
}
