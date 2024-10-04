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

# MonoFlux

MonoFlux is a class that inherits MonoBehaviour but internally manages the subscription through the attributes that we include in the methods

<pre class="language-csharp" data-line-numbers data-full-width="true"><code class="lang-csharp"><strong>using UniFlux;
</strong>// Inherit your class with "MonoFlux" instead of MonoBehaviour.
public sealed class TestFlux : MonoFlux 
{
  // Set a UniFlux's Attribute like "MethodFlux" and also set the Key, "KEY" in this case.
  [MethodFlux("KEY")]
  private void OnExampleMethodIsCalled()  //This method will automatically subscribe!
  {
    Debug.Log("Hello World");
  }
}
// then next you use "KEY".Dispatch(); and OnExampleMethodIsCalled from TestFlux will be called.
</code></pre>

MonoFlux Subscribe and unsubscribe methods with attributes in the "OnEnable" or "OnDisable" events.

In Case you want to make subscriptions without attributes you might override "OnFlux" method

{% code fullWidth="true" %}
```csharp
using UniFlux;
public sealed class TestFlux : MonoFlux 
{
  protected override void OnFlux(in bool condition) 
  { 
      // "KEY" - OnExampleMethodIsCalled
      "KEY".Store(condition, OnExampleMethodIsCalled)
  }
  private void OnExampleMethodIsCalled()
  {
    Debug.Log("Hello World");
  }
}
```
{% endcode %}

In Case you want to keep using MonoBehaviour as inherit class you can handle it using onEnable and OnDisable

{% code fullWidth="true" %}
```csharp
using UniFlux;
public sealed class TestFlux : MonoBehaviour
{
  private void OnEnable() 
  { 
      "KEY".Store(true, OnExampleMethodIsCalled)
  }
  private void OnDisable() 
  { 
      "KEY".Store(false, OnExampleMethodIsCalled)
  }
  private void OnExampleMethodIsCalled()
  {
    Debug.Log("Hello World");
  }
}
```
{% endcode %}
