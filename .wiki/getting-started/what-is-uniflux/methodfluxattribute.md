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

# MethodFluxAttribute

MethodFlux allows you to subscribe to methods as events (such as System.Action or System.Func). Depending on how the method in question is written, it will direct the subscription to the place where it corresponds. In turn, we will need to call it with specific instructions.

Here is a simple example

<pre class="language-csharp" data-line-numbers data-full-width="true"><code class="lang-csharp"><strong>using UniFlux;
</strong>public sealed class TestFlux : MonoFlux 
{
  // "1".Dispatch()
  [MethodFlux("1")] private void OnExample_1()
  {
    Debug.Log("Hello World");
  }

  // "2".Dispatch(42)
  [MethodFlux("2")] private void OnExample_2(int value)
  {
    Debug.Log(value);
  }

  //string response = "3".Dispatch&#x3C;string>()
  [MethodFlux("3")] private string OnExample_3() 
  {
    return value.ToString();
  }

  //string response = "4".Dispatch&#x3C;int, string>(42)  
  [MethodFlux("4")] private string OnExample_4(int value)
  {
    return value.ToString();
  }
}
</code></pre>

It must be taken into account that only one of the parameters to be sent can be sent. For optimization reasons, it is prohibited to include 2 arguments as parameters. The alternatives to avoid this possible problem are to create Tuples, classes, structs or records.

{% code lineNumbers="true" fullWidth="true" %}
```csharp
using UniFlux;
public sealed class TestFlux : MonoFlux 
{
    void Start()
    {
        // string.Dispatch<(int, string, bool)>()
        "Test.Tuples".Dispatch( (69, "96f", true) );
        
        // string.Dispatch<Vector2>()
        "Test.Struct".Dispatch( Vector2.zero );
    }
    
    [MethodFlux("Test.Tuples")] private void OnExample((int, string, bool) value)
    {
        Debug.Log(value);
    }
    [MethodFlux("Test.Struct")] private void OnExample(Vector2 value)
    {
        Debug.Log(value);
    }
}
```
{% endcode %}
