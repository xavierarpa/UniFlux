![Logo](https://kingdox.github.io/assets/img/uniflux.png)


UniFlux - Unidirectional Event Driven with Flux Integration for Unity
===

[![Unity](https://img.shields.io/badge/Unity-2019+-black.svg)](https://unity3d.com/pt/get-unity/download/archive)
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
[![Build status](https://ci.appveyor.com/api/projects/status/712fvbpoio49ee91?svg=true)](https://ci.appveyor.com/project/kingdox/uniflux)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-blueviolet)](https://makeapullrequest.com)

[![Releases](https://img.shields.io/github/release/kingdox/UniFlux.svg)](https://github.com/kingdox/UniFlux/releases)
[![UPM](https://img.shields.io/npm/v/com.kingdox.uniflux?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.kingdox.uniflux/)
<span class="badge-npmversion"><a href="https://npmjs.org/package/com.kingdox.uniflux" title="View this project on NPM"><img src="https://img.shields.io/npm/v/com.kingdox.uniflux.svg" alt="NPM version" /></a></span>

![GitHub all releases](https://shields.io./github/downloads/kingdox/UniFlux/total?logo=github)
![npm](https://shields.io./npm/dt/com.kingdox.uniflux?logo=npm)

[![CodeFactor](https://www.codefactor.io/repository/github/kingdox/uniflux/badge)](https://www.codefactor.io/repository/github/kingdox/uniflux)


#### Modules

| Module | Description |
| - | - |
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Scene-black.svg)](https://github.com/kingdox/UniFlux.Scene)|(Recomended👍) Manage the scenes and emit messages|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Input-black.svg)](https://github.com/kingdox/UniFlux.Input)|Manage "Input items" and then dispatch if conditions is right|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Click-black.svg)](https://github.com/kingdox/UniFlux.Click)|Manage Player Click interaction|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Binary-black.svg)](https://github.com/kingdox/UniFlux.Binary)|(Recomended👍) Save/Load Files in Binary |
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Updates-black.svg)](https://github.com/kingdox/UniFlux.Updates) |(Recomended👍) Subscribe and Unsubscribe Actions/Methods in a concrete framerate|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Addressables-black.svg)](https://github.com/kingdox/UniFlux.Addressables)|(Experimental🧪) Manages Addressables with primitive types|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Firebase-black.svg)](https://github.com/kingdox/UniFlux.Firebase)|Firebase Core of other UniFlux's modules|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Firebase.Firestore-black.svg)](https://github.com/kingdox/UniFlux.Firebase.Firestore)|Manages Firebase Cloud Firestore simplified|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Firebase.Database-black.svg)](https://github.com/kingdox/UniFlux.Firebase.Database)|Manages Firebase Realtime Database simplified|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.OpenAI-gold.svg)](https://github.com/kingdox/UniFlux.OpenAI)|(Working ! 🔨) Manages OpenAI API|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.OWO-black.svg)](https://github.com/kingdox/UniFlux.OWO)|Manages OWO API|
|[![Unity](https://img.shields.io/badge/Module-UniFlux.Netcode.RollBack-gold.svg)](https://github.com/kingdox/UniFlux.Netcode.RollBack)|(Working ! 🔨) Handle Network States, allowing netcode Rollback|

<!-- Intro -->
<details>
 <summary><b>Workflow</b></summary>
 
![Example_1 Event Bus](https://www.websequencediagrams.com/files/render?link=4t7a1KKbQLUGYJlwuv6r58a63GbmFoMJtwsVLS60scZNp1nlcxElJg1Ch6pfLypQ)
 
![Example_2 States](https://www.websequencediagrams.com/files/render?link=9rRV2Rfdmw1wAdCdeq0UfgVDEUVxdNXLY7tw6oO5QW7M2MKOlYGKkrVOCSeHSXOR)
 
</details>

<details>
 <summary><b>Advanced Workflow</b></summary>
 
![WorkFlow](https://www.websequencediagrams.com/files/render?link=lpHvFEnOec3XJH2t8AnKG2yrZDncSgC2IVJ8WIoVqDWCdvF7PThHRiEAVR7UBgRJ)
 
![Architecture](https://github.com/kingdox/UniFlux/blob/main/Contents/Architecture.drawio.png)
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
</details>

<!-- Examples -->
<details>
 <summary><b>Examples</b></summary>
 
```csharp
using Kingdox.UniFlux; // 1
public sealed class StarterFlux : MonoFlux // 2
{
  private void Start() => "StarterFlux.CastTest".Dispatch(); // 3
}
//...
public sealed class TestFlux : MonoFlux 
{
  [Flux("StarterFlux.CastTest")] private void CastTest() =>   Debug.Log("Hello World"); // 4
}
```

```cs
using Kingdox.UniFlux;
float _life;
public float Life
{
    [Flux("Get_Life")] get => _life;
    [Flux("Set_Life")] set 
    {
      _life = value;
      "OnChange_Life".Dispatch(value);
    }
}
//...
  [Flux("OnChange_Life")] private void OnChange_Life(float value)
  {
    // ...
  }
```

```cs
"1".Dispatch();
int _2 = "2".Dispatch<int>();
"3".Dispatch<int>(42);
int _4 = "4".Dispatch<int,int>(42);
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
using Kingdox.UniFlux.Core;
//...
Flux<byte>.Dispatch(13); //byte as key
string _14 = Flux<bool,string>.Dispatch(true); //bool as key
float _16 = Flux<double,string, float>.Dispatch(Math.PI, "PI"); //double as key
```

```cs
"42".Store(()=>{}, true); // Anonimous Subscriptions
```
</details>
 
 <!-- Instalation -->
<details>
 <summary><b>Installation</b></summary>
 
- You can use the *.unityPackage* in releases

- You can use the *.tzg in releases and add in PackageManager

- You can add in PackageManager ([How to install package from git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html))
```bash
https://github.com/kingdox/UniFlux.git
```
- You can install via openupm CLI
```bash
openupm add com.kingdox.uniflux
```
- You can install via npm
```bash
npm i com.kingdox.uniflux
```
</details>

 <!-- Special Content -->
<details>
 <summary><b>Special Content</b></summary>
 
To enable special content you must #define

| Definition | Description                |
| :-------- | :------------------------- |
| `UNIFLUX_UNITASK_SUPPORT` | Enable [Cysharp/UniTask]("https://github.com/Cysharp/UniTask") integration |
</details>

## Contibutions
Contributions are always welcome!

## Author Info
[@kingdox](https://github.com/kingdox/)

For support, email arpaxavier@gmail.com

[![Twitter](https://img.shields.io/twitter/follow/_kingdox_.svg?label=Follow&style=social)](https://twitter.com/intent/follow?screen_name=_kingdox_)   [![LinkedIn](https://img.shields.io/badge/Linkedin-0af.svg?&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/xavier-arpa-0332301a0/)  

## Special Thanks
[@Quinowl](https://github.com/Quinowl)

## License
[MIT](https://choosealicense.com/licenses/mit/)
