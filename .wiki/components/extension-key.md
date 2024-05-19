---
layout:
  title:
    visible: true
  description:
    visible: false
  tableOfContents:
    visible: true
  outline:
    visible: true
  pagination:
    visible: false
---

# Extension Key

The Extension Key is a name invented to refer to the class that handles the extensions of the data type that we want to use as a key, things like for example "KEY".Dispatch(); implies that a string type key is being used

By default we allow the use of string and int. But that doesn't stop you from wanting to include more key types, such as a custom enum.

Here is a simple example

{% code lineNumbers="true" fullWidth="true" %}
```csharp
//######################## 
// String Key
//######################## 
"StringKey".Dispatch();
"StringKey".Store();
"StringKey".DispatchState(true);
"StringKey".StoreState(false, OnFakeStateMethod);
"StringKey".IEnumerator();
"StringKey".Task();
private static async Task Example()
{
  await "StringKey"; // Calls "StringKey".Task();
}

//######################## 
// Int Key
//######################## 
42.Dispatch();
42.Store();
42.DispatchState(true);
42.StoreState(false, OnFakeStateMethod);
42.IEnumerator();
42.Task();
private static async Task Example()
{
  await 42; // Calls 42.Task();
}

//######################## 
// CustomEnumEvent Key
//######################## 
CustomEnumEvent.OnPlayerDead.Dispatch();
CustomEnumEvent.OnPlayerDead.Store();
CustomEnumEvent.OnPlayerDead.DispatchState(true);
CustomEnumEvent.OnPlayerDead.StoreState(false, OnFakeStateMethod);
CustomEnumEvent.OnPlayerDead.IEnumerator();
CustomEnumEvent.OnPlayerDead.Task();
private static async Task Example()
{
  await CustomEnumEvent.OnPlayerDead; // Calls CustomEnumEvent.OnPlayerDead.Task();
}
```
{% endcode %}

For more information about how to create your own Custom Key take a look here

{% content-ref url="../tutorials/how-to-use-a-custom-key.md" %}
[how-to-use-a-custom-key.md](../tutorials/how-to-use-a-custom-key.md)
{% endcontent-ref %}
