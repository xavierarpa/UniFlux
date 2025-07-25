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
using System.Linq;
using System.Reflection;
using UniFlux.Core.Internal;
namespace UniFlux.Core
{
    /// <summary>
    /// This class represents an implementation of an IFluxParam with Action<T> and using State
    /// </summary>
    public sealed class StateFlux<TKey, TValue> : IFluxParam<TKey, TValue, Action<TValue>>
    {
        /// <summary>
        /// Storage of actions
        /// </summary>
        internal readonly Dictionary<TKey, State<TValue>> dictionary = new Dictionary<TKey, State<TValue>>();
        /// <summary>
        /// Stores the action 
        /// </summary>
        /// <param name="condition">True to add the action, false to remove it</param>
        /// <param name="key">The key to associate with the action</param>
        /// <param name="action">The action to store or remove</param>
        /// <exception cref="ArgumentNullException">Thrown when action is null</exception>
        public void Store(in bool condition, TKey key, Action<TValue> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if(dictionary.TryGetValue(key, out var state)) 
            {
                state.Store(condition, action);
            }
            else if (condition)
            {
                dictionary.Add(key, new State<TValue>(action));
            }
        }
        
        /// <summary>
        /// Dispatch TKey
        /// </summary>
        /// <param name="key">The key to dispatch</param>
        /// <param name="param">The parameter value to dispatch</param>
        public void Dispatch(TKey key, TValue param)
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

        internal Dictionary<TKey, List<MethodInfo>> __GetDictOfListMethods() => dictionary.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.actions.Select(a => a.Method).ToList()
        );
    }
}