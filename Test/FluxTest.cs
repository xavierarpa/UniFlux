/*
Copyright (c) 2023 Xavier Arpa López Thomas Peter ('Kingdox')

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
*/
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Kingdox.Flux.Test
{
    public sealed class FluxTest : MonoFlux
    {
        public const string OnWait = "OnWait";
        protected override void OnEnableDisable(bool condition)
        {
            //
            // NORMAL
            //
            OnWait.Subscribe(Method,condition); // Method
            OnWait.Subscribe<string>(MethodParam,condition); // Method Param
            OnWait.Subscribe<string>(MethodReturn,condition); // Method Return
            OnWait.Subscribe<string, string>(MethodParamReturn,condition); // Method Param Return
            //
            // IENUMERATOR
            //
            OnWait.Subscribe(Yield, condition); // Method
            OnWait.Subscribe<string, IEnumerator>(YieldParam, condition); // Method Param
            OnWait.Subscribe<IEnumerator<string>>(YieldReturn, condition); // Method Return
            OnWait.Subscribe<string, IEnumerator<string>>(YieldParamReturn, condition); // Method Param Return
            //
            // TASK
            //
            OnWait.Subscribe(Await, condition); // Method
            OnWait.Subscribe<string, Task>(AwaitParam, condition); // Method Param
            OnWait.Subscribe<Task<string>>(AwaitReturn, condition); // Method Return
            OnWait.Subscribe<string, Task<string>>(AwaitParamReturn, condition); // Method Param Return
        }

        private async void Start()
        {
            //
            // NORMAL
            //
            OnWait.Invoke();
            OnWait.Invoke<string>("MethodParam");
            Debug.Log(OnWait.Invoke<string>());
            Debug.Log(OnWait.Invoke<string, string>("MethodParamReturn"));
            //
            // IENUMERATOR
            //
            StartCoroutine(OnWait.Invoke<IEnumerator>());
            StartCoroutine(OnWait.Invoke<string, IEnumerator>("a"));
            StartCoroutine(OnWait.Invoke<IEnumerator<string>>());
            StartCoroutine(OnWait.Invoke<string, IEnumerator<string>>("a"));
            //
            // TASK
            //
            await OnWait.Invoke<Task>();
            await OnWait.Invoke<string, Task>("a");
            await OnWait.Invoke<Task<string>>();
            await OnWait.Invoke<string, Task<string>>("a");
        }
        [Subscribe(OnWait)] private void Method() => Debug.Log("Method");
        [Subscribe(OnWait)] private void MethodParam(string a) => Debug.Log(a);
        [Subscribe(OnWait)] private string MethodReturn() => "MethodReturn";
        [Subscribe(OnWait)] private string MethodParamReturn(string a) => a;
        [Subscribe(OnWait)] private IEnumerator Yield() 
        {
            Debug.Log("Yield");
            yield return null;
        }
        [Subscribe(OnWait)] private IEnumerator YieldParam(string a) 
        {
            Debug.Log("YieldParam");
            yield return null;
        }
        [Subscribe(OnWait)] private IEnumerator<string> YieldReturn() 
        {
            Debug.Log("YieldReturn");
            yield return null;
        }
        [Subscribe(OnWait)] private IEnumerator<string> YieldParamReturn(string data) 
        {
            Debug.Log("YieldParamReturn");
            yield return null;
        }
        [Subscribe(OnWait)] private async Task Await() 
        {
            Debug.Log("Await");
            await Task.Yield();
        }
        [Subscribe(OnWait)] private async Task AwaitParam(string a) 
        {
            Debug.Log("AwaitParam");
            await Task.Yield();
        }
        [Subscribe(OnWait)] private async Task<string> AwaitReturn() 
        {
            Debug.Log("AwaitReturn");
            await Task.Yield();
            return "a";
        }
        [Subscribe(OnWait)] private async Task<string> AwaitParamReturn(string data) 
        {
            Debug.Log("AwaitParamReturn");
            await Task.Yield();
            return "a";
        }
    }
}
