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
using System.Reflection;
namespace UniFlux.Core.Internal
{
    /// <summary>
    /// A Class to handle value States, where TValue represents the type of value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class State<TValue>
    {
        /// <summary>
        /// Check whether the state was initialized with a value
        /// </summary>
        private bool inited = false;
        /// <summary>
        /// The current value of the state
        /// </summary>
        private TValue state = default;
        /// <summary>
        /// The set of actions that need to be cast when the state changes
        /// </summary>
        internal readonly HashSet<Action<TValue>> actions = new HashSet<Action<TValue>>();
        public State(Action<TValue> action)
        {
            Store(true, action);
        }
        public State(TValue value)
        {
            Dispatch(value);
        }
        /// <summary>
        /// Store or remove the action in actions to being handled by the current state
        /// </summary>
        public void Store(in bool condition, in Action<TValue> action)
        {
            if (condition)
            {
                actions.Add(action);
                if(inited)
                {
                    action.Invoke(state);
                }
            }
            else actions.Remove(action);
        }
        /// <summary>
        /// emit the value to modify the current state, only if the value is different
        /// </summary>
        public void Dispatch(in TValue value)
        {
            if(Equals(value, state)) // TODO: this generates Garbage (?)
            {
                // Do nothing
            }
            else
            {
                state = value;
                foreach (var item in actions) // TODO: this generates Garbage
                {
                    item.Invoke(value);
                }
            }
            inited=true;
        }
        /// <summary>
        /// retrieve the current state and return wether the state is initialized or not
        /// </summary>
        public bool Get(out TValue _value) 
        {
            _value = state;
            return inited;
        }
    }
}