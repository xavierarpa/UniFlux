UniFlux
===
Provides a convenient integration of the Flux pattern oriented for unity.
* 0 references between methods. Allows to communicate methods between classes without anyone knowing each other.
* Encourages Modular, Functional and Reactive Programming.
* Integration of `IEnumerator, Task, IObservable<T>, IObserver<T>`. Also provides `UniTask`.
* Under Learning Requirement, you usually use `MonoFlux`, `[Flux(X)]` and `X.Invoke()`.
* Invoke that do not have a return behave like Fire and Forget, allowing you to disable GameObjects without affecting performance.
* Allows scalability for long projects
* Allows to create fast projects without worrying about communications
* Extensible to create your own `Flux<TKey>,Flux<TKey,TParamOrReturn> and Flux<TKey,TParam,TReturn>` types.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Getting started](#getting-started)
- [License](#license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Getting started
---
```csharp
using Kingdox.UniFlux; // ----> required to enable common workflow
public sealed class ExampleFlux : MonoFlux // ----> required to subscribe auto Flux methods
{
    [Flux("Test"] // ----> required to handle method subscription
    private void OnTest()
    {
        Debug.Log("Test");    
    }
    
    private void Start()
    {
        "Test".invoke(); // ----> used to call OnTest method
    }   
}
```

other things

```cs
// Flux With String and Int Keys 
"1".Invoke();
int _2 = "2".Invoke<int>();
"3".Invoke<int>(42);
int _4 = "4".Invoke<int,int>(42);
//
5.Invoke();
int _6 = 2.Invoke<int>();
7.Invoke<int>(42);
int _8 = 4.Invoke<int,int>(42);
//--------
// Other Fluxes
"9".IEnumerator();
"10".Task();
"11".IObservable<int>();
"12".IObserver<int>();
//--------
// Also you can create new ways to use Flux
Kingdox.UniFlux.Core.Internal.Flux<byte>.TriggerAction(13);
string _14 = Kingdox.UniFlux.Core.Internal.Flux<bool,string>.TriggerFunc(true);
Kingdox.UniFlux.Core.Internal.Flux<bool,string>.TriggerActionParam(true,"15");
float _16 = Kingdox.UniFlux.Core.Internal.Flux<double,string, float>.TriggerFuncParam(Math.PI, "PI");
//--------
// #define UNIFLUX_UNITASK_SUPPORT
"123".UniTask();
//--------
// Allow Anonimous Subscriptions
"42".Subscribe(()=>{}, true);
```

License
---
This library is under the MIT License.
