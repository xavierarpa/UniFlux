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
namespace Kingdox.Flux
{
#region Common
    public static partial class FluxExtension //Action
    {
        public static void Subscribe(this int key, Action action, bool condition) => Core.Flux.SubscribeAction(key, action, condition);
        public static void Invoke(this int key) => Core.Flux.TriggerAction(key);
    }
    public static partial class FluxExtension //Action<T>
    {
        public static void Subscribe<T>(this int key, Action<T> action, bool condition) => Core.Flux<T>.SubscribeActionParam(key, action, condition);
        public static void Invoke<T>(this int key, T @param) => Core.Flux<T>.TriggerActionParam(key, @param);
    }
    public static partial class FluxExtension //Func<out T>
    {
        public static void Subscribe<T>(this int key, Func<T> action, bool condition) => Core.Flux<T>.SubscribeFunc(key, action, condition);
        public static T Invoke<T>(this int key) => Core.Flux<T>.TriggerFunc(key);
    }
    public static partial class FluxExtension //Func<T, out T2>
    {
        public static void Subscribe<T,T2>(this int key, Func<T, T2> action, bool condition) => Core.Flux<T,T2>.SubscribeFuncParam(key, action, condition);
        public static T2 Invoke<T, T2>(this int key, T @param) => Core.Flux<T, T2>.TriggerFuncParam(key, @param);
    }
#endregion
#region IEnumerator
    public static partial class FluxExtension //Action<IEnumerator>
    {
        public static void Subscribe(this int key, Action<IEnumerator> action, bool condition) => Core.Flux<IEnumerator>.SubscribeActionParam(key, action, condition);
        public static void Subscribe(this int key, Func<IEnumerator> action, bool condition) => Core.Flux<IEnumerator>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T>(this int key, Func<IEnumerator<T>> action, bool condition) => Core.Flux<IEnumerator<T>>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T>(this int key, Func<T, IEnumerator> action, bool condition) => Core.Flux<T,IEnumerator>.SubscribeFuncParam(key, action, condition);
        public static void Subscribe<T,T2>(this int key, Func<T, IEnumerator<T2>> action, bool condition) => Core.Flux<T,IEnumerator<T2>>.SubscribeFuncParam(key, action, condition);

        public static IEnumerator @IEnumerator(this int key) => Core.Flux<IEnumerator>.TriggerFunc(key);
        public static IEnumerator<T> @IEnumerator<T>(this int key) => Core.Flux<IEnumerator<T>>.TriggerFunc(key);
        public static IEnumerator @IEnumerator<T>(this int key, T @param) => Core.Flux<T, IEnumerator>.TriggerFuncParam(key, @param);
        public static IEnumerator<T2> @IEnumerator<T, T2>(this int key, T @param) => Core.Flux<T, IEnumerator<T2>>.TriggerFuncParam(key, @param);
    }
#endregion
#region Task
    public static partial class FluxExtension //Action<Task>
    {
        public static void Subscribe(this int key, Action<Task> action, bool condition) => Core.Flux<Task>.SubscribeActionParam(key, action, condition);
        public static void Subscribe(this int key, Func<Task> action, bool condition) => Core.Flux<Task>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T>(this int key, Func<Task<T>> action, bool condition) => Core.Flux<Task<T>>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T>(this int key, Func<T, Task> action, bool condition) => Core.Flux<T,Task>.SubscribeFuncParam(key, action, condition);
        public static void Subscribe<T,T2>(this int key, Func<T, Task<T2>> action, bool condition) => Core.Flux<T,Task<T2>>.SubscribeFuncParam(key, action, condition);

        public static Task @Task(this int key) => Core.Flux<Task>.TriggerFunc(key);
        public static Task<T> @Task<T>(this int key) => Core.Flux<Task<T>>.TriggerFunc(key);
        public static Task @Task<T>(this int key, T @param) => Core.Flux<T, Task>.TriggerFuncParam(key, @param);
        public static Task<T2> @Task<T, T2>(this int key, T @param) => Core.Flux<T, Task<T2>>.TriggerFuncParam(key, @param);
    }
#endregion
#region IObservable<T>
    public static partial class FluxExtension //Action<IObservable<T>>
    {
        public static void Subscribe<T>(this int key, Action<IObservable<T>> action, bool condition) => Core.Flux<IObservable<T>>.SubscribeActionParam(key, action, condition);
        public static void Subscribe<T>(this int key, Func<IObservable<T>> action, bool condition) => Core.Flux<IObservable<T>>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T,T2>(this int key, Func<T, IObservable<T2>> action, bool condition) => Core.Flux<T, IObservable<T2>>.SubscribeFuncParam(key, action, condition);
        public static void @IObservable<T>(this int key, IObservable<T> @param) => Core.Flux<IObservable<T>>.TriggerActionParam(key, @param);
        public static IObservable<T> @IObservable<T>(this int key) => Core.Flux<IObservable<T>>.TriggerFunc(key);
        public static IObservable<T2> @IObservable<T,T2>(this int key, T @param) => Core.Flux<T, IObservable<T2>>.TriggerFuncParam(key, @param);
    }
#endregion
#region IObserver<T>
    public static partial class FluxExtension //Action<IObserver<T>>
    {
        public static void Subscribe<T>(this int key, Action<IObserver<T>> action, bool condition) => Core.Flux<IObserver<T>>.SubscribeActionParam(key, action, condition);
        public static void Subscribe<T>(this int key, Func<IObserver<T>> action, bool condition) => Core.Flux<IObserver<T>>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T,T2>(this int key, Func<T, IObserver<T2>> action, bool condition) => Core.Flux<T, IObserver<T2>>.SubscribeFuncParam(key, action, condition);
        public static void @IObserver<T>(this int key, IObserver<T> @param) => Core.Flux<IObserver<T>>.TriggerActionParam(key, @param);
        public static IObserver<T> @IObserver<T>(this int key) => Core.Flux<IObserver<T>>.TriggerFunc(key);
        public static IObserver<T2> @IObserver<T,T2>(this int key, T @param) => Core.Flux<T, IObserver<T2>>.TriggerFuncParam(key, @param);
    }
#endregion
}
