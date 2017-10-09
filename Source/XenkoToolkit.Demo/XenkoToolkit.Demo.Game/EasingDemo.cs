using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Engine;
using XenkoToolkit.Mathematics;

namespace XenkoToolkit.Demo
{
    public class EasingDemo : AsyncScript
    {
        public Vector3 FirstPosition { get; set; }

        public float Height { get; set; }

        public Prefab SpherePrefab { get; set; }

        public override async Task Execute()
        {
            var easingFunctions = (EasingFunction[])Enum.GetValues(typeof(EasingFunction));

            var startPositions = new List<Vector3>();
            var transforms = new List<TransformComponent>();

            for (int i = 0; i < easingFunctions.Length; i++)
            {
                Vector3 startPosition = FirstPosition + new Vector3(i, 0, 0);
                var sphere = SpherePrefab.InstantiateSingle(startPosition);

                transforms.Add(sphere.Transform);
                startPositions.Add(startPosition);
                Entity.Scene.Entities.Add(sphere);
            }

            var bottom = new Vector3(0, Height,0);

            var duration = TimeSpan.FromSeconds(2);
            var elapsed = TimeSpan.Zero;


            while (Game.IsRunning)
            {
                var progress = (float)(elapsed.TotalSeconds / duration.TotalSeconds);

                if (progress > 1.0f)
                    progress = 1.0f;

                DebugText.Print($"Progress = {progress}", new Int2(10));

                for (int i = 0; i < transforms.Count; i++)
                {
                    var t = transforms[i];

                    var easing = easingFunctions[i];

                    t.Position = MathUtilEx.Interpolate(startPositions[i], startPositions[i] - bottom, progress, easing);

                }                

                elapsed += Game.UpdateTime.Elapsed;

                if (Input.IsKeyPressed(SiliconStudio.Xenko.Input.Keys.Space))
                {
                    //reset
                    elapsed = TimeSpan.Zero;
                }

                await Script.NextFrame();
            }

            
        }

        public override void Cancel()
        {
            base.Cancel();
            DebugText.Update(Game.UpdateTime);
        }
    }
}
