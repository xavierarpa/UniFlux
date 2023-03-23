UniFlux
===
Provee una integración comoda del patron Flux orientado para unity.
* Referencias 0 entre metodos. Permite comunicar metodos entre clases sin que nadie se conozca
* Favorece la Programación Modular, Funcional y Reactiva
* Integración de `IEnumerator, Task, IObservable<T>, IObserver<T>`. Tambien provee `UniTask`.
* Bajo Requerimiento de aprendizaje, sueles usar `MonoFlux`, `[Flux(X)]` y `X.Invoke()`
* Los Invoke que no poseen un retorno se comportan como Fire and Forget, permitiendo desactivar GameObjects sin que afecte el funcionamiento
* Permite una escalabilidad para proyectos largos
* Permite Crear proyectos rapidos sin preocuparse de comunicaciones
* Extensible a crear tus propios tipos de `Flux<TKey>,Flux<TKey,TParamOrReturn> y Flux<TKey,TParam,TReturn>`

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Getting started](#getting-started)
- [License](#license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Getting started
---
```csharp
using Kingdox.Flux; // ----> required to enable common workflow
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
var _2 = "2".Invoke<int>();
"3".Invoke<int>(42);
var _4 = "4".Invoke<int,int>(42);
//
5.Invoke();
var _6 = 2.Invoke<int>();
7.Invoke<int>(42);
var _8 = 4.Invoke<int,int>(42);
//--------
// Other Fluxes
"9".IEnumerator();
"10".Task();
"11".IObservable<int>();
"12".IObserver<int>();
//--------
// Also you can create new ways to use Flux
Kingdox.UniFlux.Core.Internal.Flux<byte>.TriggerAction(13);
var _14 = Kingdox.UniFlux.Core.Internal.Flux<bool,string>.TriggerFunc(true);
Kingdox.UniFlux.Core.Internal.Flux<bool,string>.TriggerActionParam(true,"15");
var _16 = Kingdox.UniFlux.Core.Internal.Flux<double,string, float>.TriggerFuncParam(Math.PI, "PI");
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