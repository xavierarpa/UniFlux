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
namespace Kingdox.Flux
{
    public static partial class FluxExtension //Action
    {
        public static void Subscribe(this string key, Action action, bool condition) => Core.Flux.Subscribe(key, action, condition);
        public static void Invoke(this string key) => Core.Flux.Trigger(key);
    }
    public static partial class FluxExtension //Action<T>
    {
        public static void Subscribe<T>(this string key, Action<T> action, bool condition) => Core.Flux<T>.Subscribe(key, action, condition);
        public static void Invoke<T>(this string key, T @param) => Core.Flux<T>.Trigger(key, @param);
    }
    public static partial class FluxExtension //Func<out T>
    {
        public static void Subscribe<T>(this string key, Func<T> action, bool condition) => Core.Flux<T>.Subscribe(key, action, condition);
        public static T Invoke<T>(this string key) => Core.Flux<T>.Trigger(key);
    }
    public static partial class FluxExtension //Func<T, out T2>
    {
        public static void Subscribe<T,T2>(this string key, Func<T, T2> action, bool condition) => Core.Flux<T,T2>.Subscribe(key, action, condition);
        public static T2 Invoke<T, T2>(this string key, T @param) => Core.Flux<T, T2>.Trigger(key, @param);
    }
}
