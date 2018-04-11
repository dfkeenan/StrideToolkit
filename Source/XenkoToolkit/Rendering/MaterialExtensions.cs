using SiliconStudio.Xenko.Rendering;

namespace XenkoToolkit.Rendering
{
    public static class MaterialExtensions
    {
        public static Material Clone(this Material material)
        {
            var clone = new Material();

            CopyProperties(material, clone);

            return clone;
        }

        internal static void CopyProperties(Material material, Material clone)
        {
            foreach (var pass in material.Passes)
            {
                clone.Passes.Add(new MaterialPass(new ParameterCollection(pass.Parameters))
                {
                    HasTransparency = pass.HasTransparency,
                    BlendState = pass.BlendState,
                    CullMode = pass.CullMode,
                    IsLightDependent = pass.IsLightDependent
                });
            }
        }
    }
}
