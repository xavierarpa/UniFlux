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
    public partial class ActionFlux<TKey> :  IFlux<TKey, Action>
    {
        /// <summary>
        /// A dictionary that stores functions with no parameters
        /// </summary>
        Dictionary<TKey, Action> dictionary = new Dictionary<TKey, Action>();
        /// <summary>
        /// Gets the dictionary that stores the functions with no parameters
        /// </summary>
        Dictionary<TKey, Action> IDictionary<TKey, Action>.Dictionary => dictionary;
        ///<summary>
        /// Subscribes an event to the action dictionary if the given condition is met
        ///</summary>
        ///<param name="condition">Condition that must be true to subscribe the event</param>
        ///<param name="key">Key of the event to subscribe</param>
        ///<param name="action">Action to execute when the event is triggered</param>
        void ISubscribe<TKey, Action>.Subscribe(in bool condition, TKey key, Action action)
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
        ///<summary>
        /// Triggers the function stored in the dictionary with the specified key. 
        ///</summary>
        void ITrigger<TKey>.Trigger(TKey key)
        {
            if (dictionary.ContainsKey(key)) dictionary[key]?.Invoke();
        }
    }
    ///<summary>
    /// This class represents an implementation of an IFlux interface with a TKey key and an action without parameters.
    ///</summary>
    public partial class ActionFluxParam<TKey, TValue> : IFluxParam<TKey, TValue, Action<TValue>>
    {
        /// <summary>
        /// A dictionary that stores functions with parameters
        /// </summary>
        Dictionary<TKey, Action<TValue>> dictionary = new Dictionary<TKey, Action<TValue>>();
        /// <summary>
        /// Gets the dictionary that stores the functions with parameters
        /// </summary>
        Dictionary<TKey, Action<TValue>> IDictionary<TKey, Action<TValue>>.Dictionary => dictionary;
        ///<summary>
        /// Subscribes an event to the action dictionary if the given condition is met
        ///</summary>
        ///<param name="condition">Condition that must be true to subscribe the event</param>
        ///<param name="key">Key of the event to subscribe</param>
        ///<param name="action">Action to execute when the event is triggered</param>
        void ISubscribe<TKey, Action<TValue>>.Subscribe(in bool condition, TKey key, Action<TValue> action)
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
        ///<summary>
        /// Triggers the function stored in the dictionary with the specified key and set the parameter as argument 
        ///</summary>
        void ITriggerParam<TKey, TValue>.Trigger(TKey key, TValue param)
        {
            if (dictionary.ContainsKey(key)) dictionary[key]?.Invoke(param);
        }
    }
}