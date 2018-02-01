using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Engine.Events;
using System;
using System.Threading.Tasks;
using XenkoToolkit.Engine;

namespace XenkoToolkit.Demo
{
    public class ReceiverScript: StartupScript
    {        
        private readonly EventReceiver<Vector2> EventTwoReciever 
            = new EventReceiver<Vector2>(SenderScript.EventTwo);

        public override void Start()
        {
            //Directly using EventKey so you don't have to declare EventReciever:
            Script.AddOnEventTask(SenderScript.EventOne, HandleOne);
            //Using an EventReciever:
            Script.AddOnEventTask(EventTwoReciever, HandleTwo);
        }

        private void HandleOne(Vector2 position)
        {
            DebugText.Print($"Even One - {position}", new Int2(10, 10));
        }

        private void HandleTwo(Vector2 position)
        {
            DebugText.Print($"Even Two - {position}", new Int2(10, 10));
        }
    }
}
