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
    // Flux Func<out T>
    //<summary>
    public partial class FluxFunc<TKey, TReturn> : IFluxReturn<TKey, TReturn, Func<TReturn>>
    {
        Dictionary<TKey, Func<TReturn>> dictionary = new Dictionary<TKey, Func<TReturn>>();
        Dictionary<TKey, Func<TReturn>> IDictionary<TKey, Func<TReturn>>.Dictionary => dictionary;
        void ISubscribe<TKey, Func<TReturn>>.Subscribe(in bool condition, in TKey key, Func<TReturn> func)
        {
            if (condition)
            {
                if (!dictionary.ContainsKey(key)) dictionary.Add(key, default);
                dictionary[key] += func;
            }
            else if (dictionary.ContainsKey(key))
            {
                dictionary[key] -= func;
                if (dictionary[key] == null) dictionary.Remove(key);
            }
        }
        TReturn ITriggerReturn<TKey, TReturn>.Trigger(TKey key)
        {
            if (dictionary.ContainsKey(key)) return dictionary[key].Invoke();
            return default;
        }
    }
    //<summary>
    // Flux Func<T, out T2>
    //<summary>
    public partial class FluxFuncParam<TKey, TParam, TReturn> : IFluxParamReturn<TKey, TParam, TReturn, Func<TParam, TReturn>>
    {
        Dictionary<TKey, Func<TParam, TReturn>> dictionary = new Dictionary<TKey, Func<TParam, TReturn>>();
        Dictionary<TKey, Func<TParam, TReturn>> IDictionary<TKey, Func<TParam, TReturn>>.Dictionary => dictionary;
        void ISubscribe<TKey, Func<TParam, TReturn>>.Subscribe(in bool condition, in TKey key, Func<TParam, TReturn> func)
        {
            if (condition)
            {
                if (!dictionary.ContainsKey(key)) dictionary.Add(key, default);
                dictionary[key] += func;
            }
            else if (dictionary.ContainsKey(key))
            {
                dictionary[key] -= func;
                if (dictionary[key] == null) dictionary.Remove(key);
            }   
        }
        TReturn ITriggerParamReturn<TKey, TParam, TReturn>.Trigger(TKey key, TParam param)
        {
            if (dictionary.ContainsKey(key)) return dictionary[key].Invoke(param);
            return default;
        }
    }
}