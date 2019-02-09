## Entity Extensions
Find `Component`s more easily:
```csharp
//Find component in self or children
var model = this.Entity.GetComponentInChildren<ModelComponent>();
//Find all components in self and children
var models = this.Entity.GetComponentsInChildren<ModelComponent>();
```

## Physics/Input/Transform Extensions
Pick and aim at an `Entity`:

```csharp
if (Input.IsMouseButtonPressed(SiliconStudio.Xenko.Input.MouseButton.Left))
{   
    var ray = MainCamera.ScreenToWorldRaySegment(Input.MousePosition);

    var hitResult = this.GetSimulation().Raycast(ray);
    if (hitResult.Succeeded)
    {
        target = hitResult.Collider.Entity;
    }
}

if(target != null)
{
    MainCamera.Entity.Transform.LookAt(target.Transform, Game.GetDeltaTime() * 3.0f);
}
```
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

Execute methods at a delayed time:
```csharp
Script.AddAction(DelayedAction, TimeSpan.FromSeconds(2));
//async method
Script.AddTask(DelayedTask, TimeSpan.FromSeconds(2));
```

Repeat method execute at a given time span:
```csharp
Script.AddAction(RepeatedAction, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
//async method
Script.AddTask(RepeatedTask, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
```
## Animation/Easing Functions
Animate a property using an easing function:
```csharp
//Instantiate prefab at a given postion
var sphere = SpherePrefab.InstantiateSingle(FirstPosition);
Entity.Scene.Entities.Add(sphere);

Script.AddOverTimeAction((progress) =>
{
    sphere.Transform.Position = MathUtilEx.Interpolate(startPosition, endPosition, progress,EasingFunction.ElasticEaseOut);

}, TimeSpan.FromSeconds(2));
```


