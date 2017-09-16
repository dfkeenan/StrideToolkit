using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Rendering.Compositing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Mathematics;

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

        /// <summary>
        /// Converts the world position to clip space coordinates relative to camera.
        /// </summary>
        /// <param name="cameraComponent"></param>
        /// <param name="position"></param>
        /// <returns>
        /// The position in clip space
        /// </returns>
        public static Vector3 WorldToClip(this CameraComponent cameraComponent, Vector3 position)
        {
            if (cameraComponent == null)
            {
                throw new ArgumentNullException(nameof(cameraComponent));
            }

            return Vector3.TransformCoordinate(position, cameraComponent.ViewProjectionMatrix);           

        }

        /// <summary>
        /// Converts the world position to screen space coordinates relative to camera.
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

        /// <summary>
        /// Converts the screen position to a <see cref="RaySegment"/> in world coordinates.
        /// </summary>
        /// <param name="cameraComponent"></param>
        /// <param name="position"></param>
        /// <returns>ReaySegment, starting at near plain and ending at the far plain.</returns>
        public static RaySegment ScreenToWorldRaySegment(this CameraComponent cameraComponent, Vector2 position)
        {
            if (cameraComponent == null)
            {
                throw new ArgumentNullException(nameof(cameraComponent));
            }

            Matrix inverseViewProjection = Matrix.Invert(cameraComponent.ViewProjectionMatrix);

            Vector3 clipSpace;
            clipSpace.X = position.X * 2f - 1f;
            clipSpace.Y = 1f - position.Y * 2f;

            clipSpace.Z = 0f;
            var near = Vector3.TransformCoordinate(clipSpace, inverseViewProjection);

            clipSpace.Z = 1f;
            var far = Vector3.TransformCoordinate(clipSpace, inverseViewProjection);

            return new RaySegment(near, far);
        }

        /// <summary>
        /// Converts the screen position to a point in world coordinates.
        /// </summary>
        /// <param name="cameraComponent"></param>
        /// <param name="position">The screen position in normalized X, Y coordinates. Top-left is (0,0), bottom-right is (1,1)</param>
        /// <param name="plane">How far from the cameras near plane. 0 is the near plane, 1 is the far plane.</param>
        /// <returns>Position in world coordinates.</returns>
        public static Vector3 ScreenToWorldPoint(this CameraComponent cameraComponent, Vector2 position, float plane = 0)
        {
            if (cameraComponent == null)
            {
                throw new ArgumentNullException(nameof(cameraComponent));
            }

            Matrix inverseViewProjection = Matrix.Invert(cameraComponent.ViewProjectionMatrix);

            Vector3 clipSpace;
            clipSpace.X = position.X * 2f - 1f;
            clipSpace.Y = 1f - position.Y * 2f;

            clipSpace.Z = 0f;
            var near = Vector3.TransformCoordinate(clipSpace, inverseViewProjection);

            clipSpace.Z = 1f;
            var far = Vector3.TransformCoordinate(clipSpace, inverseViewProjection);

            return Vector3.Lerp(near, far, plane);
        }
    }
}
