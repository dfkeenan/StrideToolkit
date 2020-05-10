Title: Handling events.
Description: Use ScriptSystem extensions to handle EventKey<T> events. 
Order: 1
---
### Packages
- <?# nuget "StrideToolkit" /?>

## Script Extensions
Script instance methods used for Actions/Tasks will automatically stop if `ScriptComponent`/`Entity` is removed. Other methods may need to be removed manually.

```csharp
public class ReceiverScript : StartupScript
{
    private List<MicroThread> tasks;

    public override void Start()
    {
        //Keep a list of tasks to stop on cancel
        tasks = new List<MicroThread>()
        {
            //Directly using EventKey so you don't have to declare EventReciever:
            Script.AddOnEventAction(SenderScript.SomeEvent, (position) => {
                //Do something cool!!
            }),
            
        };
    }

   
    public override void Cancel()
    {
        base.Cancel();
        //Stop handling event
        tasks.CancelAll();
    }    
}
```

Execute a method when an event occurs:

```csharp
public class ReceiverScript : StartupScript
{

    public override void Start()
    {
        //Directly using EventKey so you don't have to declare EventReciever:
        Script.AddOnEventAction(SenderScript.SomeEvent, HandleSomeEvent),
    }

    private void HandleSomeEvent(Vector2 position)
    {
        //Do something cool!!
    }
    
  
}
```