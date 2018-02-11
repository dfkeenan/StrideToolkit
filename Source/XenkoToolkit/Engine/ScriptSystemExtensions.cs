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


        public static MicroThread AddOnEventTaskAsync<T>(
           this ScriptSystem scriptSystem,
           EventKey<T> eventKey, Func<T,Task> action,
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

            return scriptSystem.AddOnEventTaskAsync(new EventReceiver<T>(eventKey), action, priority);

        }


        public static MicroThread AddOnEventTaskAsync<T>(
            this ScriptSystem scriptSystem,
            EventReceiver<T> receiver,
            Func<T, Task> action,
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
                    await action(await receiver.ReceiveAsync());
                }
            }
        }


        public static MicroThread AddTask(
           this ScriptSystem scriptSystem,
           Func<Task> action,
           TimeSpan delay,
           long priority = 0L)
        {
            if (scriptSystem == null)
            {
                throw new ArgumentNullException(nameof(scriptSystem));
            }          

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return scriptSystem.AddTask(DoTask, priority);

            //C# 7 Local function could also use a variable Func<Task> DoEvent = async () => { ... };
            async Task DoTask()
            {
                while (scriptSystem.Game.IsRunning && delay >= TimeSpan.Zero)
                {
                    delay -= scriptSystem.Game.UpdateTime.Elapsed;
                    await scriptSystem.NextFrame();
                }

                await action();
            }
        }

        public static MicroThread AddTask(
           this ScriptSystem scriptSystem,
           Action action,
           TimeSpan delay,
           long priority = 0L)
        {
            if (scriptSystem == null)
            {
                throw new ArgumentNullException(nameof(scriptSystem));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if(delay <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(delay), "Must be greater than zero.");
            }

            return scriptSystem.AddTask(DoTask, priority);

            //C# 7 Local function could also use a variable Func<Task> DoEvent = async () => { ... };
            async Task DoTask()
            {
                while (scriptSystem.Game.IsRunning && delay >= TimeSpan.Zero)
                {
                    delay -= scriptSystem.Game.UpdateTime.Elapsed;
                    await scriptSystem.NextFrame();
                }

                action();
            }
        }

        public static MicroThread AddTask(
           this ScriptSystem scriptSystem,
           Func<Task> action,
           TimeSpan delay,
           TimeSpan repeatEvery,
           long priority = 0L)
        {
            if (scriptSystem == null)
            {
                throw new ArgumentNullException(nameof(scriptSystem));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (delay <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(delay), "Must be greater than zero.");
            }

            return scriptSystem.AddTask(DoTask, priority);

            //C# 7 Local function could also use a variable Func<Task> DoEvent = async () => { ... };
            async Task DoTask()
            {
                var elapsedTime = new TimeSpan(0);

                while (scriptSystem.Game.IsRunning)
                {
                    elapsedTime += scriptSystem.Game.UpdateTime.Elapsed;

                    if (elapsedTime >= delay)
                    {
                        elapsedTime -= delay;
                        delay = repeatEvery;
                        await action();
                    }
                    await scriptSystem.NextFrame();
                }
            }
        }

        public static MicroThread AddTask(
           this ScriptSystem scriptSystem,
           Action action,
           TimeSpan delay,
           TimeSpan repeatEvery,
           long priority = 0L)
        {
            if (scriptSystem == null)
            {
                throw new ArgumentNullException(nameof(scriptSystem));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (delay <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(delay), "Must be greater than zero.");
            }

            if (repeatEvery <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(repeatEvery), "Must be greater than zero.");
            }

            return scriptSystem.AddTask(DoTask, priority);

            //C# 7 Local function could also use a variable Func<Task> DoEvent = async () => { ... };
            async Task DoTask()
            {
                var elapsedTime = new TimeSpan(0);

                while (scriptSystem.Game.IsRunning)
                {
                    elapsedTime += scriptSystem.Game.UpdateTime.Elapsed;

                    if(elapsedTime >= delay)
                    {
                        elapsedTime -= delay;
                        delay = repeatEvery;
                        action();
                    }                    
                    await scriptSystem.NextFrame();
                }
            }
        }

        public static void CancelAll(this ICollection<MicroThread> microThreads)
        {
            if (microThreads == null)
            {
                throw new ArgumentNullException(nameof(microThreads));
            }

            foreach (var thread in microThreads)
            {
                thread.Cancel();
            }

            microThreads.Clear();
        }
    }
}
