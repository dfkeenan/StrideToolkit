using System;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Rendering;
using XenkoToolkit.Rendering;

namespace XenkoToolkit.Engine
{
    public static class ModelComponentExtensions
    {

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ObjectParameterAccessor<T> parameterAccessor, T value, int materialIndex = 0, int passIndex = 0)
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameterAccessor, value);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ValueParameter<T> parameter, T value, int materialIndex = 0, int passIndex = 0) where T : struct
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, value);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ValueParameter<T> parameter, int count, ref T firstValue, int materialIndex = 0, int passIndex = 0) where T : struct
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, count, ref firstValue);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ValueParameter<T> parameter, ref T value, int materialIndex = 0, int passIndex = 0) where T : struct
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, ref value);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ValueParameterKey<T> parameter, int count, ref T firstValue, int materialIndex = 0, int passIndex = 0) where T : struct
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, count, ref firstValue);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ValueParameterKey<T> parameter, T[] values, int materialIndex = 0, int passIndex = 0) where T : struct
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, values);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ValueParameterKey<T> parameter, ref T value, int materialIndex = 0, int passIndex = 0) where T : struct
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, ref value);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ValueParameterKey<T> parameter, T value, int materialIndex = 0, int passIndex = 0) where T : struct
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, value);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, PermutationParameter<T> parameter, T value, int materialIndex = 0, int passIndex = 0)
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, value);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, ObjectParameterKey<T> parameter, T value, int materialIndex = 0, int passIndex = 0)
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, value);
        }

        public static void SetMaterialParameter<T>(this ModelComponent modelComponent, PermutationParameterKey<T> parameter, T value, int materialIndex = 0, int passIndex = 0)
        {
            modelComponent.GetMaterialPassParameters(materialIndex, passIndex).Set(parameter, value);
        }



        /// <summary>
        /// Clones a <see cref="ModelComponent"/>s <see cref="Material"/> if required;
        /// </summary>
        /// <param name="modelComponent"></param>
        /// <param name="materialIndex"></param>
        /// <returns></returns>
        private static Material GetMaterialCopy(this ModelComponent modelComponent, int materialIndex)
        {
            if (modelComponent == null)
            {
                throw new ArgumentNullException(nameof(modelComponent));
            }

            if (!IsValidMaterialIndex(modelComponent, materialIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(materialIndex));
            }

            var material = modelComponent.GetMaterial(materialIndex);

            if (material is ModelComponentMaterialCopy copy && copy.ModelComponent == modelComponent)
            {
                return material;
            }

            var materialCopy = new ModelComponentMaterialCopy()
            {
                ModelComponent = modelComponent,
            };

            MaterialExtensions.CopyProperties(material, materialCopy);

            modelComponent.Materials[materialIndex] = materialCopy;

            return materialCopy;
        }

        private static bool IsValidMaterialIndex(ModelComponent modelComponent, int materialIndex)
        {
            if (materialIndex < 0) return false;

            int materialCount = modelComponent.GetMaterialCount();

            if (materialCount > 0)
            {
                return materialIndex < materialCount;
            }
            else
            {
                return modelComponent.Materials.ContainsKey(materialIndex);
            }            
        }

        private static ParameterCollection GetMaterialPassParameters(this ModelComponent modelComponent, int materialIndex, int passIndex)
        {
            var material = modelComponent.GetMaterialCopy(materialIndex);

            if (passIndex < 0 || passIndex >= material.Passes.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(passIndex));
            }

            return material.Passes[passIndex].Parameters;
        }

        private class ModelComponentMaterialCopy : Material
        {
            public ModelComponent ModelComponent { get; set; }
        }
    }
}