using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenko.Core.Mathematics;
using Xenko.Input;
using Xenko.Engine;
using Xenko.Rendering.Compositing;
using XenkoToolkit.Engine;
using Xenko.Engine.Events;
using Xenko.Rendering;

namespace XenkoToolkit.Demo.Outline
{
    public class OutlineManager : SyncScript
    {

        private Entity hoverEntity;
        private Entity selectEntity;
        private EventReceiver<Entity> entitySelected;
        private EventReceiver<Entity> entityHover;

        public Prefab OutlinePrefab { get; set; }
        public Material HoverMaterial { get; set; }
        public Material SelectMaterial { get; set; }

        public override void Start()
        {
            hoverEntity = OutlinePrefab.InstantiateSingle();
            hoverEntity.Get<OutlineEntity>().Material = HoverMaterial;

            selectEntity = OutlinePrefab.InstantiateSingle();
            selectEntity.Get<OutlineEntity>().Material = SelectMaterial;

            entitySelected = new EventReceiver<Entity>(CameraExtensionsDemo.EntitySelected);
            entityHover = new EventReceiver<Entity>(CameraExtensionsDemo.EntityHover);            
        }

        public override void Update()
        {
            UpdateOutline(entityHover, hoverEntity);
            UpdateOutline(entitySelected, selectEntity);   
        }

        private void UpdateOutline(EventReceiver<Entity> outlineEvent, Entity outlineEntity)
        {
            if (outlineEvent.TryReceive(out var newOutlineEntity))
            {
                outlineEntity.Transform.Parent = newOutlineEntity?.Transform;
            }
        }

        public override void Cancel()
        {
           
        }
    }
}
