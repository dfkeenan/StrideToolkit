# Xenko Toolkit

Xenko Toolkit is a .NET Standard library for use with the [Xenko Game Engine](https://xenko.com/). It's goal is to add extra extensions and utilites to make things a bit easier.

# Installing

Coming Soon&trade;

    Install-Package XenkoToolkit -Prerelease

## NuGet Package - Xenko Engine compatibility

This table indicates which version of Xenko each version of the toolkit was compiled against.

|Branch|NuGet|Xenko|
|:--:|:--:|:--:|
|master|0.1.0.0-alpha|2.1.1.1|

## Examples
Find `Component`s more easily:
```csharp
//Find component in self or children
var model = this.Entity.GetComponentInChildren<ModelComponent>();
//Find all components in self and children
var models = this.Entity.GetComponentsInChildren<ModelComponent>();
```

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

Execute a method when an event occurs:

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
            Script.AddOnEventAction(SenderScript.SomeEvent, HandleSomeEvent),
            
        };
    }

    private void HandleSomeEvent(Vector2 position)
    {
        //Do something cool!!
    }
    
    public override void Cancel()
    {
        base.Cancel();
        //Stop handling event
        tasks.CancelAll();
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

## Building From Source

### Prerequisites

1. Xenko (and it's prerequisites for Windows).

### Building

...

## Versioning

...

## License

This project is licensed under the APACHE 2.0 License - see the [LICENSE.txt](LICENSE.txt) file for details
