using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Engine.Events;
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

        public CameraComponent MainCamera { get; set; }

        public Entity Cube { get; set; }

        public override void Start()
        {
            // MainCamera = SceneSystem.GetMainCamera();
        }

        public override void Update()
        {
            Cube?.Transform.Rotate(new Vector3(MathUtil.DegreesToRadians(60) * Game.GetDeltaTime(), 0, 0));


            

            DebugText.Print($"ScreenToWorldRaySegment {MainCamera.ScreenToWorldRaySegment(Input.MousePosition)}", new Int2(20, 40));

            if (Input.IsMouseButtonPressed(SiliconStudio.Xenko.Input.MouseButton.Left))
            {
                message = "";


                var ray = MainCamera.ScreenToWorldRaySegment(Input.MousePosition);

                var hitResult = this.GetSimulation().Raycast(ray);
                if (hitResult.Succeeded)
                {
                    Entity targetEntity = hitResult.Collider.Entity;
                    message = targetEntity.Name;

                    MainCamera.Entity.Transform.LookAt(targetEntity.Transform);
                    MainCamera.Update();
                    TargetAcquired.Broadcast(targetEntity);

                    //Vector3 targetPosition = targetEntity.Transform.Position;
                    //Vector3 targetScreenPosition = MainCamera.WorldToScreenPoint(targetPosition);
                    //Vector3 targetWorldPosition = MainCamera.ScreenToWorldPoint(targetScreenPosition);
                    ////targetPosition should == targetWorldPosition
                    //var distance = Vector3.Distance(targetPosition, targetWorldPosition).ToString("F17");

                    //Console.WriteLine($"Input World: {targetPosition}, WorldToScreenPoint: {targetScreenPosition}, ScreenToWorldPoint {targetWorldPosition}");
                }
                else
                {
                    TargetAcquired.Broadcast(null);
                }
                DebugText.Print($"Clicked on {message}", new Int2(20, 60));
            }

            //DebugText.Print($"Main {SceneSystem.GetMainCamera() != null}", new Int2(20, 40));
        }

        //Dirty Hacks
        public override void Cancel()
        {
            base.Cancel();
            DebugText.Update(Game.UpdateTime);
        }
    }
}
