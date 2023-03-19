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
namespace Kingdox.Flux.Core.Internal
{
    //<summary>
    // Flux Action
    //<summary>
    public partial class FluxAction<TKey> :  IFlux<TKey, Action>
    {
        Dictionary<TKey, Action> dictionary = new Dictionary<TKey, Action>();
        Dictionary<TKey, Action> IDictionary<TKey, Action>.Dictionary => dictionary;
        void ISubscribe<TKey, Action>.Subscribe(in bool condition, in TKey key, Action action)
        {
            if (condition)
            {
                if (!dictionary.ContainsKey(key)) dictionary.Add(key, default);
                dictionary[key] += action;
            }
            else if (dictionary.ContainsKey(key))
            {
                dictionary[key] -= action;
                if (dictionary[key] == null) dictionary.Remove(key);
            }
        }
        void ITrigger<TKey>.Trigger(TKey key)
        {
            if (dictionary.ContainsKey(key)) dictionary[key]?.Invoke();
        }
    }


    //<summary>
    // Flux Action<T>
    //<summary>
    public partial class FluxActionParam<TKey, TValue> : IFluxParam<TKey, TValue, Action<TValue>>
    {
        Dictionary<TKey, Action<TValue>> dictionary = new Dictionary<TKey, Action<TValue>>();
        Dictionary<TKey, Action<TValue>> IDictionary<TKey, Action<TValue>>.Dictionary => dictionary;
        void ISubscribe<TKey, Action<TValue>>.Subscribe(in bool condition, in TKey key, Action<TValue> action)
        {
            if (condition)
            {
                if (!dictionary.ContainsKey(key)) dictionary.Add(key, default);
                dictionary[key] += action;
            }
            else if (dictionary.ContainsKey(key))
            {
                dictionary[key] -= action;
                if (dictionary[key] == null) dictionary.Remove(key);
            }
        }
        void ITriggerParam<TKey, TValue>.Trigger(TKey key, TValue param)
        {
            if (dictionary.ContainsKey(key)) dictionary[key]?.Invoke(param);
        }
    }
}