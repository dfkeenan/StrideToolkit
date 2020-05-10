using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Input;
using Stride.Physics;
using Stride.Rendering.Compositing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrideToolkit.Engine;
using StrideToolkit.Mathematics;
using StrideToolkit.Physics;

namespace StrideToolkit.Demo
{
    public class CameraExtensionsDemo : SyncScript
    {
        public static readonly EventKey<Entity> EntitySelected = new EventKey<Entity>(eventName: nameof(EntitySelected));
        public static readonly EventKey<Entity> EntityHover = new EventKey<Entity>(eventName: nameof(EntityHover));


        private string message;

        private Entity selected = null;
        private Entity hovered = null;
        private Simulation simulation;
        private Vector3 selectedScreenPoint;
        private Vector3 worldMousePoint;
        private Vector3 offset;

        public CameraComponent MainCamera { get; set; }

        public Entity Cube { get; set; }

      

        public override void Start()
        {
            simulation = this.GetSimulation();

           
        }

        public override void Update()
        {
            Cube?.Transform.Rotate(new Vector3(MathUtil.DegreesToRadians(60) * Game.GetDeltaTime(), 0, 0));


            DebugText.Print($"ScreenToWorldRaySegment {MainCamera.ScreenToWorldRaySegment(Input.MousePosition)}", new Int2(20, 40));


            var ray = MainCamera.ScreenToWorldRaySegment(Input.MousePosition);

            HitResult hitResult = simulation.Raycast(ray);
            Entity hitEntity = hitResult.Collider?.Entity;

            if (hitResult.Succeeded && hitEntity != selected)
            {
                if (hitEntity != hovered)
                {
                    hovered = hitEntity;
                    EntityHover.Broadcast(hitEntity); 
                }
            }
            else
            {
                if (hovered != null)
                {
                    hovered = null;
                    EntityHover.Broadcast(null);
                }                
            }

            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                message = "";   

                if (hitResult.Succeeded)
                {
                    selected = hitEntity;

                    message = selected.Name;
                    EntitySelected.Broadcast(selected);
                    DebugText.Print($"Clicked on {message}", new Int2(20, 60));

                    selectedScreenPoint = MainCamera.WorldToScreenPoint(selected.Transform.Position);
                    worldMousePoint = MainCamera.ScreenToWorldPoint(new Vector3(Input.MousePosition, selectedScreenPoint.Z));
                    offset = selected.Transform.Position - worldMousePoint;
                } else
                {
                    selected = null;
                    selectedScreenPoint = Vector3.Zero;
                    worldMousePoint = Vector3.Zero;
                    EntitySelected.Broadcast(null);
                }
            }
            else if (selected != null && Input.IsMouseButtonDown(MouseButton.Left))
            {
                var currentWorldMousePoint = MainCamera.ScreenToWorldPoint(new Vector3(Input.MousePosition, selectedScreenPoint.Z));
                // mouseEntity.Transform.Position = currentWorldMousePoint;
                selected.Transform.Position = currentWorldMousePoint + offset;
                worldMousePoint = currentWorldMousePoint;
            }

        }

        //Dirty Hacks
        public override void Cancel()
        {
            base.Cancel();
            DebugText.Update(Game.UpdateTime);

           
        }
    }
}
