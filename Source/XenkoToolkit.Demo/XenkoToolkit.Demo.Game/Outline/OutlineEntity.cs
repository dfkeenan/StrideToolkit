using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenko.Core.Mathematics;
using Xenko.Input;
using Xenko.Engine;
using Xenko.Rendering;

namespace XenkoToolkit.Demo.Outline
{
    public class OutlineEntity : SyncScript
    {
        private ModelComponent modelComponent;
        private Entity currentParent;

        public Material Material { get; set; }

        public override void Start()
        {
            modelComponent = Entity.Get<ModelComponent>();

            if (Material != null)
            {
                modelComponent.Materials[0] = Material;
            }
        }

        public override void Update()
        {
            if (currentParent != Entity.GetParent())
            {
                currentParent = Entity.GetParent();
                var parentModel = currentParent.Get<ModelComponent>()?.Model;

                modelComponent.Model = parentModel;

            }
        }

        public override void Cancel()
        {
            if (currentParent != null)
            {
                modelComponent.Model = null;
                currentParent = null;

            }
        }
    }
}
