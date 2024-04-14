![Logo](https://repository-images.githubusercontent.com/616052596/1a10ad21-e1ef-4a8f-a05a-64df9b02411f)

UniFlux - Flexible Event Driven and Flux for Unity
===

[![Unity](https://img.shields.io/badge/Unity-2019+-black.svg)](https://unity3d.com/pt/get-unity/download/archive)
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
[![Build status](https://ci.appveyor.com/api/projects/status/712fvbpoio49ee91?svg=true)](https://ci.appveyor.com/project/kingdox/uniflux)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-blueviolet)](https://makeapullrequest.com)

[![Releases](https://img.shields.io/github/release/xavierarpa/UniFlux.svg)](https://github.com/xavierarpa/UniFlux/releases)
[![UPM](https://img.shields.io/npm/v/com.xavierarpa.uniflux?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.xavierarpa.uniflux/)
<span class="badge-npmversion"><a href="https://npmjs.org/package/com.xavierarpa.uniflux" title="View this project on NPM"><img src="https://img.shields.io/npm/v/com.xavierarpa.uniflux.svg" alt="NPM version" /></a></span>

![GitHub all releases](https://shields.io./github/downloads/xavierarpa/UniFlux/total?logo=github)
![npm](https://shields.io./npm/dt/com.xavierarpa.uniflux?logo=npm)

[![CodeFactor](https://www.codefactor.io/repository/github/xavierarpa/uniflux/badge)](https://www.codefactor.io/repository/github/xavierarpa/uniflux)

  <!-- Examples -->
<details>
 <summary><b>Examples</b></summary>

 In this example, we call CastTest via "StarterFlux.CastTest" key
```csharp
using UniFlux; // 1
public sealed class StarterFlux : MonoFlux // 2
{
  private void Start() => "StarterFlux.CastTest".Dispatch(); // 3
}
//...
public sealed class TestFlux : MonoFlux 
{
  [MethodFlux("StarterFlux.CastTest")] private void CastTest() =>   Debug.Log("Hello World"); // 4
}
```

Here we can use a local state and get's a reactive behaviour using "OnChange_Life", also we can call it using "Set_Life" or get the current state with "Get_Life"...
```cs
using UniFlux;
float _life;
public float Life
{
    [MethodFlux("Get_Life")] get => _life;
    [MethodFlux("Set_Life")] set 
    {
      _life = value;
      "OnChange_Life".Dispatch(value);
    }
}
//...
  [MethodFlux("OnChange_Life")] private void OnChange_Life(float value)
  {
    // ...
  }
```

Here are examples of what you can do:
```cs
"1".Dispatch(); // - Send a Message
int _2 = "2".Dispatch<int>(); // - Send a Message and return a value
"3".Dispatch<int>(42); // - Send a Message with an argument
int _4 = "4".Dispatch<int,int>(42); // - Send a Message with an argument and return a value
```

Also we made easily handle IEnumerators, Task and UniTask
```cs
"9".IEnumerator();
"10".Task();

// #define UNIFLUX_UNITASK_SUPPORT
// To enable UniTask integration from https://github.com/Cysharp/UniTask"
"123".UniTask();
```

You can use the KEY type as an TaskAwaiter, calling Task cast implicit !
```cs
private static async Task Example()
{
  await "KEY"; // Calls "KEY".Task();
}
```

Also can create anonimous subscriptions in case you don't want to do a method (not recommended)
```cs
"42".Store(()=>{}, true); // Anonimous Subscriptions
```
</details>

<!-- Performance -->
<details>
 <summary><b>Performance</b></summary>

Compared methods of UniFlux
| Name      | Iterations    | GC    | Time |
|-----------|--------------:|------:|-----:|
| UniFlux (Dispatch int )           | 10.000        | 0B        | 0ms    | 
| UniFlux (Dispatch string )        | 10.000        | 0B        | 1ms    | 
| UniFlux (Store int  ADD)          | 10.000        | 1.2MB     | ~3ms   |
| UniFlux (Store string  ADD)       | 10.000        | 1.2MB     | ~3ms   | 
| UniFlux (Store int  REMOVE)       | 10.000        | 1.2MB     | ~30ms  |
| UniFlux (Store string  REMOVE)    | 10.000        | 1.2MB     | ~30ms  | 

look how nice work Dispatching interger and string, Storing by design is planned to do it once so there's no problem in performance.
</details>
 
 <!-- Instalation -->
<details>
 <summary><b>Installation</b></summary>
 
- You can use the *.unityPackage* in releases

- You can use the *.tzg in releases and add in PackageManager

- You can add in PackageManager ([How to install package from git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html))
```bash
https://github.com/xavierarpa/UniFlux.git
```
- You can install via openupm CLI
```bash
openupm add com.xavierarpa.uniflux
```
- You can install via npm
```bash
npm i com.xavierarpa.uniflux
```
</details>

<details>
 <summary><b>Author Info</b></summary>
 
[@xavierarpa](https://github.com/xavierarpa/)

For support, email arpaxavier@gmail.com

[![Twitter](https://img.shields.io/twitter/follow/xavier_arpa.svg?label=Follow&style=social)](https://twitter.com/intent/follow?screen_name=xavier_arpa)   [![LinkedIn](https://img.shields.io/badge/Linkedin-0af.svg?&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/xavier-arpa-0332301a0/)  

</details>

<details>
 <summary><b>License</b></summary>
 
[MIT](https://choosealicense.com/licenses/mit/)

<pre>
MIT License

Copyright (c) 2023 Xavier Thomas Peter Arpa Lopez

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
</pre>

</details>
