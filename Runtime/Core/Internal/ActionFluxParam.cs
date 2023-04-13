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
    ///<summary>
    /// This class represents an implementation of an IFlux interface with a TKey key and an action without parameters.
    ///</summary>
    internal sealed class ActionFluxParam<TKey, TValue> : IFluxParam<TKey, TValue, Action<TValue>>
    {
        /// <summary>
        /// A dictionary that stores functions with parameters
        /// </summary>
        internal readonly Dictionary<TKey, Action<TValue>> dictionary = new Dictionary<TKey, Action<TValue>>();
        /// <summary>
        /// A Read Only dictionary wich contains dictionary field
        /// </summary>
        internal readonly IReadOnlyDictionary<TKey, Action<TValue>> dictionary_read = null;
        /// <summary>
        /// Constructor of ActionFluxParam
        /// </summary>
        public ActionFluxParam()
        {
            dictionary_read = dictionary;
        }
        ///<summary>
        /// Subscribes an event to the action dictionary if the given condition is met
        ///</summary>
        ///<param name="condition">Condition that must be true to subscribe the event</param>
        ///<param name="key">Key of the event to subscribe</param>
        ///<param name="action">Action to execute when the event is triggered</param>
        void IStore<TKey, Action<TValue>>.Store(in bool condition, in TKey key, in Action<TValue> action)
        {
            if(dictionary_read.TryGetValue(key, out var _actions))
            {
                if (condition) _actions += action;
                else _actions -= action;
            }
            else if (condition) dictionary.Add(key, action);
        }
        ///<summary>
        /// Triggers the function stored in the dictionary with the specified key and set the parameter as argument 
        ///</summary>
        void IFluxParam<TKey, TValue, Action<TValue>>.Dispatch(TKey key, TValue param)
        {
            if(dictionary_read.TryGetValue(key, out var _actions)) _actions?.Invoke(param);
        }
    }
}