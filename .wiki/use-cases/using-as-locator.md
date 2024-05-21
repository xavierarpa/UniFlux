---
layout:
  title:
    visible: true
  description:
    visible: false
  tableOfContents:
    visible: true
  outline:
    visible: false
  pagination:
    visible: true
---

# Using as Locator

One of the disadvantages that I have seen of using Service Locator type patterns is usually the generation of garbage every time it is called, generating poor performance if high utilization is reached. This is because many times the service is searched using the Type type, forcing you to do things like typeof(T).

This is a typical example of a Service Locator:

<pre class="language-csharp" data-full-width="true"><code class="lang-csharp">using System;
using System.Collections;
using System.Collections.Generic;
using UniFlux;
using UnityEngine;
public class ServiceLocator
{
    private static readonly Dictionary&#x3C;Type, object> services = new Dictionary&#x3C;Type, object>();

    public static void RegisterService&#x3C;T>(T service)
    {
        var type = typeof(T); // Look here, typeof causing garbage
        if (!services.ContainsKey(type))
        {
            services.Add(type, service);
        }
    }

    public static T GetService&#x3C;T>()
    {
        var type = typeof(T); // Look here, typeof causing garbage
        if (services.ContainsKey(type))
        {
            return (T)services[type]; // look here, (T) making explicit convertions
        }
        throw new Exception($"Service of type {type} not registered.");
    }
}

<strong>class Example_A : Monobehaviour, IDataService
</strong>{
    void Awake()
    {
        // Generate
        ServiceLocator.RegisterService&#x3C;IDataService>(this);
    }
    void IDataService.SaveData(string data)
    {
        // etc...
    }
}

class Example_B : Monobehaviour
{
    void Start()
    {   
        // Getting the instances..
        var dataService = ServiceLocator.GetService&#x3C;IDataService>();
        
        // Using it
        dataService.SaveData("Sample data");
    }
}
</code></pre>

With UniFlux you can not only optimize the way information is treated, but you can even choose the way you want to interact with it.

Now let's do an example using UniFlux as Service Locator:

{% code fullWidth="true" %}
```csharp
using UniFlux;
using UnityEngine;
class Example_A : MonoFlux, IDataService
{
    [MethodFlux(nameof(IDataService))] IDataService Get_IDataService_ExampleMethod() => this;
    
    void IDataService.SaveData(string data)
    {
        // etc...
    }
}

class Example_B : Monobehaviour
{
    void Start()
    {   
        var dataService = nameof(IDataService).Dispatch<IDataService>();
        // Using it
        dataService.SaveData("Sample data");
    }
}
```
{% endcode %}

In this example we can do the same treatment without having to lose performance in our execution, this is one way to do it but you can do it in different ways

You could even deal directly with the method ignoring the need for the interface:

{% code fullWidth="true" %}
```csharp
using UniFlux;
using UnityEngine;
class Example_A : MonoFlux
{   
    [MethodFlux("Service.SaveData")] void SaveData(string data)
    {
        // etc...
    }
}

class Example_B : Monobehaviour
{
    void Start()
    {   
        "Service.SaveData".Dispatch("Sample data");
    }
}
```
{% endcode %}

By working in this way you are not forced to depend on an interface, you already have the choice of how to adjust it to your needs.

There are other alternatives such as state management in case you have services that may change over time, here is an example

{% code fullWidth="true" %}
```csharp
using UniFlux;
using UnityEngine;
class Example_A : MonoFlux, IDataService
{
    private void Start()
    {
        nameof(IDataService).DispatchState<IDataService>(this);
    }

    void IDataService.SaveData(string data)
    {
        // etc...
    }
}
class Example_B : MonoFlux, ILoggerService, IDataService
{
    private void Start()
    {
        nameof(IDataService).DispatchState<IDataService>(this);
        nameof(ILoggerService).DispatchState<ILoggerService>(this);
    }
    
    void ILoggerService.Log(string data)
    {
        // etc...
    }
    void IDataService.SaveData(string data)
    {
        // etc...
    }
}

class Example_C : Monobehaviour
{
    IDataService IDataService;
    ILoggerService ILoggerService;

    [StateFlux(nameof(IDataService))] private void OnIDataService(IDataService state) => IDataService=state;
    
    [StateFlux(nameof(ILoggerService))] private void OnILoggerService(ILoggerService state) => ILoggerService=state;
    
    void Start()
    {   
        IDataService.SaveData("Sample data");
        ILoggerService.Log("Hi");
    }
}
```
{% endcode %}



