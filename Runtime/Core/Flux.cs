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
using Kingdox.Flux.Core.Internal;
namespace Kingdox.Flux.Core
{
    //<summary>
    // Flux Action
    //<summary>
    public static partial class Flux // TKey (Action)
    {
        private static readonly IFlux<string, Action> flux_action = new FluxAction<string>();
        public static void Subscribe(string key, Action action, bool condition) => flux_action.Subscribe(in condition, in key, action);
        public static void Trigger(string key) => flux_action.Trigger(key);
    }
    //<summary>
    // Flux Action<T>
    //<summary>
    public static partial class Flux<T> // TKey TParam (Action<T>)
    {
        private static readonly IFluxParam<string, T, Action<T>> flux_action_param = new FluxActionParam<string,T>();
        public static void Subscribe(string key, Action<T> action, bool condition) => flux_action_param.Subscribe(in condition, in key, action);
        public static void Trigger(string key, T @param) => flux_action_param.Trigger(key, @param);
    }
    //<summary>
    // Flux Func<out T>
    //<summary>
    public static partial class Flux<T> // TKey TReturn (Func<out T>)
    {
        private static readonly IFluxReturn<string, T, Func<T>> flux_func = new FluxFunc<string,T>();
        public static void Subscribe(string key, Func<T> action, bool condition) => flux_func.Subscribe(in condition, in key, action);
        public static T Trigger(string key) => flux_func.Trigger(key);
    }
    //<summary>
    // Flux Func<T, out T>
    //<summary>
    public static partial class Flux<T,T2> // TKey TReturn (Func<T, out T>)
    {
        private static readonly IFluxParamReturn<string, T, T2, Func<T,T2>> flux_func_param = new FluxFuncParam<string, T, T2>();
        public static void Subscribe(string key, Func<T, T2> action, bool condition) => flux_func_param.Subscribe(in condition, in key, action);
        public static T2 Trigger(string key, T @param) => flux_func_param.Trigger(key, @param);
    }
}