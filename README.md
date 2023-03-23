# flux


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
