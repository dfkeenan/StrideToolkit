using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrideToolkit.Demo
{
    public class SenderScript : SyncScript
    {
        public static EventKey<Vector2> EventOne = new EventKey<Vector2>("Input", "EventOne");
        public static EventKey<Vector2> EventTwo = new EventKey<Vector2>("Input", "EventTwo");

        public override void Update()
        {
            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                EventOne.Broadcast(Input.MousePosition);
            }

            if (Input.IsMouseButtonPressed(MouseButton.Right))
            {
                EventTwo.Broadcast(Input.MousePosition);
            }
        }
    }
}
