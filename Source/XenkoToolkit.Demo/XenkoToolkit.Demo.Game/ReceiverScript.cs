using Xenko.Core.Mathematics;
using Xenko.Core.MicroThreading;
using Xenko.Engine;
using Xenko.Engine.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XenkoToolkit.Engine;

namespace XenkoToolkit.Demo
{
    public class ReceiverScript : StartupScript
    {
        private readonly EventReceiver<Vector2> EventTwoReciever
            = new EventReceiver<Vector2>(SenderScript.EventTwo);

        public override void Start()
        {
            //Directly using EventKey so you don't have to declare EventReciever:
            Script.AddOnEventAction(SenderScript.EventOne, (Action<Vector2>)this.HandleOne);
            //Using an EventReciever:
            Script.AddOnEventAction(EventTwoReciever, (Action<Vector2>)this.HandleTwo);

            Script.AddAction(DelayedAction, TimeSpan.FromSeconds(2));

            Script.AddAction(DelayRepeatAction, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
        }

        private void HandleOne(Vector2 position)
        {
            //DebugText.Print($"Even One - {position}", new Int2(10, 10));
        }

        private void HandleTwo(Vector2 position)
        {
            //DebugText.Print($"Even Two - {position}", new Int2(10, 20));
        }

        private void DelayedAction()
        {
            //DebugText.Print("Delay", new Int2(10, 30));
        }

        private void DelayRepeatAction()
        {
            //DebugText.Print("Delay Repeat", new Int2(10, 40));
        }

        public override void Cancel()
        {
           // DebugText.Update(Game.UpdateTime);
        }


    }
}
