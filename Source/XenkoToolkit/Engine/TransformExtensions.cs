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
    /// <summary>
    /// Extension methods for <see cref="TransformComponent"/>.
    /// </summary>
    public static class TransformExtensions
    {
        public static Vector3 WorldUp = Vector3.UnitY;

        public static void UpdateTRSFromLocal(this TransformComponent transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.LocalMatrix.Decompose(out transform.Scale, out transform.Rotation, out transform.Position);
        }

        public static void Translate(this TransformComponent transform, ref Vector3 translation, Space relativeTo = Space.Self)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (!transform.UseTRS)
            {
                throw new ArgumentException("Must use TRS", nameof(transform));
            }

            var localTranslation = translation;


            if (relativeTo == Space.Self)
            {
                Vector3.TransformNormal(ref translation, ref transform.WorldMatrix, out localTranslation);
            }

            if (transform.Parent != null)
            {
                Matrix.Invert(ref transform.Parent.WorldMatrix, out var inverseParent);
                Vector3.TransformNormal(ref localTranslation, ref inverseParent, out localTranslation);
            }

            transform.Position += localTranslation;

            transform.UpdateWorldMatrix();
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

            if (!transform.UseTRS)
            {
                throw new ArgumentException("Must use TRS", nameof(transform));
            }

            if (relativeTo == null)
            {
                throw new ArgumentNullException(nameof(relativeTo));
            }

            relativeTo.TransformDirection(ref translation, out var localTranslation);
            transform.Translate(ref localTranslation, Space.World);

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

            if (!transform.UseTRS)
            {
                throw new ArgumentException("Must use TRS", nameof(transform));
            }


            Quaternion rotation = Quaternion.Identity;

            if(relativeTo == Space.Self)
            {
                //What do I do here???
                if(eulerAngles.X != 0f)
                {
                    var right = transform.WorldMatrix.Right; right.Normalize();
                    Quaternion.RotationAxis(ref right, eulerAngles.X, out var axisRotation);
                    Quaternion.Multiply(ref rotation, ref axisRotation, out rotation);
                }

                if (eulerAngles.Y != 0f)
                {
                    var up = transform.WorldMatrix.Up; up.Normalize();
                    Quaternion.RotationAxis(ref up, eulerAngles.Y, out var axisRotation);
                    Quaternion.Multiply(ref rotation, ref axisRotation, out rotation);
                }

                if (eulerAngles.Z != 0f)
                {
                    var forward = transform.WorldMatrix.Forward; forward.Normalize();
                    Quaternion.RotationAxis(ref forward, eulerAngles.Z, out var axisRotation);
                    Quaternion.Multiply(ref rotation, ref axisRotation, out rotation);
                }
            }
            else
            {
                //Quaternion.RotationYawPitchRoll(eulerAngles.Y, eulerAngles.X, eulerAngles.Z, out rotation);
                MathUtilEx.ToQuaternion(ref eulerAngles, out rotation);
            }

            if(transform.Parent != null)
            {
                transform.Parent.WorldMatrix.Decompose(out var _, out Quaternion parentRotation, out var _);
                parentRotation.Conjugate();

                Quaternion.Multiply(ref rotation, ref parentRotation, out rotation);
            }

            Quaternion.Multiply(ref transform.Rotation, ref rotation, out transform.Rotation);

            transform.UpdateWorldMatrix();
        }

        public static void Rotate(this TransformComponent transform, Vector3 eulerAngles, Space relativeTo = Space.Self)
        {
            transform.Rotate(ref eulerAngles, relativeTo);
        }

        //public static void RotateAround(this TransformComponent transform, ref Vector3 point, ref Vector3 axis, float angle)
        //{
        //    if (transform == null)
        //    {
        //        throw new ArgumentNullException(nameof(transform));
        //    }

        //    throw new NotImplementedException();

        //}

        //public static void RotateAround(this TransformComponent transform, Vector3 point, Vector3 axis, float angle)
        //{
        //    transform.RotateAround(ref point, ref axis, angle);
        //}

        public static void TransformDirection(this TransformComponent transform, ref Vector3 direction, out Vector3 result)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }            

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

            Matrix.Invert(ref transform.WorldMatrix, out var inverseMatrix);

            Vector3.TransformNormal(ref vector, ref inverseMatrix, out result);
        }

        public static Vector3 InverseTransformVector(this TransformComponent transform, Vector3 vector)
        {
            transform.InverseTransformVector(ref vector, out var result);
            return result;
        }

        public static void LookAt(this TransformComponent transform, TransformComponent target, ref Vector3 worldUp, float smooth = 1.0f)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }            

            var targetPosition = target.WorldMatrix.TranslationVector;
            transform.LookAt(ref targetPosition, ref worldUp,smooth);
        }

        public static void LookAt(this TransformComponent transform, TransformComponent target, Vector3 worldUp, float smooth = 1.0f)
        {
            transform.LookAt(target, ref worldUp, smooth);
        }

        public static void LookAt(this TransformComponent transform, TransformComponent target, float smooth = 1.0f)
        {
            transform.LookAt(target, ref WorldUp, smooth);
        }

        public static void LookAt(this TransformComponent transform, ref Vector3 target, ref Vector3 worldUp, float smooth = 1.0f)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (!transform.UseTRS)
            {
                throw new ArgumentException("Must use TRS", nameof(transform));
            }

            var localTarget = target;
            var localUp = worldUp;

            if(transform.Parent != null)
            {
                Matrix.Invert(ref transform.Parent.WorldMatrix, out var inverseParent);
                Vector3.TransformCoordinate(ref target, ref inverseParent, out localTarget);
                Vector3.TransformNormal(ref worldUp, ref inverseParent, out localUp);
            }

            var localEye = transform.LocalMatrix.TranslationVector;

            MathUtilEx.LookRotation(ref localEye, ref localTarget, ref localUp, out var lookRotation);

            if(smooth == 1.0f)
            {
                transform.Rotation = lookRotation;
            }
            else
            {
                Quaternion.Slerp(ref transform.Rotation, ref lookRotation, smooth, out transform.Rotation);
            }


            transform.UpdateWorldMatrix();
        }

        public static void LookAt(this TransformComponent transform, Vector3 target, Vector3 worldUp, float smooth = 1.0f)
        {
            transform.LookAt(ref target, ref worldUp, smooth);
        }

        public static void LookAt(this TransformComponent transform, ref Vector3 target, float smooth = 1.0f)
        {
            transform.LookAt(ref target, ref WorldUp, smooth);
        }

        public static void LookAt(this TransformComponent transform, Vector3 target, float smooth = 1.0f)
        {
            transform.LookAt(ref target, ref WorldUp, smooth);
        }
    }
}
