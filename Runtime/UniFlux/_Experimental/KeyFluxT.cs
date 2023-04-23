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
namespace Kingdox.UniFlux.Experimental
{
    [Serializable] public abstract class KeyFlux<T> : KeyFluxBase, IKeyFlux<T>
    {
        protected override object Key => KeyT;
        protected abstract T KeyT {get; }
        T IKeyFlux<T>.KeyT => KeyT;
        public sealed override void Store(in Action callback, in bool condition) => Core.Flux.Store(KeyT, callback, condition);
        public sealed override void Dispatch() => Core.Flux.Dispatch(KeyT);
        public sealed override void Store<T2>(in Action<T2> callback, in bool condition) => Core.Flux.Store<T,T2>(KeyT, in callback, in condition);
        public sealed override void Dispatch<T2>(in T2 @param) => Core.Flux.Dispatch(KeyT, in @param);
        public sealed override void Store<T2>(in Func<T2> callback, in bool condition) => Core.Flux.Store<T,T2>(KeyT, in callback, in condition);
        public sealed override T2 Dispatch<T2>() => Core.Flux.Dispatch<T, T2>(KeyT);
        public sealed override void Store<T2, T3>(in Func<T2, T3> callback, in bool condition) => Core.Flux.Store<T,T2,T3>(KeyT, in callback, in condition);
        public sealed override T3 Dispatch<T2, T3>(in T2 @param) => Core.Flux.Dispatch<T,T2,T3>(KeyT, in @param);
        public sealed override void StoreState<T2>(in Action<T2> callback, in bool condition) => Core.Flux.StoreState<T,T2>(KeyT, in callback, in condition);
        public sealed override void DispatchState<T2>(in T2 @param) => Core.Flux.DispatchState(KeyT, in @param);
    }
}