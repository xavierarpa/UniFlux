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
namespace Kingdox.UniFlux.Core.Internal
{
    internal static class FluxState<T,T2>
    {
        internal static readonly IFluxParam<T, T2, Action<T2>> flux_action_param = new StateFlux<T,T2>();
        internal static void Store(in T key, in  Action<T2> action, in  bool condition) => flux_action_param.Store(in condition, key, action);
        internal static void Dispatch(in T key,in  T2 @param) => flux_action_param.Dispatch(key, @param);
        internal static bool Get(in T key, out T2 _state)
        {
            //TODO TEMP
            if((flux_action_param as StateFlux<T,T2>).dictionary.TryGetValue(key, out var state)) 
            {
                return state.Get(out _state);
            }
            else
            {
                _state = default;
                return false;
            }
        } 
    }
}