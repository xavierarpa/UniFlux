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
using System.Collections.Generic;
using UniFlux.Core.Internal;
namespace UniFlux.Core
{
    /// <summary>
    /// This class represents an implementation of an IFluxParamReturn with Func<TParam, TReturn>
    /// </summary>
    public sealed class FuncParamFlux<TKey, TParam, TReturn> : IFluxParamReturn<TKey, TParam, TReturn, Func<TParam, TReturn>>
    {
        /// <summary>
        /// Storage of actions
        /// </summary>
        internal readonly Dictionary<TKey, Func<TParam, TReturn>> dictionary = new Dictionary<TKey, Func<TParam, TReturn>>();
        ///<summary>
        /// Stores the action 
        ///</summary>
        public void Store(in bool condition, TKey key, Func<TParam, TReturn> func)
        {
            if(dictionary.TryGetValue(key, out var values))
            {
                if (condition) dictionary[key] += func;
                else
                {
                    values -= func;
                    if (values is null) dictionary.Remove(key);
                    else dictionary[key] = values;
                }
            }
            else if (condition) dictionary.Add(key, func);
        }
        ///<summary>
        /// Dispatch TKey
        ///</summary>
        public TReturn Dispatch(TKey key, TParam param)
        {
            if(dictionary.TryGetValue(key, out var _actions))
            {
                return _actions.Invoke(param);
            }
            return default;
        }
    }
}