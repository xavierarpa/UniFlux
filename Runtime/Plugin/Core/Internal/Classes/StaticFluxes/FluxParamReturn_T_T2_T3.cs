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
    /// Flux<T> Func<T2, out T3>
    ///</summary>
    internal static class FluxParamReturn<T,T2,T3> // (T, Func<T2, out T3>)
    {
        /// <summary>
        /// Constructor
        /// </summary>
        static FluxParamReturn()
        {
            #if UNIFLUX_DEBUG
            UniFlux.Core.Internal.Flux.AddFluxType(typeof(FluxParamReturn<T,T2, T3>));
            #endif
        }
        ///<summary>
        /// Defines a static instance of FuncFluxParam<T, T2, T3>
        ///</summary>
        internal static readonly IFluxParamReturn<T, T2, T3, Func<T2,T3>> flux_func_param = new FuncParamFlux<T, T2, T3>();
        ///<summary>
        /// Defines a static method that subscribes a function with a parameter that returns a value to a key with a condition
        ///</summary>
        internal static void Store(in T key, in Func<T2, T3> action, in bool condition) => flux_func_param.Store(in condition, key, action);
        ///<summary>
        /// Defines a static method that triggers a function with a parameter with a key and returns the result
        ///</summary>
        internal static T3 Dispatch(in T key, in T2 @param) => flux_func_param.Dispatch(key, @param);
    }
}