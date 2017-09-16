using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Engine;
using XenkoToolkit.Physics;

namespace XenkoToolkit.Samples
{
    public class CameraExtensionScript : SyncScript
    {
        private CameraComponent mainCamera;

        public CameraComponent MainCamera { get => mainCamera; set => mainCamera = value; }

        public override void Start()
        {
           // MainCamera = SceneSystem.GetMainCamera();
        }

        public override void Update()
        {
            
            DebugText.Print($"Screen {MainCamera.WorldToScreen(Entity.Transform.Position)}", new Int2(20,20));

            DebugText.Print($"ScreenToWorldRaySegment {MainCamera.ScreenToWorldRaySegment(Input.MousePosition)}", new Int2(20, 40));

            
            var simulation = this.GetSimulation();

            var ray = mainCamera.ScreenToWorldRaySegment(Input.MousePosition);

            var hitResult = simulation.Raycast(ray);


            //DebugText.Print($"Main {SceneSystem.GetMainCamera() != null}", new Int2(20, 40));
        }
    }
}
