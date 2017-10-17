using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Mathematics;

namespace XenkoToolkit.Engine
{
    public static class TransformExtensions
    {
        public static Vector3 WorldUp = Vector3.UnitY;

        public static void Translate(this TransformComponent transform, ref Vector3 translation, Space relativeTo = Space.Self)
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

        public static void Translate(this TransformComponent transform, Vector3 translation, Space relativeTo = Space.Self)
        {
            transform.Translate(ref translation, relativeTo);
        }

        public static void Translate(this TransformComponent transform, ref Vector3 translation, TransformComponent relativeTo)
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

        public static void Translate(this TransformComponent transform, Vector3 translation, TransformComponent relativeTo)
        {
            transform.Translate(ref translation, relativeTo);
        }

        public static void Rotate(this TransformComponent transform, ref Vector3 eulerAngles, Space relativeTo = Space.Self)
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

        public static void Rotate(this TransformComponent transform, Vector3 eulerAngles, Space relativeTo = Space.Self)
        {
            transform.Rotate(ref eulerAngles, relativeTo);
        }

        public static void RotateAround(this TransformComponent transform, ref Vector3 point, ref Vector3 axis, float angle)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.UpdateLocalMatrix();
            var translationToOrigin = point * -1.0f;

            Matrix.Translation(ref translationToOrigin, out var translationToOriginMatrix);
            Matrix.RotationAxis(ref axis, angle, out var rotationMatrix);
            Matrix.Translation(ref point, out var translationMatrix);

            transform.LocalMatrix = transform.LocalMatrix * translationToOriginMatrix * rotationMatrix * translationMatrix;

            if (transform.UseTRS)
            {                
                transform.LocalMatrix.Decompose(out transform.Scale, out transform.Rotation, out transform.Position);
            }
            
        }

        public static void RotateAround(this TransformComponent transform, Vector3 point, Vector3 axis, float angle)
        {
            transform.RotateAround(ref point, ref axis, angle);
        }

        public static void TransformDirection(this TransformComponent transform, ref Vector3 direction, out Vector3 result)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.UpdateWorldMatrix();

            Vector3.TransformNormal(ref direction, ref transform.WorldMatrix, out result);
        }

        public static Vector3 TransformDirection(this TransformComponent transform, Vector3 direction)
        {
            transform.TransformDirection(ref direction, out var result);
            return result;
        }

        public static void InverseTransformDirection(this TransformComponent transform, ref Vector3 direction, out Vector3 result)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.UpdateWorldMatrix();

            Matrix.Invert(ref transform.WorldMatrix, out var inverseMatrix);

            Vector3.TransformNormal(ref direction, ref inverseMatrix, out result);
        }

        public static Vector3 InverseTransformDirection(this TransformComponent transform, Vector3 direction)
        {
            transform.InverseTransformDirection(ref direction, out var result);
            return result;
        }

        public static void TransformPosition(this TransformComponent transform, ref Vector3 position, out Vector3 result)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.UpdateWorldMatrix();

            Vector3.TransformCoordinate(ref position, ref transform.WorldMatrix, out result);
        }

        public static Vector3 TransformPosition(this TransformComponent transform, Vector3 position)
        {
            transform.TransformPosition(ref position, out var result);
            return result;
        }

        public static void InverseTransformPosition(this TransformComponent transform, ref Vector3 position, out Vector3 result)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.UpdateWorldMatrix();

            Matrix.Invert(ref transform.WorldMatrix, out var inverseMatrix);

            Vector3.TransformCoordinate(ref position, ref inverseMatrix, out result);
        }

        public static Vector3 InverseTransformPosition(this TransformComponent transform, Vector3 position)
        {
            transform.InverseTransformPosition(ref position, out var result);
            return result;
        }

        public static void TransformVector(this TransformComponent transform, ref Vector3 vector, out Vector3 result)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.UpdateWorldMatrix();

            Vector3.TransformNormal(ref vector, ref transform.WorldMatrix, out result);
        }

        public static Vector3 TransformVector(this TransformComponent transform, Vector3 vector)
        {
            transform.TransformVector(ref vector, out var result);
            return result;
        }

        public static void InverseTransformVector(this TransformComponent transform, ref Vector3 vector, out Vector3 result)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.UpdateWorldMatrix();

            Matrix.Invert(ref transform.WorldMatrix, out var inverseMatrix);

            Vector3.TransformNormal(ref vector, ref inverseMatrix, out result);
        }

        public static Vector3 InverseTransformVector(this TransformComponent transform, Vector3 vector)
        {
            transform.InverseTransformVector(ref vector, out var result);
            return result;
        }

        public static void LookAt(this TransformComponent transform, TransformComponent target, ref Vector3 worldUp)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }            
            target.UpdateWorldMatrix();

            var targetPosition = target.WorldMatrix.TranslationVector;
            transform.LookAt(ref targetPosition, ref worldUp);
        }

        public static void LookAt(this TransformComponent transform, TransformComponent target, Vector3 worldUp)
        {
            transform.LookAt(target, ref worldUp);
        }

        public static void LookAt(this TransformComponent transform, TransformComponent target)
        {
            transform.LookAt(target, ref WorldUp);
        }

        public static void LookAt(this TransformComponent transform, ref Vector3 target, ref Vector3 worldUp)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }            

            transform.UpdateWorldMatrix();

            if(transform.UseTRS)
            {
                transform.Rotation = MathUtilEx.LookRotation(transform.WorldMatrix.TranslationVector, target, worldUp);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static void LookAt(this TransformComponent transform, Vector3 target, Vector3 worldUp)
        {
            transform.LookAt(ref target, ref worldUp);
        }

        public static void LookAt(this TransformComponent transform, ref Vector3 target)
        {
            transform.LookAt(ref target, ref WorldUp);
        }

        public static void LookAt(this TransformComponent transform, Vector3 target)
        {
            transform.LookAt(ref target, ref WorldUp);
        }
    }
}
