using SiliconStudio.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit
{
    /// <summary>
    /// Extensions for <see cref="RandomExtensions"/>.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Generates a random <see cref="float"/>.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns>A random <see cref="float"/>.</returns>
        /// <exception cref="ArgumentNullException">If the random is null.</exception>
        public static float NextSingle(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return (float)random.NextDouble();
        }

        /// <summary>
        /// Generates a random point in 2D space within the specified region.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="region">A 2D region in which point is generated.</param>
        /// <returns>A random point in 2D space within the specified region.</returns>
        /// <exception cref="ArgumentNullException">If the random is null.</exception>
        public static Vector2 NextPoint(this Random random, RectangleF region)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return Vector2.Lerp(region.TopLeft, region.BottomRight, random.NextSingle());
            
        }

        /// <summary>
        /// Generates a random point in 3D space within the specified region.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="region">A 3D region in which point is generated.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If the random is null.</exception>
        public static Vector3 NextPoint(this Random random, BoundingBox region)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return Vector3.Lerp(region.Minimum, region.Maximum, random.NextSingle());

        }

        /// <summary>
        /// Generates a random normalized 2D direction vector.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If the random is null.</exception>
        public static Vector2 NextDirection2D(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return Vector2.Normalize(new Vector2(random.NextSingle(), random.NextSingle()));
        }

        /// <summary>
        /// Generates a random normalized 3D direction vector.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If the random is null.</exception>
        public static Vector3 NextDirection3D(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return Vector3.Normalize(new Vector3(random.NextSingle(), random.NextSingle(), random.NextSingle()));
        }

        /// <summary>
        /// Generates a random color.
        /// </summary>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <returns>A random color. Aplha is set to 255. </returns>
        /// <exception cref="ArgumentNullException">If the random is null.</exception>
        public static Color NextColor(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return new Color(NextDirection3D(random));
        }

        /// <summary>
        /// Chooses a random item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="collection">Collection to choose item from.</param>
        /// <returns>A random item from collection</returns>
        /// <exception cref="ArgumentNullException">If the random is null.</exception>
        /// <exception cref="ArgumentNullException">If the collection is null.</exception>
        public static T Choose<T>(this Random random, IList<T> collection)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection[random.Next(collection.Count)];
        }

        /// <summary>
        /// Chooses a random item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="collection">Collection to choose item from.</param>
        /// <returns>A random item from collection</returns>
        /// <exception cref="ArgumentNullException">If the random  is null.</exception>
        /// <exception cref="ArgumentNullException">If the collection is null.</exception>
        public static T Choose<T>(this Random random, params T[] collection)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection[random.Next(collection.Length)];
        }

        /// <summary>
        /// Shuffles the collection in place.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random">An instance of <see cref="Random"/>.</param>
        /// <param name="collection">Collection to shuffle.</param>
        /// <exception cref="ArgumentNullException">If the random is null.</exception>
        /// <exception cref="ArgumentNullException">If the random collection is null.</exception>
        public static void Shuffle<T>(this Random random, IList<T> collection)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            int n = collection.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = collection[k];
                collection[k] = collection[n];
                collection[n] = value;
            }
        }
    }
}
