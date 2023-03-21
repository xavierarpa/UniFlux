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
// asmdef Version Defines, enabled when com.cysharp.unitask is imported.
#if FLUX_UNITASK_SUPPORT
using System;
using Cysharp.Threading.Tasks;
namespace Kingdox.Flux
{
#region UniTask
    public static partial class FluxExtension //Action<UniTask>
    {
        public static void Subscribe(this byte key, Action<UniTask> action, bool condition) => Core.Flux<UniTask>.SubscribeActionParam(key, action, condition);
        public static void Subscribe(this byte key, Func<UniTask> action, bool condition) => Core.Flux<UniTask>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T>(this byte key, Func<UniTask<T>> action, bool condition) => Core.Flux<UniTask<T>>.SubscribeFunc(key, action, condition);
        public static void Subscribe<T>(this byte key, Func<T, UniTask> action, bool condition) => Core.Flux<T,UniTask>.SubscribeFuncParam(key, action, condition);
        public static void Subscribe<T,T2>(this byte key, Func<T, UniTask<T2>> action, bool condition) => Core.Flux<T,UniTask<T2>>.SubscribeFuncParam(key, action, condition);

        public static UniTask @UniTask(this byte key) => Core.Flux<UniTask>.TriggerFunc(key);
        public static UniTask<T> @UniTask<T>(this byte key) => Core.Flux<UniTask<T>>.TriggerFunc(key);
        public static UniTask @UniTask<T>(this byte key, T @param) => Core.Flux<T, UniTask>.TriggerFuncParam(key, @param);
        public static UniTask<T2> @UniTask<T, T2>(this byte key, T @param) => Core.Flux<T, UniTask<T2>>.TriggerFuncParam(key, @param);
    }
#endregion
}
#endif