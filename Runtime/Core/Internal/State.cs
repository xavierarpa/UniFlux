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
    internal sealed class State<TValue>
    {
        private bool inited = false;
        private TValue state = default;
        private readonly HashSet<Action<TValue>> actions = new HashSet<Action<TValue>>();
        public State(Action<TValue> action)
        {
            Store(true, action);
        }
        public State(TValue value)
        {
            Dispatch(value);
        }
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
        private bool IsDifferentState(in TValue value) => !Object.Equals(value, state);
        public void Dispatch(in TValue value)
        {
            if (IsDifferentState(value))
            {
                state = value;
                foreach (var item in actions) item.Invoke(value);
            }
            inited=true;
        }
    }
}