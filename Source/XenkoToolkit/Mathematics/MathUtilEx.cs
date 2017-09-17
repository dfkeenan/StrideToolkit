using SiliconStudio.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit.Mathematics
{
    /// <summary>
    /// More common utility methods for math operations.
    /// </summary>
    public static class MathUtilEx
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CeilingToInt(this float value) => (int)Math.Ceiling(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FloorToInt(this float value) => (int)Math.Floor(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundToInt(this float value) => (int)Math.Round(value);

        /// <summary>
        /// Orthonormalizes 2 vectors.
        /// </summary>
        /// <param name="normal">The normal vector.</param>
        /// <param name="tangent">The tangent vector.</param>
        /// <remarks>
        /// <para>Makes vectors normalized and orthogonal to each other. 
        /// Normalizes normal. Normalizes tangent and makes sure it is orthogonal to normal.</para>
        /// </remarks>
        public static void Orthonormalize(ref Vector3 normal, ref Vector3 tangent)
        {
            //Uses the modified Gram-Schmidt process.
            //Because we are making unit vectors, we can optimize the math for orthogonalization
            //and simplify the projection operation to remove the division.
            //q1 = m1 / |m1|
            //q2 = (m2 - (q1 ⋅ m2) * q1) / |m2 - (q1 ⋅ m2) * q1|

            normal.Normalize();
            tangent -= Vector3.Dot(normal, tangent) * normal;
            tangent.Normalize();
        }

        /// <summary>
        /// Creates a rotation with the specified forward and upwards directions.
        /// </summary>
        /// <param name="eye">The postion of the observer. i.e. camera</param>
        /// <param name="target">The location of the object to look-at.</param>
        /// <param name="up">The vector that defines which direction is up.</param>
        /// <returns>The created quaternion rotation</returns>
        /// <example>
        /// var cameraRotation = Quaternion.LookRotation(cameraPosition, targetPosition, Vector3.UnitY); 
        /// </example>
        public static Quaternion LookRotation(Vector3 eye, Vector3 target, Vector3 up)
        {
            var forward = target - eye;
            
            // Which would create LH rotation. Xenko uses RH so we need to reverse it.
            forward *= -1f;


            Orthonormalize(ref forward, ref up);
            Vector3 right;
            Vector3.Cross(ref up, ref forward, out right);

            right.Normalize();

            var w = (float)Math.Sqrt(1.0f + right.X + up.Y + forward.Z) * 0.5f;
            var w4_recip = 1.0f / (4.0f * w);

            var result = new Quaternion()
            {
                W = w,
                X = (up.Z - forward.Y) * w4_recip,
                Y = (forward.X - right.Z) * w4_recip,
                Z = (right.Y - up.X) * w4_recip,
            };

            return result;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Interpolate(float start, float end, float amount, EasingFunction easingFunction)
        {
            return MathUtil.Lerp(start, end, Easing.Ease(amount, easingFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Interpolate(Vector2 start, Vector2 end, float amount, EasingFunction easingFunction)
        {
            return Vector2.Lerp(start, end, Easing.Ease(amount, easingFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Interpolate(Vector3 start, Vector3 end, float amount, EasingFunction easingFunction)
        {
            return Vector3.Lerp(start, end, Easing.Ease(amount, easingFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Interpolate(Vector4 start, Vector4 end, float amount, EasingFunction easingFunction)
        {
            return Vector4.Lerp(start, end, Easing.Ease(amount, easingFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Interpolate(Color start, Color end, float amount, EasingFunction easingFunction)
        {
            return Color.Lerp(start, end, Easing.Ease(amount, easingFunction));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Interpolate(ref Vector2 start, ref Vector2 end, float amount, EasingFunction easingFunction, out Vector2 result)
        {
            Vector2.Lerp(ref start, ref end, Easing.Ease(amount, easingFunction), out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Interpolate(ref Vector3 start, ref Vector3 end, float amount, EasingFunction easingFunction, out Vector3 result)
        {
            Vector3.Lerp(ref start, ref end, Easing.Ease(amount, easingFunction), out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Interpolate(ref Vector4 start, ref Vector4 end, float amount, EasingFunction easingFunction, out Vector4 result)
        {
            Vector4.Lerp(ref start, ref end, Easing.Ease(amount, easingFunction), out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Interpolate(ref Color start, ref Color end, float amount, EasingFunction easingFunction, out Color result)
        {
            Color.Lerp(ref start, ref end, Easing.Ease(amount, easingFunction), out result);
        }
        
    }
}
