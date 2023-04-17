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
    /// <summary>
    /// The `FuncFlux` class represents a flux that stores functions with no parameters and a return value of type `TReturn`.
    /// It provides a dictionary to store the functions and methods to subscribe and trigger the stored functions.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys used to store the functions in the dictionary.</typeparam>
    /// <typeparam name="TReturn">The return type of the functions stored in the dictionary.</typeparam>
    internal sealed class FuncFlux<TKey, TReturn> : IFluxReturn<TKey, TReturn, Func<TReturn>>
    {
        /// <summary>
        /// A dictionary that stores functions with no parameters and a return value of type `TReturn`.
        /// </summary>
        internal readonly Dictionary<TKey, List<Func<TReturn>>> dictionary = new Dictionary<TKey, List<Func<TReturn>>>();
        /// <summary>
        /// A Read Only dictionary wich contains dictionary field
        /// </summary>
        internal readonly IReadOnlyDictionary<TKey, List<Func<TReturn>>> dictionary_read = null;
        /// <summary>
        /// Constructor of FuncFlux
        /// </summary>
        public FuncFlux()
        {
            dictionary_read = dictionary;
        }
        /// <summary>
        /// Subscribes the provided function to the dictionary with the specified key when `condition` is true. 
        /// If `condition` is false and the dictionary contains the specified key, the function is removed from the dictionary.
        /// </summary>
        void IStore<TKey, Func<TReturn>>.Store(in bool condition, TKey key, Func<TReturn> func) 
        {
            // if(dictionary_read.ContainsKey(key))
            // {
            //     if (condition) dictionary[key] += func;
            //     else dictionary[key] -= func;
            // }
            // else if (condition) dictionary.Add(key, func);
            if(dictionary_read.TryGetValue(key, out var values))
            {
                if (condition) values.Add(func);
                else values.Remove(func);
            }
            else if (condition) dictionary.Add(key, new List<Func<TReturn>>(){func});
        }
        // <summary>
        /// Triggers the function stored in the dictionary with the specified key and returns its return value. 
        /// If the dictionary does not contain the specified key, returns the default value of type `TReturn`.
        /// </summary>
        TReturn IFluxReturn<TKey, TReturn, Func<TReturn>>.Dispatch(TKey key)
        {
            // if(dictionary_read.TryGetValue(key, out var _actions)) return _actions.Invoke();
            if(dictionary_read.TryGetValue(key, out var _actions)) 
            {
                for (int i = 0; i < _actions.Count - 1; i++)
                {
                    _actions[i].Invoke();
                }
                return _actions[_actions.Count-1].Invoke();
                UnityEngine.Debug.Log("_actions.Count");
            }
            return default;
        }
    }
}