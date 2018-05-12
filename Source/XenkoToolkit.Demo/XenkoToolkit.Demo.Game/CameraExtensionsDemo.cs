using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Engine.Events;
using SiliconStudio.Xenko.Input;
using SiliconStudio.Xenko.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Engine;
using XenkoToolkit.Mathematics;
using XenkoToolkit.Physics;

namespace XenkoToolkit.Demo
{
    public class CameraExtensionsDemo : SyncScript
    {
        public static readonly EventKey<Entity> TargetAcquired = new EventKey<Entity>(eventName: "TargetAcquired");
        
        private string message;

        private Entity selected = null;
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

            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                message = "";
                

                var ray = MainCamera.ScreenToWorldRaySegment(Input.MousePosition);

                HitResult hitResult = simulation.Raycast(ray);
                if (hitResult.Succeeded)
                {
                    selected = hitResult.Collider.Entity;

                    message = selected.Name;
                    TargetAcquired.Broadcast(selected);
                    DebugText.Print($"Clicked on {message}", new Int2(20, 60));

                    selectedScreenPoint = MainCamera.WorldToScreenPoint(selected.Transform.Position);
                    worldMousePoint = MainCamera.ScreenToWorldPoint(new Vector3(Input.MousePosition, selectedScreenPoint.Z));
                    offset = selected.Transform.Position - worldMousePoint;
                } else
                {
                    selected = null;
                    selectedScreenPoint = Vector3.Zero;
                    worldMousePoint = Vector3.Zero;
                    TargetAcquired.Broadcast(null);
                }
            }
            else if (selected != null && Input.IsMouseButtonDown(MouseButton.Left))
            {
                var currentWorldMousePoint = MainCamera.ScreenToWorldPoint(new Vector3(Input.MousePosition, selectedScreenPoint.Z));
                // mouseEntity.Transform.Position = currentWorldMousePoint;
                selected.Transform.Position = currentWorldMousePoint + offset;
                worldMousePoint = currentWorldMousePoint;
            }
            else if (selected != null && Input.IsKeyPressed(Keys.F))
            {
                MainCamera.Entity.Transform.LookAt(selected.Transform);
                MainCamera.Update();
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
