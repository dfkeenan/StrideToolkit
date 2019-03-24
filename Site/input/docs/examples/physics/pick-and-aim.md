Title: Pick and aim
Description: Use camera, physics and transform extensions to look at selected entity. 
Order: 1
---
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