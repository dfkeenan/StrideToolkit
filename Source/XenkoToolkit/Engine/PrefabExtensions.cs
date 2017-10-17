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
    public static class PrefabExtensions
    {
        public static Entity InstantiateSingle(this Prefab prefab)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            var instances = prefab.Instantiate();

            if(instances.Count > 1)
            {
                throw new InvalidOperationException("Prefab contains for than 1 entity.");
            }

            return instances[0];
        }

        public static Entity InstantiateSingle(this Prefab prefab, ref Vector3 translation)
        {
            var one = Vector3.One;
            var rotation = Quaternion.Identity;
            return prefab.InstantiateSingle(ref translation, ref rotation, ref one);
        }

        public static Entity InstantiateSingle(this Prefab prefab, Vector3 translation)
        {
            var one = Vector3.One;
            var rotation = Quaternion.Identity;
            return prefab.InstantiateSingle(ref translation, ref rotation, ref one);
        }

        public static Entity InstantiateSingle(this Prefab prefab, ref Vector3 translation, ref Vector3 rotationEulerXYZ)
        {
            var one = Vector3.One;
            MathUtilEx.ToQuaternion(ref rotationEulerXYZ, out var rotation);
            return prefab.InstantiateSingle(ref translation, ref rotation, ref one);
        }

        public static Entity InstantiateSingle(this Prefab prefab, Vector3 translation, Vector3 rotationEulerXYZ)
        {
            var one = Vector3.One;
            MathUtilEx.ToQuaternion(ref rotationEulerXYZ, out var rotation);
            return prefab.InstantiateSingle(ref translation, ref rotation, ref one);
        }

        public static Entity InstantiateSingle(this Prefab prefab, ref Vector3 translation, ref Quaternion rotation)
        {
            var one = Vector3.One;
            return prefab.InstantiateSingle(ref translation, ref rotation, ref one);
        }

        public static Entity InstantiateSingle(this Prefab prefab, Vector3 translation, Quaternion rotation)
        {
            var one = Vector3.One;
            return prefab.InstantiateSingle(ref translation, ref rotation, ref one);
        }

        public static Entity InstantiateSingle(this Prefab prefab, ref Vector3 translation, ref Vector3 rotationEulerXYZ, ref Vector3 scale)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            MathUtilEx.ToQuaternion(ref rotationEulerXYZ, out var rotation);
            return prefab.InstantiateSingle(ref translation, ref rotation, ref scale);
        }

        public static Entity InstantiateSingle(this Prefab prefab, Vector3 translation, Vector3 rotationEulerXYZ, Vector3 scale)
        {
            MathUtilEx.ToQuaternion(ref rotationEulerXYZ, out var rotation);
            return prefab.InstantiateSingle(ref translation, ref rotation, ref scale);
        }

        public static Entity InstantiateSingle(this Prefab prefab, ref Vector3 translation, ref Quaternion rotation, ref Vector3 scale)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            var instances = prefab.Instantiate();

            if (instances.Count > 1)
            {
                throw new InvalidOperationException("Prefab contains for than 1 entity.");
            }

            var instance = instances[0];
            Matrix.Transformation(ref scale, ref rotation, ref translation, out Matrix localMatrix);

            instance.Transform.UpdateLocalMatrix();
            var entityMatrix = instance.Transform.LocalMatrix * localMatrix;

            if (instance.Transform.UseTRS)
            {
                entityMatrix.Decompose(out instance.Transform.Scale, out instance.Transform.Rotation, out instance.Transform.Position);
            }
            else
            {
                instance.Transform.LocalMatrix = entityMatrix;
            }

            return instance;
        }

        public static Entity InstantiateSingle(this Prefab prefab, Vector3 translation, Quaternion rotation, Vector3 scale)
        {
            return prefab.InstantiateSingle(ref translation, ref rotation, ref scale);
        }

        public static List<Entity> Instantiate(this Prefab prefab, ref Vector3 translation)
        {
            var one = Vector3.One;
            var rotation = Quaternion.Identity;
            return prefab.Instantiate(ref translation, ref rotation, ref one);
        }

        public static List<Entity> Instantiate(this Prefab prefab, Vector3 translation)
        {
            var one = Vector3.One;
            var rotation = Quaternion.Identity;
            return prefab.Instantiate(ref translation, ref rotation, ref one);
        }

        public static List<Entity> Instantiate(this Prefab prefab, ref Vector3 translation, ref Vector3 rotationEulerXYZ)
        {
            var one = Vector3.One;
            MathUtilEx.ToQuaternion(ref rotationEulerXYZ, out var rotation);
            return prefab.Instantiate(ref translation, ref rotation, ref one);
        }

        public static List<Entity> Instantiate(this Prefab prefab, Vector3 translation, Vector3 rotationEulerXYZ)
        {
            var one = Vector3.One;
            MathUtilEx.ToQuaternion(ref rotationEulerXYZ, out var rotation);
            return prefab.Instantiate(ref translation, ref rotation, ref one);
        }

        public static List<Entity> Instantiate(this Prefab prefab, ref Vector3 translation, ref Quaternion rotation)
        {
            var one = Vector3.One;
            return prefab.Instantiate(ref translation, ref rotation, ref one);
        }

        public static List<Entity> Instantiate(this Prefab prefab, Vector3 translation, Quaternion rotation)
        {
            var one = Vector3.One;
            return prefab.Instantiate(ref translation, ref rotation, ref one);
        }

        public static List<Entity> Instantiate(this Prefab prefab, ref Vector3 translation, ref Vector3 rotationEulerXYZ, ref Vector3 scale)
        {
            MathUtilEx.ToQuaternion(ref rotationEulerXYZ, out var rotation);
            return prefab.Instantiate(ref translation, ref rotation, ref scale);
        }

        public static List<Entity> Instantiate(this Prefab prefab, Vector3 translation, Vector3 rotationEulerXYZ, Vector3 scale)
        {
            MathUtilEx.ToQuaternion(ref rotationEulerXYZ, out var rotation);
            return prefab.Instantiate(ref translation, ref rotation, ref scale);
        }

        public static List<Entity> Instantiate(this Prefab prefab, ref Vector3 translation, ref Quaternion rotation, ref Vector3 scale)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            Matrix localMatrix;
            Matrix.Transformation(ref scale, ref rotation, ref translation, out localMatrix);

            var instances = prefab.Instantiate();

            foreach (var instance in instances)
            {
                instance.Transform.UpdateLocalMatrix();
                var entityMatrix = instance.Transform.LocalMatrix * localMatrix;

                if (instance.Transform.UseTRS)
                {
                    entityMatrix.Decompose(out instance.Transform.Scale, out instance.Transform.Rotation, out instance.Transform.Position);
                }
                else
                {
                    instance.Transform.LocalMatrix = entityMatrix;
                }                
                
            }   

            return instances;
        }

        public static List<Entity> Instantiate(this Prefab prefab, Vector3 translation, Quaternion rotation, Vector3 scale)
        {
            return prefab.Instantiate(ref translation, ref rotation, ref scale);
        }
    }
}
