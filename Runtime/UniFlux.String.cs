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
using Kingdox.UniFlux.Core;
// asmdef Version Defines, enabled when com.cysharp.unitask is imported.
#if UNIFLUX_UNITASK_SUPPORT
    using Cysharp.Threading.Tasks;
    namespace Kingdox.UniFlux
    {
        public static partial class FluxExtension //Action<UniTask>
        {
            public static void Store(this string key, Action<UniTask> action, bool condition) => Flux.Store(key, action, condition);
            public static void Store(this string key, Func<UniTask> action, bool condition) => Flux.Store(key, action, condition);
            public static void Store<T>(this string key, Func<UniTask<T>> action, bool condition) => Flux.Store(key, action, condition);
            public static void Store<T>(this string key, Func<T, UniTask> action, bool condition) => Flux.Store(key, action, condition);
            public static void Store<T,T2>(this string key, Func<T, UniTask<T2>> action, bool condition) => Flux.Store(key, action, condition);

            public static UniTask @UniTask(this string key) => Flux.Dispatch<string, UniTask>(key);
            public static UniTask<T> @UniTask<T>(this string key) => Flux.Dispatch<string, UniTask<T>>(key);
            public static UniTask @UniTask<T>(this string key, T @param) => Flux.Dispatch<string, T, UniTask>(key, @param);
            public static UniTask<T2> @UniTask<T, T2>(this string key, T @param) => Flux.Dispatch<string, T, UniTask<T2>>(key, @param);
        }
    }
#endif
#if UNIFLUX_UNIRX_SUPPORT
    namespace Kingdox.UniFlux
    {
        #if !(NETFX_CORE || NET_4_6 || NET_STANDARD_2_0 || UNITY_WSA_10_0)
        public static partial class FluxExtension //Action<UniRx.IObservable<T>>
        {
            public static void Store<T>(this string key, Action<UniRx.IObservable<T>> action, bool condition) => Flux.Store(key, action, condition);
            public static void Store<T>(this string key, Func<UniRx.IObservable<T>> action, bool condition) => Flux.Store(key, action, condition);
            public static void Store<T,T2>(this string key, Func<T, UniRx.IObservable<T2>> action, bool condition) => Flux.Store(key, action, condition);
            public static void @IObservable<T>(this string key, UniRx.IObservable<T> @param) => Flux.Dispatch(key, @param);
            public static UniRx.IObservable<T> @IObservable<T>(this string key) => Flux.Dispatch<int,UniRx.IObservable<T>>(key);
            public static UniRx.IObservable<T2> @IObservable<T,T2>(this string key, T @param) => Flux.Dispatch<int, T, UniRx.IObservable<T2>>(key, @param);
        }
        public static partial class FluxExtension //Action<UniRx.IObserver<T>>
        {
            public static void Store<T>(this string key, Action<UniRx.IObserver<T>> action, bool condition) => Flux.Store(key, action, condition);
            public static void Store<T>(this string key, Func<UniRx.IObserver<T>> action, bool condition) => Flux.Store(key, action, condition);
            public static void Store<T,T2>(this string key, Func<T, UniRx.IObserver<T2>> action, bool condition) => Flux.Store(key, action, condition);
            public static void @IObserver<T>(this string key, UniRx.IObserver<T> @param) => Flux.Dispatch(key, @param);
            public static UniRx.IObserver<T> @IObserver<T>(this string key) => Flux.Dispatch<int,UniRx.IObserver<T>>(key);
            public static UniRx.IObserver<T2> @IObserver<T,T2>(this string key, T @param) => Flux.Dispatch<int, T, UniRx.IObserver<T2>>(key, @param);
        }
        #endif
    }
#endif
namespace Kingdox.UniFlux
{
#region Common
    public static partial class FluxExtension //Action
    {
        public static void Store(this string key, in Action action, in bool condition) => Flux.Store(in key, in action,in condition);
        public static void Dispatch(this string key) => Flux.Dispatch(key);
    }
    public static partial class FluxExtension //Action<T>
    {
        public static void Store<T>(this string key, in Action<T> action,in bool condition) => Flux.Store(in key, in action, in condition);
        public static void Dispatch<T>(this string key, in T @param) => Flux.Dispatch(in key, in @param);
    }
    public static partial class FluxExtension //Func<out T>
    {
        public static void Store<T>(this string key, in Func<T> action, in bool condition) => Flux.Store(in key, in action, in condition);
        public static T Dispatch<T>(this string key) => Flux.Dispatch<string, T>(in key);
    }
    public static partial class FluxExtension //Func<T, out T2>
    {
        public static void Store<T,T2>(this string key, in Func<T, T2> action, in bool condition) => Flux.Store(in key, in action, in condition);
        public static T2 Dispatch<T, T2>(this string key, in T @param) => Flux.Dispatch<string, T, T2>(in key, in @param);
    }
    public static partial class FluxExtension //State Action<T>
    {
        public static void StoreState<T>(this string key, in Action<T> action,in bool condition) => Flux.StoreState(in key, in action, in condition);
        public static void DispatchState<T>(this string key, in T state) => Flux.DispatchState(in key, in state);
        public static bool GetState<T>(this string key, out T state) => Flux.GetState(in key, out state);
    }
#endregion
#region IEnumerator
    public static partial class FluxExtension //Action<IEnumerator>
    {
        public static void Store(this string key, in Action<IEnumerator> action, in bool condition) => Flux.Store(in key, in action, in condition);
        public static void Store(this string key, in Func<IEnumerator> action, in bool condition) => Flux.Store(in key, in action, in condition);
        public static void Store<T>(this string key, in Func<IEnumerator<T>> action, in bool condition) => Flux.Store(in key, in action, in condition);
        public static void Store<T>(this string key, in Func<T, IEnumerator> action, in bool condition) => Flux.Store(in key, in action, in condition);
        public static void Store<T,T2>(this string key, in Func<T, IEnumerator<T2>> action, in bool condition) => Flux.Store(in key, in action, in condition);

        public static IEnumerator @IEnumerator(this string key) => Flux.Dispatch<string, IEnumerator>(in key);
        public static IEnumerator<T> @IEnumerator<T>(this string key) => Flux.Dispatch<string, IEnumerator<T>>(in key);
        public static IEnumerator @IEnumerator<T>(this string key, in T @param) => Flux.Dispatch<string, T, IEnumerator>(in key, in @param);
        public static IEnumerator<T2> @IEnumerator<T, T2>(this string key, in T @param) => Flux.Dispatch<string, T, IEnumerator<T2>>(in key, in @param);
    }
#endregion
#region Task
    public static partial class FluxExtension //Action<Task>
    {
        public static void Store(this string key, in Action<Task> action, in bool condition) => Flux.Store(in key,in action,in condition);
        public static void Store(this string key, in Func<Task> action, in bool condition) => Flux.Store(in key,in action,in condition);
        public static void Store<T>(this string key, in Func<Task<T>> action, in bool condition) => Flux.Store(in key,in action,in condition);
        public static void Store<T>(this string key, in Func<T, Task> action, in bool condition) => Flux.Store(in key,in action,in condition);
        public static void Store<T,T2>(this string key, in Func<T, Task<T2>> action, in bool condition) => Flux.Store(in key,in action,in condition);

        public static Task @Task(this string key) => Flux.Dispatch<string, Task>(key);
        public static Task<T> @Task<T>(this string key) => Flux.Dispatch<string, Task<T>>(key);
        public static Task @Task<T>(this string key, in T @param) => Flux.Dispatch<string, T, Task>(key, in @param);
        public static Task<T2> @Task<T, T2>(this string key, in T @param) => Flux.Dispatch<string, T, Task<T2>>(key, in @param);
    }
#endregion
#region IObservable<T>
    public static partial class FluxExtension //Action<IObservable<T>>
    {
        public static void Store<T>(this string key, Action<IObservable<T>> action, bool condition) => Flux.Store(key, action, condition);
        public static void Store<T>(this string key, Func<IObservable<T>> action, bool condition) => Flux.Store(key, action, condition);
        public static void Store<T,T2>(this string key, Func<T, IObservable<T2>> action, bool condition) => Flux.Store(key, action, condition);
        public static void @IObservable<T>(this string key, IObservable<T> @param) => Flux.Dispatch(key, @param);
        public static IObservable<T> @IObservable<T>(this string key) => Flux.Dispatch<string,IObservable<T>>(key);
        public static IObservable<T2> @IObservable<T,T2>(this string key, T @param) => Flux.Dispatch<string, T, IObservable<T2>>(key, @param);
    }
#endregion
#region IObserver<T>
    public static partial class FluxExtension //Action<IObserver<T>>
    {
        public static void Store<T>(this string key, Action<IObserver<T>> action, bool condition) => Flux.Store(key, action, condition);
        public static void Store<T>(this string key, Func<IObserver<T>> action, bool condition) => Flux.Store(key, action, condition);
        public static void Store<T,T2>(this string key, Func<T, IObserver<T2>> action, bool condition) => Flux.Store(key, action, condition);
        public static void @IObserver<T>(this string key, IObserver<T> @param) => Flux.Dispatch(key, @param);
        public static IObserver<T> @IObserver<T>(this string key) => Flux.Dispatch<string, IObserver<T>>(key);
        public static IObserver<T2> @IObserver<T,T2>(this string key, T @param) => Flux.Dispatch<string, T, IObserver<T2>>(key, @param);
    }
#endregion
}
