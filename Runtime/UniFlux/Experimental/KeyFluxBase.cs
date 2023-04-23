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
using UnityEngine;
namespace Kingdox.UniFlux.Experimental
{
    [Serializable]
    public abstract class KeyFluxBase : ScriptableObject, IKeyFlux
    {
        protected abstract object Key { get; }
        object IKeyFlux.Key => Key;
        public virtual void Store(in Action callback, in bool condition) => Core.Flux.Store(Key, callback, condition);
        public virtual void Dispatch() => Core.Flux.Dispatch(Key);
        public virtual void Store<T2>(in Action<T2> callback, in bool condition) => Core.Flux.Store<object,T2>(Key, in callback, in condition);
        public virtual void Dispatch<T2>(in T2 @param) => Core.Flux.Dispatch(Key, in @param);
        public virtual void Store<T2>(in Func<T2> callback, in bool condition) => Core.Flux.Store<object,T2>(Key, in callback, in condition);
        public virtual T2 Dispatch<T2>() => Core.Flux.Dispatch<object, T2>(Key);
        public virtual void Store<T2, T3>(in Func<T2, T3> callback, in bool condition) => Core.Flux.Store<object,T2,T3>(Key, in callback, in condition);
        public virtual T3 Dispatch<T2, T3>(in T2 @param) => Core.Flux.Dispatch<object,T2,T3>(Key, in @param);
        public virtual void StoreState<T2>(in Action<T2> callback, in bool condition) => Core.Flux.StoreState<object,T2>(Key, in callback, in condition);
        public virtual void DispatchState<T2>(in T2 @param) => Core.Flux.DispatchState(Key, in @param);
    }
}