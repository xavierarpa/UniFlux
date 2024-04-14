/*
Copyright (c) 2023 Xavier Arpa López Thomas Peter ('xavierarpa')

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
namespace UniFlux.Core.Internal
{
    ///<summary>
    /// Flux Action
    ///</summary>
    internal static class Flux<T> //(T, Action)
    {
        ///<summary>
        /// Defines a static instance of ActionFlux<T>
        ///</summary>
        internal static readonly IFlux<T, Action> flux_action = new ActionFlux<T>();
        // ///<summary>
        // /// Defines a static method that subscribes an action to a key with a condition
        // ///</summary>
        internal static void Store(in T key, in Action action, in bool condition) => flux_action.Store(in condition, key, action);
        // ///<summary>
        // /// Defines a static method that triggers an action with a key
        // ///</summary>
        internal static void Dispatch(in T key) => flux_action.Dispatch(key);
    }
}