using SiliconStudio.Core.MicroThreading;
using SiliconStudio.Xenko.Engine.Events;
using SiliconStudio.Xenko.Engine.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit.Engine
{
    public static class ScriptSystemExtensions
    {
        public static MicroThread AddOnEventTask<T>(
            this ScriptSystem scriptSystem, 
            EventKey<T> eventKey, Action<T> action, 
            long priority = 0L)
        {
            if (scriptSystem == null)
            {
                throw new ArgumentNullException(nameof(scriptSystem));
            }
            
            if (eventKey == null)
            {
                throw new ArgumentNullException(nameof(eventKey));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }            

            return scriptSystem.AddOnEventTask(new EventReceiver<T>(eventKey), action, priority);

        }
        

        public static MicroThread AddOnEventTask<T>(
            this ScriptSystem scriptSystem, 
            EventReceiver<T> receiver, 
            Action<T> action, 
            long priority = 0L)
        {
            if (scriptSystem == null)
            {
                throw new ArgumentNullException(nameof(scriptSystem));
            }

            if (receiver == null)
            {
                throw new ArgumentNullException(nameof(receiver));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return scriptSystem.AddTask(DoEvent, priority);

            //C# 7 Local function could also use a variable Func<Task> DoEvent = async () => { ... };
            async Task DoEvent()
            {
                while (scriptSystem.Game.IsRunning)
                {
                    action(await receiver.ReceiveAsync());
                }
            }
        }

    }
}
