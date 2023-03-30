
<!-- ![Logo](https://kingdox.github.io/assets/img/uniflux.png) -->

UniFlux
===
#### Provides a convenient integration of the Flux pattern oriented for unity.
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

[![Releases](https://img.shields.io/github/release/kingdox/UniFlux.svg)](https://github.com/kingdox/UniFlux/releases)

[![UPM](https://img.shields.io/npm/v/com.kingdox.uniflux?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.kingdox.uniflux/)



## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage/Examples](#usage-examples)
- [Special Content](#special-content)
- [Contributing](#contributing)
- [Author Info](#author-info)
- [Special Thanks](#special-thanks)
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

- You can use the *.unityPackage* in releases

- You can use the *.tzg in releases and add in PackageManager

- You can add in PackageManager
```bash
https://github.com/kingdox/UniFlux.git
```
- You can install via openupm CLI
```bash
openupm add com.kingdox.uniflux
```

## Usage/Examples
#### Common Scenarios
```csharp
using Kingdox.UniFlux; // 1
public sealed class StarterFlux : MonoFlux // 2
{
  private void Start() => "StarterFlux.CastTest".Invoke(); // 3
}
//...
public sealed class TestFlux : MonoFlux 
{
  [Flux("StarterFlux.CastTest")] private void CastTest() =>   Debug.Log("Hola Mundo"); // 4
}
```

```cs
float _life;
public float Life
{
    [Flux("Get_Life")] get => _life;
    [Flux("Set_Life")] set => _life = value;
}
```

```cs
"1".Invoke();
int _2 = "2".Invoke<int>();
"3".Invoke<int>(42);
int _4 = "4".Invoke<int,int>(42);
```

```cs
"9".IEnumerator();
"10".Task();
```

```cs
// #define UNIFLUX_UNITASK_SUPPORT
"123".UniTask();
```

#### Advanced features

```cs
using Kingdox.UniFlux.Core.Internal;
//...
Flux<byte>.TriggerAction(13); //byte as key
string _14 = Flux<bool,string>.TriggerFunc(true); //bool as key
float _16 = Flux<double,string, float>.TriggerFuncParam(Math.PI, "PI"); //double as key
```

```cs
"42".Subscribe(()=>{}, true); // Anonimous Subscriptions
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

## Special Thanks

[@Quinowl](https://github.com/Quinowl)

## License

[MIT](https://choosealicense.com/licenses/mit/)

