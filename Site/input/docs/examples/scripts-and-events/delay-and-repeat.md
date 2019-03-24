Title: Delay and repeat.
Description: Use ScriptSystem extensions to delay or repeat executtion of methods. 
Order: 1
---
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