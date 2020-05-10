Title: Miscellaneous.
Description: Examples without a home. 
Order: 1000
---
### Packages
- <?# nuget "StrideToolkit" /?>

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