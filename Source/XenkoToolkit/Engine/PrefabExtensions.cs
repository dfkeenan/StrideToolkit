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

        public static Entity InstantiateSingleEulerXYZ(this Prefab prefab, Vector3? position = null, Vector3? rotationEulerXYZ = null, Vector3? scale = null)
        {
            return prefab.InstantiateSingle(position, rotationEulerXYZ?.ToQuaternion(), scale);
        }

        public static Entity InstantiateSingle(this Prefab prefab, Vector3? position = null, Quaternion? rotation = null, Vector3? scale = null)
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

            if (instance.Transform.UseTRS)
            {
                instance.Transform.Position = position ?? instance.Transform.Position;

                if (rotation.HasValue)
                {
                    instance.Transform.Rotation = rotation.Value;
                }

                instance.Transform.Scale = scale ?? instance.Transform.Scale;
            }
            else
            {
                var localTranslation = position ?? Vector3.Zero;
                var localRotation = rotation ?? Quaternion.Identity;
                var localScale = scale ?? Vector3.One;
                Matrix localMatrix;
                Matrix.Transformation(ref localScale, ref localRotation, ref localTranslation, out localMatrix);

                instance.Transform.LocalMatrix = localMatrix;
            }

            

            return instance;
        }

        public static List<Entity> InstantiateEulerXYZ(this Prefab prefab, Vector3? translation = null, Vector3? rotationEulerXYZ = null, Vector3? scale = null)
        {
            return prefab.Instantiate(translation, rotationEulerXYZ?.ToQuaternion(), scale);
        }

        public static List<Entity> Instantiate(this Prefab prefab, Vector3? translation = null, Quaternion? rotation = null, Vector3? scale = null)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            var localTranslation = translation ?? Vector3.Zero;
            var localRotation = rotation ?? Quaternion.Identity;
            var localScale = scale ?? Vector3.One;

            Matrix localMatrix;
            Matrix.Transformation(ref localScale, ref localRotation, ref localTranslation, out localMatrix);

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
    }
}
