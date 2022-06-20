# Rwlock

More friendly rwlock wraps


**\*must use using**


### Rwlock

```csharp
using Volight.LockWraps;

var rwl = new Rwlock();


using (_ = rwl.Read()) {
    // .. do anything
}
// auto exit lock


using (_ = rwl.Write()) {
    // .. do anything
}
// auto exit lock
```

### Rwlock\<T>

```csharp
var rwl = new Rwlock<int>(0);


using (var g = rwl.Read())
{
    // .. do anything
    Console.WriteLine(g.Value);
}
// auto exit lock


using (var g = rwl.Write())
{
    // .. do anything
    g.Value = 1;
}
// auto exit lock
```