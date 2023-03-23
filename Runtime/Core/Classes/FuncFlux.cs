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
    public partial class FuncFlux<TKey, TReturn> : IFluxReturn<TKey, TReturn, Func<TReturn>>
    {
        /// <summary>
        /// A dictionary that stores functions with no parameters and a return value of type `TReturn`.
        /// </summary>
        Dictionary<TKey, Func<TReturn>> dictionary = new Dictionary<TKey, Func<TReturn>>();
        /// <summary>
        /// Gets the dictionary that stores the functions with no parameters and a return value of type `TReturn`.
        /// </summary>
        Dictionary<TKey, Func<TReturn>> IDictionary<TKey, Func<TReturn>>.Dictionary => dictionary;
        /// <summary>
        /// Subscribes the provided function to the dictionary with the specified key when `condition` is true. 
        /// If `condition` is false and the dictionary contains the specified key, the function is removed from the dictionary.
        /// </summary>
        void ISubscribe<TKey, Func<TReturn>>.Subscribe(in bool condition, TKey key, Func<TReturn> func)
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
        // <summary>
        /// Triggers the function stored in the dictionary with the specified key and returns its return value. 
        /// If the dictionary does not contain the specified key, returns the default value of type `TReturn`.
        /// </summary>
        TReturn ITriggerReturn<TKey, TReturn>.Trigger(TKey key)
        {
            if (dictionary.ContainsKey(key)) return dictionary[key].Invoke();
            return default;
        }
    }
    /// <summary>
    /// The `FuncFluxParam` class represents a flux that stores functions with one parameter of type `TParam` and a return value of type `TReturn`.
    /// It provides a dictionary to store the functions and methods to subscribe and trigger the stored functions.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys used to store the functions in the dictionary.</typeparam>
    /// <typeparam name="TParam">The type of the parameter passed to the functions stored in the dictionary.</typeparam>
    /// <typeparam name="TReturn">The return type of the functions stored in the dictionary.</typeparam>
    public partial class FuncFluxParam<TKey, TParam, TReturn> : IFluxParamReturn<TKey, TParam, TReturn, Func<TParam, TReturn>>
    {
        /// <summary>
        /// A dictionary that stores functions with one parameter of type `TParam` and a return value of type `TReturn`.
        /// </summary>
        Dictionary<TKey, Func<TParam, TReturn>> dictionary = new Dictionary<TKey, Func<TParam, TReturn>>();
        /// <summary>
        /// Gets the dictionary that stores the functions with one parameter of type `TParam` and a return value of type `TReturn`.
        /// </summary>
        Dictionary<TKey, Func<TParam, TReturn>> IDictionary<TKey, Func<TParam, TReturn>>.Dictionary => dictionary;
        /// <summary>
        /// Subscribes the provided function to the dictionary with the specified key when `condition` is true. 
        /// If `condition` is false and the dictionary contains the specified key, the function is removed from the dictionary.
        /// </summary>
        void ISubscribe<TKey, Func<TParam, TReturn>>.Subscribe(in bool condition, TKey key, Func<TParam, TReturn> func)
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
        /// <summary>
        /// Triggers the function stored in the dictionary with the specified key and parameter, and returns its return value. 
        /// If the dictionary does not contain the specified key, returns the default value of type `TReturn`.
        /// </summary>
        TReturn ITriggerParamReturn<TKey, TParam, TReturn>.Trigger(TKey key, TParam param)
        {
            if (dictionary.ContainsKey(key)) return dictionary[key].Invoke(param);
            return default;
        }
    }
}