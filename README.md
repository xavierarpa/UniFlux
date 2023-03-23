
![Logo](https://kingdox.github.io/assets/img/uniflux.png)

UniFlux
===
#### Provides a convenient integration of the Flux pattern oriented for unity.
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

[![Releases](https://img.shields.io/github/release/kingdox/UniFlux.svg)](https://github.com/kingdox/UniFlux/releases)

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
- Invoke that do not have a return behave like Fire and Forget, allowing you to disable GameObjects.
- Allows scalability for long projects
- Allows to create fast projects without worrying about communications
- Extensible to create your own `Flux<TKey>,Flux<TKey,TParamOrReturn> and Flux<TKey,TParam,TReturn>` types.


## Installation

Soon

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
  [Flux("StarterFlux.CastTest"] private void CastTest()  // 4
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
  [Flux(Data.Key.OnTest] private void OnTest(int data) // 5
  {
    Debug.Log($"The answer of everithing {data}");
    data.Invoke();
  }
  [Flux(42)] private void OnTestAnswer() //6.2
  {
    Debug.Log($"OnTestAnswer on TestFlux");
  }
}
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

