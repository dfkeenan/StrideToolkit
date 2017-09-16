using SiliconStudio.Xenko.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit.Engine
{
    /// <summary>
    /// Extensions for <see cref="CameraComponent"/>
    /// </summary>
    public static class CameraExtensions
    {
        /// <summary>
        /// Gets the scenes composition main camera or null if there is no GraphicsCompositor or Cameras.
        /// </summary>
        /// <param name="sceneSystem"></param>
        /// <param name="mainCameraSlotName"></param>
        /// <returns>The selected main CameraComponent</returns>
        /// <remarks>
        /// If there is more than 1 SceneCameraSlot and there is one with matching name it returns it.
        /// If no matching SceneCameraSlot is found the first CameraComponent is Returned.
        /// </remarks>
        public static CameraComponent GetMainCamera(this SceneSystem sceneSystem, string mainCameraSlotName = "Main")
        {
            if (sceneSystem == null)
            {
                throw new ArgumentNullException(nameof(sceneSystem));
            }

            var cameras = sceneSystem.GraphicsCompositor?.Cameras;

            if (cameras == null)
            {
                return null;
            }

            if (cameras.Count == 1)
            {
                return cameras[0].Camera;
            }

            var camera = cameras.Where(s => s.Name == mainCameraSlotName).Select(s => s.Camera).FirstOrDefault();

            if (camera == null && cameras.Count > 0)
            {
                camera = cameras[0].Camera;
            }

            return camera;
        }


    }
}
