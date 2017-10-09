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

            return prefab.Instantiate().Single();
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

            Entity instance = prefab.Instantiate().Single();

            if(!instance.Transform.UseTRS)
            {
                throw new NotSupportedException("Entity Transforms must have UseTRS set to true.");
            }

            instance.Transform.Position = position ?? instance.Transform.Position;

            if (rotation.HasValue)
            {
                instance.Transform.Rotation = rotation.Value;
            }

            instance.Transform.Scale = scale ?? instance.Transform.Scale;

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
                if (!instance.Transform.UseTRS)
                {
                    throw new NotSupportedException("Entity Transforms must have UseTRS set to true.");
                }

                instance.Transform.UpdateLocalMatrix();
                var entityMatrix = instance.Transform.LocalMatrix * localMatrix;
                entityMatrix.Decompose(out instance.Transform.Scale, out instance.Transform.Rotation, out instance.Transform.Position);
            }   

            return instances;
        }
    }
}
