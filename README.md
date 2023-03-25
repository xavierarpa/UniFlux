
<!-- ![Logo](https://kingdox.github.io/assets/img/uniflux.png) -->

UniFlux
===
#### Provides a convenient integration of the Flux pattern oriented for unity.
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

[![Releases](https://img.shields.io/github/release/kingdox/UniFlux.svg)](https://github.com/kingdox/UniFlux/releases)

[![UPM](https://img.shields.io/npm/v/com.kingdox.uniflux?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.kingdox.uniflux/)

[![UPM](https://img.shields.io/github/downloads/kingdox/UniFlux/total?style=social)](https://img.shields.io/github/downloads/kingdox/UniFlux/total?style=social)



## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage/Examples](#usage-examples)
- [Special Content](#special-content)
- [Contributing](#contributing)
- [Author Info](#author-info)
- [License](#license)

## Features

- 0 references between methods. Allows to communicate methods between classes without anyone knowing each other.
- Encourages Modular, Functional and Reactive Programming.
- Integration of `IEnumerator, Task, IObservable<T>, IObserver<T>`. Also provides `UniTask`.
- Under Learning Requirement, you usually use `MonoFlux`, `[Flux(X)]` and `X.Invoke()`.
- Invokes that do not have a return behave like Fire and Forget.
- Allows scalability for long projects
- Allows to create fast projects without worrying about communications
- Extensible to create your own `Flux<TKey>,Flux<TKey,TParamOrReturn> and Flux<TKey,TParam,TReturn>` types.


## Installation

You can install via openupm CLI
```bash
openupm add com.kingdox.uniflux
```

Also you can use the *.unityPackage* in releases


## Usage/Examples

```csharp
namespace Data
{
  public static partial class Key
  {
    public const string OnTest = nameof(OnTest);
  }
}
//...
using Kingdox.UniFlux; // 1
public sealed class StarterFlux : MonoFlux // 2
{
  private void Start() 
  {
    "StarterFlux.CastTest".Invoke(); // 3
  }
  [Flux("StarterFlux.CastTest")] private void CastTest()  // 4
  {
    Data.Key.OnTest.Invoke(42);
  }
  [Flux(42)] private void OnTestAnswer() // 6.1
  {
    Debug.Log($"OnTestAnswer on StarterFlux");
  }
}
//...
public sealed class TestFlux : MonoFlux 
{
  [Flux(Data.Key.OnTest)] private void OnTest(int data) // 5
  {
    Debug.Log($"The answer of everything {data}");
    data.Invoke();
  }
  [Flux(42)] private void OnTestAnswer() //6.2
  {
    Debug.Log($"OnTestAnswer on TestFlux");
  }
}
```

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


## Special Content

To enable special content you must #define

| Definition | Description                |
| :-------- | :------------------------- |
| `UNIFLUX_UNITASK_SUPPORT` | Enable [Cysharp/UniTask]("https://github.com/Cysharp/UniTask") integration |


## Contributing

Contributions are always welcome!



## Author Info

[@kingdox](https://github.com/kingdox/)

For support, email arpaxavier@gmail.com

## License

[MIT](https://choosealicense.com/licenses/mit/)

