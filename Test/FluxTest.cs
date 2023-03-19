using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Kingdox.Flux;

public class FluxTest : MonoBehaviour
{
    public const string OnWait = "OnWait";

    // Start is called before the first frame update
    private async void Start()
    {
        //
        // NORMAL
        //
        OnWait.Subscribe(Method,true); // Method
        OnWait.Subscribe<string>(MethodParam,true); // Method Param
        OnWait.Subscribe<string>(MethodReturn,true); // Method Return
        OnWait.Subscribe<string, string>(MethodParamReturn,true); // Method Param Return
        OnWait.Invoke();
        OnWait.Invoke<string>("MethodParam");
        Debug.Log(OnWait.Invoke<string>());
        Debug.Log(OnWait.Invoke<string, string>("MethodParamReturn"));
        //
        // IENUMERATOR
        //
        OnWait.Subscribe(Yield, true); // Method
        OnWait.Subscribe<string, IEnumerator>(YieldParam, true); // Method Param
        OnWait.Subscribe<IEnumerator<string>>(YieldReturn, true); // Method Return
        OnWait.Subscribe<string, IEnumerator<string>>(YieldParamReturn, true); // Method Param Return
        StartCoroutine(OnWait.Invoke<IEnumerator>());
        StartCoroutine(OnWait.Invoke<string, IEnumerator>("a"));
        StartCoroutine(OnWait.Invoke<IEnumerator<string>>());
        StartCoroutine(OnWait.Invoke<string, IEnumerator<string>>("a"));
        //
        // TASK
        //
        OnWait.Subscribe(Await, true); // Method
        OnWait.Subscribe<string, Task>(AwaitParam, true); // Method Param
        OnWait.Subscribe<Task<string>>(AwaitReturn, true); // Method Return
        OnWait.Subscribe<string, Task<string>>(AwaitParamReturn, true); // Method Param Return
        await OnWait.Invoke<Task>();
        await OnWait.Invoke<string, Task>("a");
        await OnWait.Invoke<Task<string>>();
        await OnWait.Invoke<string, Task<string>>("a");
    }

    private void Method() => Debug.Log("Method");
    private void MethodParam(string a) => Debug.Log(a);
    private string MethodReturn() => "MethodReturn";
    private string MethodParamReturn(string a) => a;

    private IEnumerator Yield() 
    {
        Debug.Log("Yield");
        yield return null;
    }
    private IEnumerator YieldParam(string a) 
    {
        Debug.Log("YieldParam");
        yield return null;
    }
    private IEnumerator<string> YieldReturn() 
    {
        Debug.Log("YieldReturn");
        yield return null;
    }
    private IEnumerator<string> YieldParamReturn(string data) 
    {
        Debug.Log("YieldParamReturn");
        yield return null;
    }

    private async Task Await() 
    {
        Debug.Log("Await");
        await Task.Yield();
    }
    private async Task AwaitParam(string a) 
    {
        Debug.Log("AwaitParam");
        await Task.Yield();
    }
    private async Task<string> AwaitReturn() 
    {
        Debug.Log("AwaitReturn");
        await Task.Yield();
        return "a";
    }
    private async Task<string> AwaitParamReturn(string data) 
    {
        Debug.Log("AwaitParamReturn");
        await Task.Yield();
        return "a";
    }
}
