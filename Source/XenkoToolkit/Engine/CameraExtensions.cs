using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Rendering.Compositing;
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
        ///// <summary>
        ///// Gets the scenes composition main camera or null if there is no GraphicsCompositor or Cameras.
        ///// </summary>
        ///// <param name="sceneSystem"></param>
        ///// <param name="mainCameraSlotName"></param>
        ///// <returns>The selected main CameraComponent</returns>
        ///// <remarks>
        ///// If there is more than 1 SceneCameraSlot and there is one with matching name it returns it.
        ///// If no matching SceneCameraSlot is found the first CameraComponent is Returned.
        ///// </remarks>
        //public static CameraComponent GetMainCamera(this SceneSystem sceneSystem, string mainCameraSlotName = "Main")
        //{
        //    if (sceneSystem == null)
        //    {
        //        throw new ArgumentNullException(nameof(sceneSystem));
        //    }

        //    var cameras = sceneSystem.GraphicsCompositor?.Cameras;

        //    if (cameras == null)
        //    {
        //        return null;
        //    }

        //    SceneCameraSlot cameraSlot = null;

        //    if (cameras.Count == 1)
        //    {
        //        cameraSlot = cameras[0];
        //    }
        //    else
        //    {
        //        cameraSlot = cameras.FirstOrDefault(s => s.Name == mainCameraSlotName);
        //    }

        //    if (cameraSlot == null && cameras.Count > 0)
        //    {
        //        cameraSlot = cameras[0];
        //    }

        //    return cameraSlot?.Camera;
        //}


        public static Vector3 WorldToClip(this CameraComponent cameraComponent, Vector3 position)
        {
            if (cameraComponent == null)
            {
                throw new ArgumentNullException(nameof(cameraComponent));
            }

            return Vector3.TransformCoordinate(position, cameraComponent.ViewProjectionMatrix);           

        }

        /// <summary>
        /// Converts the position to screen space coordinates relative to camera.
        /// </summary>
        /// <param name="cameraComponent"></param>
        /// <param name="position"></param>
        /// <returns>
        /// The screen position in normalized X, Y coordinates. Top-left is (0,0), bottom-right is (1,1)
        /// </returns>
        public static Vector2 WorldToScreen(this CameraComponent cameraComponent, Vector3 position)
        {
            var clipSpace = cameraComponent.WorldToClip(position);

            var screenSpace = new Vector2
            {
                X = (clipSpace.X + 1f) / 2f,
                Y = 1f - (clipSpace.Y + 1f) / 2f,
            };

            return screenSpace;
        }

    }
}
