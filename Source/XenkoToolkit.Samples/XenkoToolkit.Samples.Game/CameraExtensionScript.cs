using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Engine;

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
            
            //DebugText.Print($"Screen {MainCamera.WorldToScreen(Entity.Transform.Position)}", new Int2(20,20));
            DebugText.Print($"Main {SceneSystem.GetMainCamera() != null}", new Int2(20, 40));
        }
    }
}
