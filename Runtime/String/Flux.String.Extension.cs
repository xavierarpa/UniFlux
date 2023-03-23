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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Kingdox.UniFlux
{
#region Common
    public static partial class FluxExtension //Action
    {
        public static void Subscribe(this string key, in Action action, in bool condition) => Core.Flux.SubscribeAction(in key,in action,in condition);
        public static void Invoke(this string key) => Core.Flux.TriggerAction(key);
    }
    public static partial class FluxExtension //Action<T>
    {
        public static void Subscribe<T>(this string key, in Action<T> action,in bool condition) => Core.Flux<T>.SubscribeActionParam(in key, in action, in condition);
        public static void Invoke<T>(this string key, in T @param) => Core.Flux<T>.TriggerActionParam(in key, in @param);
    }
    public static partial class FluxExtension //Func<out T>
    {
        public static void Subscribe<T>(this string key, in Func<T> action, in bool condition) => Core.Flux<T>.SubscribeFunc(in key, in action, in condition);
        public static T Invoke<T>(this string key) => Core.Flux<T>.TriggerFunc(in key);
    }
    public static partial class FluxExtension //Func<T, out T2>
    {
        public static void Subscribe<T,T2>(this string key, in Func<T, T2> action, in bool condition) => Core.Flux<T,T2>.SubscribeFuncParam(in key, in action, in condition);
        public static T2 Invoke<T, T2>(this string key, in T @param) => Core.Flux<T, T2>.TriggerFuncParam(in key, in @param);
    }
#endregion
#region IEnumerator
    public static partial class FluxExtension //Action<IEnumerator>
    {
        public static void Subscribe(this string key, in Action<IEnumerator> action, in bool condition) => Core.Flux<IEnumerator>.SubscribeActionParam(in key, in action, in condition);
        public static void Subscribe(this string key, in Func<IEnumerator> action, in bool condition) => Core.Flux<IEnumerator>.SubscribeFunc(in key, in action, in condition);
        public static void Subscribe<T>(this string key, in Func<IEnumerator<T>> action, in bool condition) => Core.Flux<IEnumerator<T>>.SubscribeFunc(in key, in action, in condition);
        public static void Subscribe<T>(this string key, in Func<T, IEnumerator> action, in bool condition) => Core.Flux<T,IEnumerator>.SubscribeFuncParam(in key, in action, in condition);
        public static void Subscribe<T,T2>(this string key, in Func<T, IEnumerator<T2>> action, in bool condition) => Core.Flux<T,IEnumerator<T2>>.SubscribeFuncParam(in key, in action, in condition);

        public static IEnumerator @IEnumerator(this string key) => Core.Flux<IEnumerator>.TriggerFunc(in key);
        public static IEnumerator<T> @IEnumerator<T>(this string key) => Core.Flux<IEnumerator<T>>.TriggerFunc(in key);
        public static IEnumerator @IEnumerator<T>(this string key, in T @param) => Core.Flux<T, IEnumerator>.TriggerFuncParam(in key, in @param);
        public static IEnumerator<T2> @IEnumerator<T, T2>(this string key, in T @param) => Core.Flux<T, IEnumerator<T2>>.TriggerFuncParam(in key, in @param);
    }
#endregion
#region Task
    public static partial class FluxExtension //Action<Task>
    {
        public static void Subscribe(this string key, in Action<Task> action, in bool condition) => Core.Flux<Task>.SubscribeActionParam(in key,in action,in condition);
        public static void Subscribe(this string key, in Func<Task> action, in bool condition) => Core.Flux<Task>.SubscribeFunc(in key,in action,in condition);
        public static void Subscribe<T>(this string key, in Func<Task<T>> action, in bool condition) => Core.Flux<Task<T>>.SubscribeFunc(in key,in action,in condition);
        public static void Subscribe<T>(this string key, in Func<T, Task> action, in bool condition) => Core.Flux<T,Task>.SubscribeFuncParam(in key,in action,in condition);
        public static void Subscribe<T,T2>(this string key, in Func<T, Task<T2>> action, in bool condition) => Core.Flux<T,Task<T2>>.SubscribeFuncParam(in key,in action,in condition);

        public static Task @Task(this string key) => Core.Flux<Task>.TriggerFunc(key);
        public static Task<T> @Task<T>(this string key) => Core.Flux<Task<T>>.TriggerFunc(key);
        public static Task @Task<T>(this string key, in T @param) => Core.Flux<T, Task>.TriggerFuncParam(key, in @param);
        public static Task<T2> @Task<T, T2>(this string key, in T @param) => Core.Flux<T, Task<T2>>.TriggerFuncParam(key, in @param);
    }
#endregion
#region IObservable<T>
    public static partial class FluxExtension //Action<IObservable<T>>
    {
        public static void Subscribe<T>(this string key, Action<IObservable<T>> action, bool condition) => Core.Flux<IObservable<T>>.SubscribeActionParam(key, action, condition);
        public static void Subscribe<T>(this string key, Func<IObservable<T>> action, bool condition) => Core.Flux<IObservable<T>>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T,T2>(this string key, Func<T, IObservable<T2>> action, bool condition) => Core.Flux<T, IObservable<T2>>.SubscribeFuncParam(key, action, condition);
        public static void @IObservable<T>(this string key, IObservable<T> @param) => Core.Flux<IObservable<T>>.TriggerActionParam(key, @param);
        public static IObservable<T> @IObservable<T>(this string key) => Core.Flux<IObservable<T>>.TriggerFunc(key);
        public static IObservable<T2> @IObservable<T,T2>(this string key, T @param) => Core.Flux<T, IObservable<T2>>.TriggerFuncParam(key, @param);
    }
#endregion
#region IObserver<T>
    public static partial class FluxExtension //Action<IObserver<T>>
    {
        public static void Subscribe<T>(this string key, Action<IObserver<T>> action, bool condition) => Core.Flux<IObserver<T>>.SubscribeActionParam(key, action, condition);
        public static void Subscribe<T>(this string key, Func<IObserver<T>> action, bool condition) => Core.Flux<IObserver<T>>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T,T2>(this string key, Func<T, IObserver<T2>> action, bool condition) => Core.Flux<T, IObserver<T2>>.SubscribeFuncParam(key, action, condition);
        public static void @IObserver<T>(this string key, IObserver<T> @param) => Core.Flux<IObserver<T>>.TriggerActionParam(key, @param);
        public static IObserver<T> @IObserver<T>(this string key) => Core.Flux<IObserver<T>>.TriggerFunc(key);
        public static IObserver<T2> @IObserver<T,T2>(this string key, T @param) => Core.Flux<T, IObserver<T2>>.TriggerFuncParam(key, @param);
    }
#endregion
}
