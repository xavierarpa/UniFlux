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
using System.Collections.Generic;
namespace Kingdox.UniFlux.Core.Internal
{
    internal sealed class StateFlux<TKey, TValue> : IFluxParam<TKey, TValue, Action<TValue>>
    {
        internal readonly Dictionary<TKey, State<TValue>> dictionary = new Dictionary<TKey, State<TValue>>();
        void IStore<TKey, Action<TValue>>.Store(in bool condition, TKey key, Action<TValue> action)
        {
            if(dictionary.TryGetValue(key, out var state)) 
            {
                state.Store(condition,action);
            }
            else if (condition)
            {
                dictionary.Add(key, new State<TValue>(action));
            }
        }
        void IFluxParam<TKey, TValue, Action<TValue>>.Dispatch(TKey key, TValue param)
        {
            if(dictionary.TryGetValue(key, out var state)) 
            {
                state.Dispatch(param);
            }
            else
            {
                dictionary.Add(key, new State<TValue>(param));
            }
        }
    }
}