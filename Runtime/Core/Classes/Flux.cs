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
namespace Kingdox.UniFlux.Core.Internal
{
    ///<summary>
    /// Flux Action
    ///</summary>
    public static partial class Flux<T> //(T, Action)
    {
        ///<summary>
        /// Defines a static instance of ActionFlux<T>
        ///</summary>
        private static readonly IFlux<T, Action> flux_action = new ActionFlux<T>();
        ///<summary>
        /// Defines a static method that subscribes an action to a key with a condition
        ///</summary>
        public static void SubscribeAction(in T key, in Action action, in bool condition) => flux_action.Subscribe(in condition, key, action);
        ///<summary>
        /// Defines a static method that triggers an action with a key
        ///</summary>
        public static void TriggerAction(in T key) => flux_action.Trigger(key);
    }
    ///<summary>
    /// Flux<T> Action<T2>
    ///</summary>
    public static partial class Flux<T,T2> // (T, Action<T2>)
    {
        ///<summary>
        /// Defines a static instance of ActionFluxParam<T, T2>
        ///</summary>
        private static readonly IFluxParam<T, T2, Action<T2>> flux_action_param = new ActionFluxParam<T,T2>();
        ///<summary>
        /// Defines a static method that subscribes an action with a parameter to a key with a condition
        ///</summary>
        public static void SubscribeActionParam(in T key, in  Action<T2> action, in  bool condition) => flux_action_param.Subscribe(in condition, key, action);
        ///<summary>
        /// Defines a static method that triggers an action with a parameter with a key
        ///</summary>
        public static void TriggerActionParam(in T key,in  T2 @param) => flux_action_param.Trigger(key, @param);
    }
    ///<summary>
    /// Flux<T> Func<out T2>
    ///</summary>
    public static partial class Flux<T,T2> //  (T, Func<out T2>)
    {
        ///<summary>
        /// Defines a static instance of FuncFlux<T,T2>
        ///</summary>
        private static readonly IFluxReturn<T, T2, Func<T2>> flux_func = new FuncFlux<T,T2>();
        ///<summary>
        /// Defines a static method that subscribes a function that returns a value to a key with a condition
        ///</summary>
        public static void SubscribeFunc(in T key,in  Func<T2> action,in  bool condition) => flux_func.Subscribe(in condition, key, action);
        ///<summary>
        /// Defines a static method that triggers a function with a key and returns the result
        ///</summary>
        public static T2 TriggerFunc(in T key) => flux_func.Trigger(key);
    }
    ///<summary>
    /// Flux<T> Func<T2, out T3>
    ///</summary>
    public static partial class Flux<T,T2,T3> // (T, Func<T2, out T3>)
    {
        ///<summary>
        /// Defines a static instance of FuncFluxParam<T, T2, T3>
        ///</summary>
        private static readonly IFluxParamReturn<T, T2, T3, Func<T2,T3>> flux_func_param = new FuncFluxParam<T, T2, T3>();
        ///<summary>
        /// Defines a static method that subscribes a function with a parameter that returns a value to a key with a condition
        ///</summary>
        public static void SubscribeFuncParam(in T key, in Func<T2, T3> action, in bool condition) => flux_func_param.Subscribe(in condition, key, action);
        ///<summary>
        /// Defines a static method that triggers a function with a parameter with a key and returns the result
        ///</summary>
        public static T3 TriggerFuncParam(in T key, in T2 @param) => flux_func_param.Trigger(key, @param);
    }
}