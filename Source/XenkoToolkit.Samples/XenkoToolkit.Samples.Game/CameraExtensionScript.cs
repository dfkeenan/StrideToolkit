using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Engine;
using XenkoToolkit.Mathematics;
using XenkoToolkit.Physics;

namespace XenkoToolkit.Samples
{
    public class CameraExtensionScript : SyncScript
    {
        private CameraComponent mainCam;
        private string message;

        public CameraComponent MainCamera { get => mainCam; set => mainCam = value; }

        public override void Start()
        {
            // MainCamera = SceneSystem.GetMainCamera();

        }

        

        public override void Update()
        {
            
            DebugText.Print($"Screen {MainCamera.WorldToScreen(Entity.Transform.Position)}", new Int2(20,20));

            DebugText.Print($"ScreenToWorldRaySegment {MainCamera.ScreenToWorldRaySegment(Input.MousePosition)}", new Int2(20, 40));

            if (Input.IsMouseButtonPressed(SiliconStudio.Xenko.Input.MouseButton.Left))
            {
                message = "";

                var simulation = this.GetSimulation();

                var ray = mainCam.ScreenToWorldRaySegment(Input.MousePosition);

                var hitResult = simulation.Raycast(ray);
                if(hitResult.Succeeded)
                {
                    message = hitResult.Collider.Entity.Name;

                    mainCam.Entity.Transform.Rotation = MathUtilEx.LookRotation(mainCam.Entity.Transform.Position, hitResult.Collider.Entity.Transform.Position, Vector3.UnitY);
                }
                DebugText.Print($"Clicked on {message}", new Int2(20, 60));
            }

            //DebugText.Print($"Main {SceneSystem.GetMainCamera() != null}", new Int2(20, 40));
        }
    }

}
