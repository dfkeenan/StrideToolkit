using SiliconStudio.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit
{
    public static class RandomExtensions
    {
        public static float NextSingle(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return (float)random.NextDouble();
        }

        public static Vector2 NextPoint(this Random random, RectangleF region)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return Vector2.Lerp(region.TopLeft, region.BottomRight, random.NextSingle());
            
        }

        public static Vector3 NextPoint(this Random random, BoundingBox region)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return Vector3.Lerp(region.Minimum, region.Maximum, random.NextSingle());

        }

        public static Vector2 NextDirection2D(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return Vector2.Normalize(new Vector2(random.NextSingle(), random.NextSingle()));
        }

        public static Vector3 NextDirection3D(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return Vector3.Normalize(new Vector3(random.NextSingle(), random.NextSingle(), random.NextSingle()));
        }

        public static Color NextColor(this Random random) => new Color(NextDirection3D(random));

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
