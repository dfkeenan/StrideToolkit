Title: Finding Components
Description: Searching entity hierarchy for components. 
Order: 1
---
## Entity Component Search Extensions
Find `Component` in children and self:
```csharp
//Find component in self or children
var model = this.Entity.GetComponentInChildrenAndSelf<ModelComponent>();
//Find all components in self and children
var models = this.Entity.GetComponentInChildrenAndSelf<ModelComponent>();
```