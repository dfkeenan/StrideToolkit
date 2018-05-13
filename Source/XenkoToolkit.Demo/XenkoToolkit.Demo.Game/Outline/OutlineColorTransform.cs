using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Core;
using SiliconStudio.Core.Annotations;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering.Images;

namespace XenkoToolkit.Demo.Outline
{
    [DataContract("Outline")]
    public class Outline : ColorTransform
    {

        public Outline() : base("OutlineEffect")
        {
            Thickness = 1;
            ColorOne = Color.Transparent;
            ColorTwo = Color.Transparent;
            ColorThree = Color.Transparent;
        }

        [DataMember(10)]
        [NotNull]
        public Texture OutlineTexture
        {
            get => Parameters.Get(OutlineShaderKeys.OutlineTexture);
            set => Parameters.Set(OutlineShaderKeys.OutlineTexture, value);
        }

        [DataMember(15)]
        [DefaultValue(1)]
        public int Thickness
        {
            get => Parameters.Get(OutlineKeys.Thickness);
            set => Parameters.Set(OutlineKeys.Thickness, value);
        }

        [DataMember(20)]
        //[DefaultValue("Black")]
        public Color ColorOne
        {
            get => (Color)Parameters.Get(OutlineShaderKeys.ColorOne);
            set => Parameters.Set(OutlineShaderKeys.ColorOne, value);
        }

        [DataMember(30)]
        //[DefaultValue("Black")]
        public Color ColorTwo
        {
            get => (Color)Parameters.Get(OutlineShaderKeys.ColorTwo);
            set => Parameters.Set(OutlineShaderKeys.ColorTwo, value);
        }

        [DataMember(40)]
        //[DefaultValue("Black")]
        public Color ColorThree
        {
            get => (Color)Parameters.Get(OutlineShaderKeys.ColorThree);
            set => Parameters.Set(OutlineShaderKeys.ColorThree, value);
        }


        public override void UpdateParameters(ColorTransformContext context)
        {

            if (OutlineTexture != null)
            {
                var size = OutlineTexture.Size;
                var texelSize = new Vector2(1.0f / size.Width, 1.0f / size.Height);
                Parameters.Set(OutlineShaderKeys.OutlineTexelSize, texelSize);
            }

            base.UpdateParameters(context);
        }
    }
}
