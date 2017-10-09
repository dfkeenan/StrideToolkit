using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit.Engine
{
    public static class TransformExtensions
    {
        public static Vector3 WorldUp = Vector3.UnitY;

        public static void Translate(this TransformComponent transform, Vector3 translation, Space relativeTo = Space.Self)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            if(transform.UseTRS)
            {

            }
            else
            {

            }
        }

        public static void Translate(this TransformComponent transform, Vector3 translation, TransformComponent relativeTo)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (relativeTo == null)
            {
                throw new ArgumentNullException(nameof(relativeTo));
            }

            throw new NotImplementedException();

            if (transform.UseTRS)
            {

            }
            else
            {

            }
        }

        public static void Rotate(this TransformComponent transform, Vector3 eulerAngles, Space relativeTo = Space.Self)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            if (transform.UseTRS)
            {

            }
            else
            {

            }
        }

        public static void RotateAround(this TransformComponent transform, Vector3 point, Vector3 axis, float angle)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            if (transform.UseTRS)
            {

            }
            else
            {

            }
        }

        public static Vector3 TransformDirection(this TransformComponent transform, Vector3 direction)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            return direction;
        }

        public static Vector3 InverseTransformDirection(this TransformComponent transform, Vector3 direction)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            return direction;
        }

        public static Vector3 TransformPosition(this TransformComponent transform, Vector3 position)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            return position;
        }

        public static Vector3 InversTransformPosition(this TransformComponent transform, Vector3 position)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            return position;
        }

        public static Vector3 TransformVector(this TransformComponent transform, Vector3 vector)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            return vector;
        }

        public static Vector3 InverseTransformVector(this TransformComponent transform, Vector3 vector)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();

            return vector;
        }

        public static void LookAt(this TransformComponent transform, TransformComponent target, Vector3 worldUp)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            transform.LookAt(target.WorldMatrix.TranslationVector, worldUp);
        }

        public static void LookAt(this TransformComponent transform, TransformComponent target)
        {
            transform.LookAt(target, WorldUp);
        }

        public static void LookAt(this TransformComponent transform, Vector3 target, Vector3 worldUp)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            throw new NotImplementedException();
            
        }

        public static void LookAt(this TransformComponent transform, Vector3 target)
        {
            transform.LookAt(target, WorldUp);
        }
    }
}
