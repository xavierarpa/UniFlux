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
namespace Kingdox.Flux.Core
{
    public static partial class Flux // TKey (Action)
    {
        public static void SubscribeAction(byte key, Action action, bool condition) => Internal.Flux<byte>.SubscribeAction(key, action, condition);
        public static void TriggerAction(byte key) => Internal.Flux<byte>.TriggerAction(key);
    }
    public static partial class Flux<T> // TKey TParam (Action<T>)
    {
        public static void SubscribeActionParam(byte key, Action<T> action, bool condition) => Internal.Flux<byte,T>.SubscribeActionParam(key, action, condition);
        public static void TriggerActionParam(byte key, T @param) => Internal.Flux<byte,T>.TriggerActionParam(key, @param);
    }
    public static partial class Flux<T> // TKey TReturn (Func<out T>)
    {
        public static void SubscribeFunc(byte key, Func<T> action, bool condition) => Internal.Flux<byte,T>.SubscribeFunc(key, action, condition);
        public static T TriggerFunc(byte key) => Internal.Flux<byte, T>.TriggerFunc(key);
    }
    public static partial class Flux<T,T2> // TKey TReturn (Func<T, out T>)
    {
        public static void SubscribeFuncParam(byte key, Func<T, T2> action, bool condition) => Internal.Flux<byte,T,T2>.SubscribeFuncParam(key,action,condition);
        public static T2 TriggerFuncParam(byte key, T @param) => Internal.Flux<byte,T,T2>.TriggerFuncParam(key, @param);
    }    
}
