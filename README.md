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

⚠️ Please read [Documentation](https://xavierarpa.gitbook.io/uniflux)

🛠 Try [UniFlux Toolkit](https://github.com/xavierarpa/UniFlux.Toolkit)



[Contact Me](mailto:arpaxavier@gmail.com)

# Installation

- You can Download at [Unity Asset Store](https://assetstore.unity.com/packages/slug/250332)

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

# Performance
| Name      | Iterations    | GC    | Time |
|-----------|--------------:|------:|-----:|
| UniFlux (Dispatch<string>)        | 10.000        | 0B        | 1ms    | 
| UniFlux (ADD Store<string>)       | 10.000        | 1.2MB     | ~3ms   | 
| UniFlux (REMOVE Store<string>)    | 10.000        | 1.2MB     | ~30ms  | 

Note: Storing (ADD and REMOVE) by design is planned to do it once so there's no problem in performance.

 # License
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
